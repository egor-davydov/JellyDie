﻿using System.Linq;
using Code.StaticData;
using Fluxy;
using UnityEngine;
using Zenject;

namespace Code.Services
{
  public class PaintCountCalculationService
  {
    private FluxySolver _fluxySolver;
    private StaticDataService _staticDataService;
    private FluxyContainer[] _fluxyContainers;

    private static readonly Color ClearColor = new(0, 0, 0, 0);

    [Inject]
    public void Construct(StaticDataService staticDataService) =>
      _staticDataService = staticDataService;

    public void Initialize(FluxySolver fluxySolver, FluxyContainer[] fluxyContainers)
    {
      _fluxySolver = fluxySolver;
      _fluxyContainers = fluxyContainers;
    }

    public bool HasPaintOnAllMeshes()
    {
      Texture2D fluxyTexture = ConvertToTexture2D(_fluxySolver.framebuffer.stateA);
      foreach (FluxyContainer fluxyContainer in _fluxyContainers)
      {
        JellyConfig jellyConfig = _staticDataService.ForJellies().JellyConfigs.First(config => config.Mesh == fluxyContainer.customMesh);
        if (!HasPaintOnMesh(fluxyTexture, jellyConfig, _fluxySolver.GetContainerUVRect(fluxyContainer)))
          return false;
      }

      return true;
    }

    private bool HasPaintOnMesh(Texture2D fluxyTexture, JellyConfig jellyConfig, Vector4 containerUVRect)
    {
      Texture2D maskTexture = jellyConfig.MaskTexture;

      foreach (Vector2 uvCoordinates in jellyConfig.Mesh.uv)
      {
        float uvCoordinatesX = containerUVRect.x + uvCoordinates.x * containerUVRect.z;
        float uvCoordinatesY = containerUVRect.y + uvCoordinates.y * containerUVRect.w;
        int x = (int)(uvCoordinatesX * fluxyTexture.width);
        int y = (int)(uvCoordinatesY * fluxyTexture.height);
        if (maskTexture.GetPixel((int)(uvCoordinates.x * maskTexture.width), (int)(uvCoordinates.y * maskTexture.height)).r != 0)
          continue;

        Color pixelColor = fluxyTexture.GetPixel(x, y);
        if (pixelColor != ClearColor)
          return true;
      }

      return false;
    }

    public float CalculatePaintPercentage()
    {
      Texture2D fluxyTexture = ConvertToTexture2D(_fluxySolver.framebuffer.stateA);
      int paintedPixelsCount = 0;
      int countPixelsShouldPaint = 0;
      foreach (FluxyContainer fluxyContainer in _fluxyContainers)
      {
        JellyConfig jellyConfig = _staticDataService.ForJellies().JellyConfigs.First(config => config.Mesh == fluxyContainer.customMesh);
        Vector2Int pixelsCount = CalculateJellyPaintedPixelsCount(fluxyTexture, jellyConfig, _fluxySolver.GetContainerUVRect(fluxyContainer));
        paintedPixelsCount += pixelsCount.x;
        countPixelsShouldPaint += pixelsCount.y;
      }

      return ((float)paintedPixelsCount / countPixelsShouldPaint * 100);
    }

    private Vector2Int CalculateJellyPaintedPixelsCount(Texture2D fluxyTexture, JellyConfig jellyConfig, Vector4 containerUVRect)
    {
      int paintedPixelsCount = 0;
      int shouldPaintedPixelsCount = jellyConfig.Mesh.uv.Length;
      Texture2D maskTexture = jellyConfig.MaskTexture;
      foreach (Vector2 uvCoordinates in jellyConfig.Mesh.uv)
      {
        float uvCoordinatesX = containerUVRect.x + uvCoordinates.x * containerUVRect.z;
        float uvCoordinatesY = containerUVRect.y + uvCoordinates.y * containerUVRect.w;
        int x = (int)(uvCoordinatesX * fluxyTexture.width);
        int y = (int)(uvCoordinatesY * fluxyTexture.height);
        if (maskTexture.GetPixel((int)(uvCoordinates.x * maskTexture.width), (int)(uvCoordinates.y * maskTexture.height)).r != 0)
        {
          shouldPaintedPixelsCount--;
          continue;
        }

        Color pixelColor = fluxyTexture.GetPixel(x, y);
        if (pixelColor != ClearColor && pixelColor.a > 0.8f)
        {
          // if (jellyConfig.Mesh.name == "topM")
          //   Debug.Log($"pixelColor= {pixelColor}");
          if (VectorsAlmostSame(Abs(pixelColor - jellyConfig.TargetColor), Vector4.one * 0.27f))
            paintedPixelsCount++;
        }
      }

      Debug.Log($"name={jellyConfig.Mesh.name}; percentage= {(float)paintedPixelsCount/shouldPaintedPixelsCount * 100}; painted={paintedPixelsCount};shouldPainted={shouldPaintedPixelsCount};");

      return new Vector2Int(paintedPixelsCount, shouldPaintedPixelsCount);
    }

    private bool VectorsAlmostSame(Vector4 target, Vector4 epsilon)
    {
      return target.x < epsilon.x && target.y < epsilon.y && target.z < epsilon.z && target.w < epsilon.w;
    }

    private Vector4 Abs(Color delta)
    {
      var absColor = new Vector4(Mathf.Abs(delta.r), Mathf.Abs(delta.g), Mathf.Abs(delta.b), Mathf.Abs(delta.a));
      return absColor;
    }

    private Texture2D ConvertToTexture2D(RenderTexture rTex)
    {
      Texture2D tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
      RenderTexture.active = rTex;
      tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
      tex.Apply();
      return tex;
    }
  }
}