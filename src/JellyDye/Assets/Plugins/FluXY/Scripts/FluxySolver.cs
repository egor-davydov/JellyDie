﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


/**
\mainpage FluXY documentation
 
Introduction:
------------- 
     
FluXY is a GPU, grid-based, 2.5D fluid simulator. It's lightweight, fast, robust, and easy to use.
Can be used to simulate fire, smoke, paint, water, trails, and a variety of VFX.
 
Features:
-------------------

- Uses vanilla vertex/fragment shaders, no compute shader support required.
- Compatible with all rendering pipelines: built-in, URP and HDRP.
- Expand 2D fluids into the 3D realm
- Dynamic Level of Detail (LOD)
- Simulate fire, smoke, ink, water, and other VFX
- Turbulence, pressure, vorticity, buoyancy, external forces...
- Inertial effects
- Lighting fast pressure solver
- Parallel simulation of multiple fluid containers
- Fast support and regular updates

*/

namespace Fluxy
{
    [AddComponentMenu("Physics/FluXY/Solver", 800)]
    public class FluxySolver : MonoBehaviour
    {
        private const int MAX_TILES = 17; // 16 + 1 phantom tile.

        public enum PressureSolver
        {
            Separable,
            Iterative
        }

        [Flags]
        public enum ReadbackMode
        {
            None = 0,
            Density = 1 << 0,
            Velocity = 1 << 1
        }

        /// <summary>
        /// Storage used to store and manage simulation buffers.
        /// </summary>
        [Header("Storage")]
        [Tooltip("Storage used to store and manage simulation buffers.")]
        public FluxyStorage storage;

        /// <summary>
        /// Desired buffer resolution.
        /// </summary>
        [Tooltip("Desired buffer resolution.")]
        [Delayed]
        [Min(16)]
        public int desiredResolution = 128;

        /// <summary>
        /// Supersampling used by density buffer. Eg. a value of 4 will use a density buffer that's 4 times the size of the velocity buffer.
        /// </summary>
        [Tooltip("Supersampling used by density buffer. Eg. a value of 4 will use a density buffer that's 4 times the size of the velocity buffer.")]
        [Range(1, 8)]
        public int densitySupersampling = 2;

        /// <summary>
        /// Dispose of this solver's buffers when culled by LOD.
        /// </summary>
        [Tooltip("Dispose of this solver's buffers when culled by LOD.")]
        public bool disposeWhenCulled = false;

        /// <summary>
        /// Allows this solver's data to be read back to the CPU.
        /// </summary>
        [Tooltip("Allows this solver's data to be read back to the CPU.")]
        public ReadbackMode readable = ReadbackMode.None;

        /// <summary>
        /// Material used to update fluid simulation.
        /// </summary>
        [Header("Simulation")]
        [Tooltip("Material used to update fluid simulation.")]
        public Material simulationMaterial;

        /// <summary>
        /// Maximum amount of time advanced in a single simulation step.
        /// </summary>
        [Tooltip("Maximum amount of time advanced in a single simulation step.")]
        [Min(0.0001f)]
        public float maxTimestep = 0.008f;

        /// <summary>
        /// Maximum amount of simulation steps taken in a single frame.
        /// </summary>
        [Tooltip("Maximum amount of simulation steps taken in a single frame.")]
        [Min(1)]
        public float maxSteps = 4;

        /// <summary>
        /// Type of pressure solver used: traditional, iterative Jacobi or separable poisson filter.
        /// </summary>
        [Tooltip("Type of pressure solver used: traditional, iterative Jacobi or separable poisson filter.")]
        public PressureSolver pressureSolver = PressureSolver.Separable;

        /// <summary>
        /// Amount of iterations when the iterative pressure solver is being used.
        /// </summary>
        [Tooltip("Amount of iterations when the iterative pressure solver is being used.")]
        [Range(0,32)]
        public int pressureIterations = 3;


        private int visibleLOD;
        private bool visible = true;

        private List<FluxyContainer> containers = new List<FluxyContainer>();
        private int framebufferID = -1;
        private bool tilesDirty;

