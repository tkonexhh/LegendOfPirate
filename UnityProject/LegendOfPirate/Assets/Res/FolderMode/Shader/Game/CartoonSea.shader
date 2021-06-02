Shader "V/URP/SimplesUnlitShader"
{
    Properties
    {
		_MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "Queue"="Geometry+0"
        }
        
        Pass
        {
            Name "Pass"
            Tags 
            { 
                
            }
            
            // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            #pragma multi_compile_instancing
            
            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"        
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"
            
            CBUFFER_START(UnityPerMaterial)
            // 常量缓冲区所填充的内容
            half4 _Color;
            CBUFFER_END
            
			Texture2D _MainTex;
            float4 _MainTex_ST;            

            #define smp SamplerState_Point_Repeat
            SAMPLER(smp);

            // 顶点着色器的输入
            struct Attributes
            {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
            
            // 顶点着色器的输出
            struct Varyings
            {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				float4 projPos : TEXCOORD2;
				float3 worldPos : TEXCOORD3;
            };           
            
            // 顶点着色器
            Varyings vert(Attributes v)
            {
                Varyings o = (Varyings)0;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                o.worldNormal = TransformObjectToWorldNormal(v.normal);
                o.worldPos = TransformObjectToWorld(v.vertex);
                // 计算屏幕空间位置(此处还没有进行齐次除法)
                o.projPos = ComputeScreenPos(o.vertex);
                
                //o.projPos = -UnityObjectToViewPos( v.vertex ).z
                return o;
            }
		
			half4 cosine_gradient(float x,  half4 phase, half4 amp, half4 freq, half4 offset){
				const float TAU = 2. * 3.14159265;
  				phase *= TAU;
  				x *= TAU;

  				return half4(
    				offset.r + amp.r * 0.5 * cos(x * freq.r + phase.r) + 0.5,
    				offset.g + amp.g * 0.5 * cos(x * freq.g + phase.g) + 0.5,
    				offset.b + amp.b * 0.5 * cos(x * freq.b + phase.b) + 0.5,
    				offset.a + amp.a * 0.5 * cos(x * freq.a + phase.a) + 0.5
  				);
			}

			half3 toRGB(half3 grad){
  				 return grad.rgb;
			}

			float2 rand(float2 st, int seed)
			{
				float2 s = float2(dot(st, float2(127.1, 311.7)) + seed, dot(st, float2(269.5, 183.3)) + seed);
				return -1 + 2 * frac(sin(s) * 43758.5453123);
			}

			float noise(float2 st, int seed)
			{
				st.y += _Time[1];

				float2 p = floor(st);
				float2 f = frac(st);
 
				float w00 = dot(rand(p, seed), f);
				float w10 = dot(rand(p + float2(1, 0), seed), f - float2(1, 0));
				float w01 = dot(rand(p + float2(0, 1), seed), f - float2(0, 1));
				float w11 = dot(rand(p + float2(1, 1), seed), f - float2(1, 1));
				
				float2 u = f * f * (3 - 2 * f);
 
				return lerp(lerp(w00, w10, u.x), lerp(w01, w11, u.x), u.y);
			}

			float3 swell(float3 normal , float3 pos , float anisotropy){
				float height = noise(pos.xz * 0.1,0);
				height *= anisotropy ;
				normal = normalize(
					cross ( 
						float3(0,ddy(height),1),
						float3(1,ddx(height),0)
					)
				);
				return normal;
			}

			// 采样深度贴图中的深度值
            float GetLinearEyeDepth(float2 UV)
            {
                float depth = LinearEyeDepth(SampleSceneDepth(UV.xy), _ZBufferParams);
                return depth;
            }

            // 片段着色器
            half4 frag(Varyings i) : SV_TARGET 
            {    
				half4 col = _MainTex.Sample(smp,i.uv);

    			float sceneZ = GetLinearEyeDepth(i.projPos.xy/i.projPos.w);
				float partZ = i.projPos.w;
				float volmeZ = saturate( (sceneZ - partZ)/10.0f);

				const half4 phases = half4(0.28, 0.50, 0.07, 0.);
				const half4 amplitudes = half4(4.02, 0.34, 0.65, 0.);
				const half4 frequencies = half4(0.00, 0.48, 0.08, 0.);
				const half4 offsets = half4(0.00, 0.16, 0.00, 0.);

				half4 cos_grad = cosine_gradient(1-volmeZ, phases, amplitudes, frequencies, offsets);
  				cos_grad = clamp(cos_grad, 0., 1.);
  				col.rgb = toRGB(cos_grad);

				half3 worldViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

				float3 v = i.worldPos - _WorldSpaceCameraPos;
				float anisotropy = saturate(1/(ddy(length ( v.xz )))/5);
				float3 swelledNormal = swell(i.worldNormal , i.worldPos , anisotropy);

				// relfection color
                //half3 reflDir = reflect(-worldViewDir, swelledNormal);
				//half4 reflectionColor = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, reflDir, 0);
				/* speclar
				float spe = pow( saturate(dot( reflDir, normalize(_lightPos.xyz))),100);
				float3 lightColor = float3(1,1,1);
				reflectionColor += 0.4 * half4((spe * lightColor).xxxx);
				*/

				// fresnel reflect 
				float f0 = 0.02;
    			float vReflect = f0 + (1-f0) * pow(
					(1 - dot(worldViewDir,swelledNormal)),
				5);
				vReflect = saturate(vReflect * 2.0);

				//col = lerp(col , reflectionColor , vReflect);

				float alpha = saturate(volmeZ);
				
  				col.a = alpha;

return half4(volmeZ,volmeZ,volmeZ,1);
				//return col;
            }
            
            ENDHLSL
        }
    }
    FallBack "Hidden/Shader Graph/FallbackError"
}