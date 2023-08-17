﻿Shader "Obi/DistanceFieldSlice"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct MeshData
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Interpolators
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			Interpolators vert (MeshData v)
			{
				Interpolators o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			half4 frag (Interpolators i) : SV_Target
			{
				half dist = tex2D(_MainTex, i.uv).x-0.5; // signed distance
				half adist = abs(dist);					 // absolute distance
				half isofunc = sin(adist*40);			 // isolines function

				// antialiased isolines:
				half change = fwidth(isofunc);
				half isolines = smoothstep(-change,change,isofunc);

				// antialiazed sign:
				change = fwidth(dist);
				half sign = smoothstep(-change,change,dist);
				
				half final = (1-adist) * (0.85+isolines*0.15);

				return half4(final,final*sign,final*sign,1);
			}
			ENDCG
		}
	}
}