        private byte[] indices = new byte[MAX_TILES];
        private Vector4[] rects = new Vector4[MAX_TILES];
        private Vector4[] externalForce = new Vector4[MAX_TILES];
        private Vector4[] buoyancy = new Vector4[MAX_TILES];
        private Vector4[] dissipation = new Vector4[MAX_TILES];
        private float[] pressure = new float[MAX_TILES];
        private float[] viscosity = new float[MAX_TILES];
        private float[] turbulence = new float[MAX_TILES];
        private float[] adhesion = new float[MAX_TILES];
        private float[] surfaceTension = new float[MAX_TILES];
        private Vector4[] wrapmode = new Vector4[MAX_TILES];
        private Vector4[] densityFalloff = new Vector4[MAX_TILES];
        private Vector4[] offsets = new Vector4[MAX_TILES];
        private Camera _camera;

        public delegate void SolverCallback(FluxySolver solver);
        public event SolverCallback OnStep;

        public FluxyStorage.Framebuffer framebuffer
        {
            get { return storage != null ? storage.GetFramebuffer(framebufferID) : null; }
        }

        public Texture2D velocityReadbackTexture { get; private set; }
        public Texture2D densityReadbackTexture { get; private set; }

        private void OnEnable()
        {
            visibleLOD = 0;
            UpdateFramebuffer();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnDisable()
        {
            DisposeOfFramebuffer();
        }

        private void OnValidate()
        {
            UpdateFramebuffer();
        }

        public bool IsFull()
        {
            return containers.Count >= MAX_TILES - 1;
        }

        public bool RegisterContainer(FluxyContainer container)
        {
            if (IsFull())
                return false;

            if (!containers.Contains(container))
            {
                containers.Add(container);
                tilesDirty = true;
            }

            return true;
        }

        public void UnregisterContainer(FluxyContainer container)
        {
            if (containers.Contains(container))
            {
                containers.Remove(container);
                tilesDirty = true;
            }
        }

        public int GetContainerID(FluxyContainer container)
        {
            return containers.IndexOf(container);
        }

        public Vector4 GetContainerUVRect(FluxyContainer container)
        {
            int index = containers.IndexOf(container);
            if (index >= 0)
                return rects[index + 1];
            return Vector4.zero;
        }

        private void UpdateFramebuffer()
        {
            if (!visible)
            {
                if (disposeWhenCulled)
                    DisposeOfFramebuffer();
                else return;
            }
            // visible, but not yet created.
            else if (framebufferID < 0)
            {
                // create a framebuffer.
                if (storage != null)
                    framebufferID = storage.RequestFramebuffer(desiredResolution / (visibleLOD + 1), densitySupersampling);
            }
            // visible and created.
            else
            {
                // update resolution based on LOD:
                var fb = framebuffer;
                if (fb != null)
                {
                    fb.desiredResolution = desiredResolution / (visibleLOD + 1);
                    fb.stateSupersampling = densitySupersampling;
                    storage.ResizeStorage();
                }
            }

            var b = framebuffer;
            if (b != null)
            {
                velocityReadbackTexture = new Texture2D(b.velocityA.width, b.velocityA.height, TextureFormat.RGBAHalf, false);
                densityReadbackTexture = new Texture2D(b.stateA.width, b.stateA.height, TextureFormat.RGBAHalf, false);

                Color[] resetColorArray = velocityReadbackTexture.GetPixels();
                for (int i = 0; i < resetColorArray.Length; i++)
                    resetColorArray[i] = new Color(0,0,0,0);
                velocityReadbackTexture.SetPixels(resetColorArray);
                velocityReadbackTexture.Apply();

                resetColorArray = densityReadbackTexture.GetPixels();
                for (int i = 0; i < resetColorArray.Length; i++)
                    resetColorArray[i] = new Color(0, 0, 0, 0);
                densityReadbackTexture.SetPixels(resetColorArray);
                densityReadbackTexture.Apply();
            }
        }

        private void DisposeOfFramebuffer()
        {
            if (storage != null && framebufferID >= 0)
            {
                storage.DisposeFramebuffer(framebufferID);
                framebufferID = -1;
            }

            Destroy(velocityReadbackTexture);
            Destroy(densityReadbackTexture);
        }
        
        protected virtual void SimulationStep(FluxyStorage.Framebuffer fb, float deltaTime)
        {
            if (fb == null)
                return;

            // callback for custom stuff:
            OnStep?.Invoke(this);

            fb.velocityA.filterMode = FilterMode.Point;
            fb.stateA.filterMode = FilterMode.Point;

            simulationMaterial.SetFloat("_DeltaTime", deltaTime);
            simulationMaterial.SetTexture("_Velocity", fb.velocityA);
            simulationMaterial.SetTexture("_State", fb.stateB);

            // advection (state):
            Graphics.Blit(fb.stateA, fb.stateB, simulationMaterial, 0);

            // advection (velocity):
            Graphics.Blit(fb.velocityA, fb.velocityB, simulationMaterial, 1);

            // dissipation:
            Graphics.Blit(fb.stateB, fb.stateA, simulationMaterial, 2);

            // calculate curl:
            Graphics.Blit(fb.velocityB, fb.velocityA, simulationMaterial, 3);

            // density gradient: 
            Graphics.Blit(fb.stateA, fb.stateB, simulationMaterial, 4);

            // apply external forces and project velocity to surface:
            fb.stateB.filterMode = FilterMode.Bilinear;
            UpdateExternalForces(fb.velocityA, fb.velocityB);
            fb.stateB.filterMode = FilterMode.Point;

            // calculate divergence:
            Graphics.Blit(fb.velocityB, fb.velocityA, simulationMaterial, 5);

            // calculate pressure:
            if (pressureSolver == PressureSolver.Separable)
            {
                Graphics.Blit(fb.velocityA, fb.velocityB, simulationMaterial, 11);
                simulationMaterial.SetVector("axis", Vector2.right);
                Graphics.Blit(fb.velocityB, fb.velocityA, simulationMaterial, 6);
                simulationMaterial.SetVector("axis", Vector2.up);
                Graphics.Blit(fb.velocityA, fb.velocityB, simulationMaterial, 6);
            }
            else
            {
                for (int i = 0; i < pressureIterations; ++i)
                {
                    Graphics.Blit(fb.velocityA, fb.velocityB, simulationMaterial, 10);
                    Graphics.Blit(fb.velocityB, fb.velocityA, simulationMaterial, 10);
                }
                Graphics.Blit(fb.velocityA, fb.velocityB);
            }

            // subtract pressure gradient from velocity field:
            Graphics.Blit(fb.velocityB, fb.velocityA, simulationMaterial, 7);

            fb.velocityA.filterMode = FilterMode.Bilinear;
            fb.stateA.filterMode = FilterMode.Bilinear;
        }

        private void UpdateExternalForces(RenderTexture source, RenderTexture dest)
        {
         
            RenderTexture old = RenderTexture.active;
            RenderTexture.active = dest;

            // Clear dest before copying data from source.
            // This ensures velocity is set to zero outside of the mapped texture regions.
            GL.Clear(false, true, Color.clear);

            simulationMaterial.SetTexture("_MainTex", source);

            // for each container, draw directly into the velocity map:
            for (byte i = 0; i < containers.Count; ++i)
            {
                byte tile = i;
                tile += 1;
                byte c = indices[tile];

                simulationMaterial.SetInt("_TileIndex", tile);
                simulationMaterial.SetFloat("_NormalScale", containers[c].normalScale);
                simulationMaterial.SetVector("_NormalTiling", containers[c].normalTiling);
                simulationMaterial.SetTexture("_Normals", containers[c].surfaceNormals);

                // must call SetPass() *after* setting material properties:
                if (simulationMaterial.SetPass(12))
                {
                    GL.PushMatrix();
                    GL.LoadProjectionMatrix(Matrix4x4.Ortho(0, 1, 0, 1, -1, 1));
                    Graphics.DrawMeshNow(containers[c].containerMesh, containers[c].transform.localToWorldMatrix);
                    GL.PopMatrix();
                }
            }

            RenderTexture.active = old;
        }

        public void ClearContainer(int id)
        {
            var fb = framebuffer;

            if (fb != null && simulationMaterial != null && id >= 0 && id < indices.Length)
            {
                UpdateTileData();

                int tile = id + 1;
                int c = indices[tile];

                simulationMaterial.SetInt("_TileIndex", tile);
                simulationMaterial.SetColor("_ClearColor", containers[c].clearColor);
                Graphics.Blit(containers[c].clearTexture, fb.stateA, simulationMaterial, 9);
            }
        }

        public void UpdateTileData()
        {
            if (tilesDirty)
            {
                // phantom rect should span the entire UV space from -1 to 2.
                rects[0] = new Vector4(-1, -1, 3, 3);

                for (byte i = 0; i < containers.Count; ++i)
                {
                    rects[i+1] = new Vector4(0, 0, containers[i].size.x * 1024, containers[i].size.y * 1024);
                    indices[i+1] = i;
                }

                var boundsSize = RectPacking.Pack(rects, indices, 1, containers.Count, 0);

                // normalize rect coordinates:
                float size = Mathf.Max(boundsSize.x, boundsSize.y);
                for (byte i = 0; i < containers.Count; ++i)
                {
                    rects[i + 1] /= size;

                    float res = FluxyStorage.minFramebufferSize;
                    rects[i + 1].x = Mathf.FloorToInt(rects[i + 1].x * res) / res;
                    rects[i + 1].y = Mathf.FloorToInt(rects[i + 1].y * res) / res;
                    rects[i + 1].z = Mathf.FloorToInt(rects[i + 1].z * res) / res;
                    rects[i + 1].w = Mathf.FloorToInt(rects[i + 1].w * res) / res;
                }

                Shader.SetGlobalVectorArray("_TileData", rects);
                tilesDirty = false;
            }
        }

        private void UpdateContainerTransforms(FluxyStorage.Framebuffer fb)
        {
            for (byte i = 0; i < containers.Count; ++i)
            {
                byte tile = i;
                tile += 1;
                byte c = indices[tile];

                //containers[c].UpdateTransform();
                containers[c].UpdateMaterial(tile, fb);
            }
        }

        private void UpdateContainers(FluxyStorage.Framebuffer fb, float deltaTime)
        {
            if (fb == null)
                return;

            for (byte i = 0; i < containers.Count; ++i)
            {
                byte tile = i;
                tile += 1;
                byte c = indices[tile];

                simulationMaterial.SetInt("_TileIndex", tile);
                Graphics.Blit(null, fb.tileID, simulationMaterial, 8);

                turbulence[tile] = containers[c].turbulence;
                adhesion[tile] = containers[c].adhesion;
                surfaceTension[tile] = containers[c].surfaceTension;
                pressure[tile] = containers[c].pressure;
                viscosity[tile] = Mathf.Pow(1 - Mathf.Clamp01(containers[c].viscosity), deltaTime);
                wrapmode[tile] = containers[c].boundaries;
            }

            simulationMaterial.SetFloatArray("_Pressure", pressure);
            simulationMaterial.SetFloatArray("_Viscosity", viscosity);
            simulationMaterial.SetFloatArray("_VortConf", turbulence);
            simulationMaterial.SetFloatArray("_Adhesion", adhesion);
            simulationMaterial.SetFloatArray("_SurfaceTension", surfaceTension);
            simulationMaterial.SetVectorArray("_WrapMode", wrapmode);
        }

        private void Splat(FluxyStorage.Framebuffer fb)
        {
            if (fb == null || simulationMaterial == null)
                return;

            Shader.SetGlobalTexture("_TileID", fb.tileID);

            for (byte i = 0; i < containers.Count; ++i)
            {
                byte tile = i;
                tile += 1;
                byte c = indices[tile];

                // container's target list:
                for (byte j = 0; j < containers[c].targets.Count; ++j)
                    if (containers[c].targets[j] != null)
                        containers[c].targets[j].Splat(containers[c], fb, tile, rects[tile]);
            }
        }
        
        public void UpdateSolver(float deltaTime)
        {
            if (storage != null && deltaTime > 0)
            {
                UpdateTileData();

                UpdateContainerTransforms(framebuffer);

                if (visible && simulationMaterial != null) 
                {

                    Splat(framebuffer);

                    // semi-fixed timestep: if the delta is larger than the timestep, chop it up.
                    int steps = 0;
                    while (deltaTime > 0 && steps++ < maxSteps)
                    {
                        float timestep = Mathf.Min(deltaTime, maxTimestep);;
                        deltaTime -= timestep;

                        UpdateContainers(framebuffer, maxTimestep);

                        SimulationStep(framebuffer, maxTimestep);
                    }
                }
            }
        }

        protected virtual void LateUpdate()
        {
            UpdateSolver(Time.deltaTime);
        }
    }
}