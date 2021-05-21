Shader "XHH/AnimMapShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _AnimMap ("AnimMap", 2D) = "white" { }
        _AnimRate ("_AnimRate", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" }
        LOD 100
        Cull off

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }
            
            Cull Back
            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup


            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float2 uv: TEXCOORD0;
                float4 positionOS: POSITION;
                uint instanceID: SV_InstanceID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv: TEXCOORD0;
                float4 positionCS: SV_POSITION;
                float f: TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D(_MainTex);SAMPLER(sampler_MainTex);
            sampler2D _AnimMap;float4 _AnimMap_TexelSize;//x == 1/width


            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                struct AnimInfo
                {
                    float animRate1;
                    float animRate2;
                    float animLerp;
                };
                StructuredBuffer<AnimInfo> _AnimInfo;
            #endif

            // UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
            // UNITY_DEFINE_INSTANCED_PROP(float, _AnimRate1)
            // UNITY_DEFINE_INSTANCED_PROP(float, _AnimRate2)
            // UNITY_DEFINE_INSTANCED_PROP(float, _AnimLerp)
            // UNITY_INSTANCING_BUFFER_END(UnityPerMaterial);
            // float _AnimRate;
            
            void setup()
            {
                
            }

            Varyings vert(Attributes input, uint vid: SV_VertexID)//vid对应的就是
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);


                float animMap_y1 = 0;//UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _AnimRate1);
                float animMap_y2 = 0;//UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _AnimRate2);
                float animLerp = 0;//UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _AnimLerp);

                #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                    AnimInfo animInfo = _AnimInfo[instanceID];
                    animMap_y1 = animInfo.animRate1;
                    animMap_y2 = animInfo.animRate2;
                    animLerp = animInfo.animLerp;
                    

                #endif

                

                float animMap_x = (vid + 0.5) * _AnimMap_TexelSize.x;
                
                float4 pos1 = tex2Dlod(_AnimMap, float4(animMap_x, animMap_y1, 0, 0));
                float4 pos2 = tex2Dlod(_AnimMap, float4(animMap_x, animMap_y2, 0, 0));

                float4 pos = lerp(pos1, pos2, animLerp);

                
                output.uv = input.uv;
                output.positionCS = TransformObjectToHClip(pos);
                output.f = animLerp;
                return output;
            }
            
            float4 frag(Varyings i): SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                // return i.f;
                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                return col;
            }
            ENDHLSL
            
        }
    }
}
