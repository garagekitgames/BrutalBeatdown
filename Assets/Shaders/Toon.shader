﻿Shader "Custom/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
         _ShadowClip("ShadowClip", Range(0.01, 1)) = 0.01
		_MainTex("Main Texture", 2D) = "white" {}	
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32
        _SpecularClip("SpecularClip", Range(0.01, 1)) = 0.01

       

        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimClip("Rim Clip", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1

	}
	SubShader
	{
		Pass
		{
        Tags
        {
        //"RenderType" =  "Opaque"
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
            // Add below the existing #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"


			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
                float3 normal : NORMAL;

			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;
            // Matching variable, add above the fragment shader.
            float4 _AmbientColor;
            float _Glossiness;
            float4 _SpecularColor;
            float _ShadowClip;
            float _SpecularClip;

            float4 _RimColor;
            float _RimAmount;
            float _RimClip;
            float _RimThreshold;

			float4 frag (v2f i) : SV_Target
			{
                // Add to the v2f struct.
                

                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                //float lightIntensity = NdotL > 0 ? 1 : 0;
                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, _ShadowClip, NdotL * shadow);
                // Add below the lightIntensity declaration.
                float4 light = lightIntensity * _LightColor0;

                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);

                float specularIntensitySmooth = smoothstep(0.005, _SpecularClip, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor; 


                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount -0.01 , _RimAmount + _RimClip, rimIntensity);
                float4 rim = rimIntensity * _RimColor;


				float4 sample = tex2D(_MainTex, i.uv);

				return _Color * sample * (_AmbientColor + light + specular + rim);

			}
			ENDCG
		}
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}