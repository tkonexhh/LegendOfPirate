Shader "URP/Character"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" { }
        _BaseColor ("BaseColor", color) = (1, 1, 1, 1)
        _ShadowMid ("Shadow Mid", Range(0, 1)) = 0.5
        _ShadowSmooth ("Shadow Smooth", Range(0, 1)) = 0.2
        _ShadowColor ("Shadow Color", color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" }

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }
            
            Cull Back
            
            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"
            // #include "../HLSL/CharacterBasePass.HLSL"
            
            

            CBUFFER_START(UnityPerMaterial)

            float4 _ShadowColor;
            float _ShadowMid, _ShadowSmooth;
            CBUFFER_END

            TEXTURE2D(_MainTex);SAMPLER(sampler_MainTex);
            
            struct Attributes
            {
                float4 positionOS: POSITION;
                float2 uv: TEXCOORD0;
                float3 normalOS: NORMAL;
            };


            struct Varyings
            {
                float4 positionCS: SV_POSITION;
                float3 positionWS: TEXCOORD1;
                float2 uv: TEXCOORD0;
                float3 normalWS: NORMAL;
            };


            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.uv = input.uv;

                return output;
            }


            float4 frag(Varyings input): SV_Target
            {
                // input.normalWS = normalize(cross(ddy(input.positionWS), ddx(input.positionWS)));
                Light light = GetMainLight();
                
                float3 lightColor = light.color;
                float3 N = normalize(input.normalWS);
                float3 L = normalize(light.direction);
                float NdotL = saturate(dot(N, L));
                
                
                half shadowStep = smoothstep(_ShadowMid - _ShadowSmooth, _ShadowMid + _ShadowSmooth, NdotL);
                // return shadowStep;
                // return NdotL;
                
                half4 var_MainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                float4 finalRGB = lerp(var_MainTex, var_MainTex * _ShadowColor, 1 - shadowStep);

                
                return finalRGB;
            }
            
            ENDHLSL
            
        }
    }
    FallBack "Diffuse"
}
