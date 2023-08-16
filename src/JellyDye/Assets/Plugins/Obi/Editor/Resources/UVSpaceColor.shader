﻿Shader "Obi/UVSpaceColor"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct MeshData
			{
				float4 vertex : POSITION;
                fixed4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct Interpolators
			{
				float2 uv : TEXCOORD0;
                fixed4 color    : COLOR;
				float4 vertex : SV_POSITION;
			};
			
			Interpolators vert (MeshData v)
			{
				Interpolators o;
                o.uv = v.uv;
                o.vertex = float4(v.uv.xy,0,1);
				o.vertex = UnityObjectToClipPos(float4(v.uv,0,1));
                o.color = v.color;
				return o;
			}
			
			fixed4 frag (Interpolators i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}
	}
}
