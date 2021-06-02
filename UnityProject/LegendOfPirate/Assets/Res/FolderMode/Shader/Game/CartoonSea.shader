Shader "URP/CartoonSea"
{
    Properties
    {
		_MainColor("Main Color", Color) = (1, 1, 1, .5) 
		_MainTex ("Main Texture", 2D) = "white" {}
		//_NoiseTex("Wave Noise", 2D) = "white" {}
		//_Speed("Wave Speed", Range(0,1)) = 0.5
		_WaveScale("Wave Scale", Range(0,2)) = 0.1
		_ReflectColorAdjust("Reflect Color Adujst", Range(0.1, 10)) = 5
		_ColorBlendAdjust("Color Blend Adujst", Range(0.1, 10)) = 5
		_Foam("Foamline Thickness", Range(0,3)) = 0.5
        _FoamColor("Foam Color", Color) = (1, 1, 1, .5) 
        _DistortStrength("Distort strength", Range(0,1)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "Queue"="Transparent+0"
        }
        
        Pass
        {
            Name "Pass"
            Tags 
            { 
                
            }
            
            Blend SrcAlpha OneMinusSrcAlpha
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
            half4 _MainColor;
            float _WaveScale;
            float _ColorBlendAdjust;
            float _ReflectColorAdjust;
            float _Foam;
            half4 _FoamColor;
            //float _DistortStrength;
            CBUFFER_END
            
            Texture2D _MainTex;
            float4 _MainTex_ST;
            
            Texture2D _NoiseTex;
            float4 _NoiseTex_ST;

			// 贴图采样器
            SamplerState smp_Point_Repeat;

            // 顶点着色器的输入
            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv :TEXCOORD0;
				float3 normalOS : NORMAL;
            };
            
            // 顶点着色器的输出
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv :TEXCOORD0;
                float4 screenPos : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
            };

			// 将采样的深度贴图中的深度值转换为
            float GetLinearEyeDepth(float2 UV)
            {
                float depth = LinearEyeDepth(SampleSceneDepth(UV.xy), _ZBufferParams);
                return depth;
            }

		    float4 cosine_gradient(float x,  float4 phase, float4 amp, float4 freq, float4 offset){
				const float TAU = 2. * 3.14159265;
  				phase *= TAU;
  				x *= TAU;

  				return float4(
    				offset.r + amp.r * 0.5 * cos(x * freq.r + phase.r) + 0.5,
    				offset.g + amp.g * 0.5 * cos(x * freq.g + phase.g) + 0.5,
    				offset.b + amp.b * 0.5 * cos(x * freq.b + phase.b) + 0.5,
    				offset.a + amp.a * 0.5 * cos(x * freq.a + phase.a) + 0.5
  				);
			}

			float3 toRGB(float3 grad){
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
				float height = noise(pos.xz * _WaveScale,0);
				height *= anisotropy ;
				normal = normalize(
					cross ( 
						float3(0,ddy(height),1),
						float3(1,ddx(height),0)
					)
				);
				return normal;
			}

            // 顶点着色器
            Varyings vert(Attributes v)
            {
                Varyings o = (Varyings)0;
                // 采样噪声贴图
                //float4 tex = SAMPLE_TEXTURE2D_LOD(_NoiseTex, smp_Point_Repeat, v.uv, 0);
                // 修改Mesh顶点坐标Y值
				//v.positionOS.y += sin(_Time.z * _Speed + (v.positionOS.x * v.positionOS.z * _Amount * tex)) * _Height;
				// 计算裁剪空间位置
                o.positionCS = TransformObjectToHClip(v.positionOS);
				// 计算世界空间位置
				o.worldPos = TransformObjectToWorld(v.positionOS);
				// 计算世界空间法线
				o.worldNormal = TransformObjectToWorldNormal(v.normalOS);
                // 计算UV值
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // 计算屏幕空间位置(此处还没有进行齐次除法)
				o.screenPos = ComputeScreenPos(o.positionCS);

                return o;
            }

            // 片段着色器
            half4 frag(Varyings i) : SV_TARGET 
            {    
				// 采样主贴图
				half4 col = SAMPLE_TEXTURE2D(_MainTex, smp_Point_Repeat, i.uv) * _MainColor;
                // 获取深度纹理中的深度值
				half depth = GetLinearEyeDepth(i.screenPos.xy/i.screenPos.w);
                // 通过深度图中的深度值和像素深度值的差值，实现foam效果
				half4 foamLine = 1 - saturate(_Foam * (depth - i.screenPos.w));
				col += foamLine * _FoamColor;
				
                // 采样Noise贴图
                //half4 noise = SAMPLE_TEXTURE2D(_NoiseTex, smp_Point_Repeat, i.uv);
                //float2 distort = _DistortStrength * (noise.xy);
                // 使用Noise的值对SceneColor进行扰动，达到扭曲的效果
                //half3 sceneColor = SampleSceneColor(i.screenPos.xy/i.screenPos.w +  distort);
                
				float volmeZ = saturate( (depth - i.screenPos.w)/30.0f);

				const float4 phases = float4(0.28, 0.50, 0.07, 0.);
				const float4 amplitudes = float4(4.02, 0.34, 0.65, 0.);
				const float4 frequencies = float4(0.00, 0.48, 0.08, 0.);
				const float4 offsets = float4(0.00, 0.16, 0.00, 0.);

				float4 cos_grad = cosine_gradient(1-volmeZ, phases, amplitudes, frequencies, offsets);
  				cos_grad = clamp(cos_grad, 0., 1.);
  				col.rgb = toRGB(cos_grad);
					
				half3 worldViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

				float3 v = i.worldPos - _WorldSpaceCameraPos;
				float anisotropy = saturate(1/(ddy(length ( v.xz )))/5);
				float3 swelledNormal = swell(i.worldNormal , i.worldPos , anisotropy);

				// relfection color
                half3 reflDir = reflect(-worldViewDir, swelledNormal);
				float4 reflectionColor = _ReflectColorAdjust * SAMPLE_TEXTURECUBE_LOD(unity_SpecCube0, samplerunity_SpecCube0,reflDir, 0);
				
				// speclar
				Light mainLight = GetMainLight();
				float spe = pow( saturate(dot( reflDir, normalize(mainLight.direction))),10);
				float3 lightColor = float3(1,0,0);
				reflectionColor += 100.4 * half4((spe * lightColor).xxxx);

				// fresnel reflect 
				float f0 = 0.02;
    			float vReflect = f0 + (1-f0) * pow((1 - dot(worldViewDir,swelledNormal)),5);
				vReflect = saturate(vReflect * _ColorBlendAdjust);

				col = lerp(col , reflectionColor , vReflect);

				float alpha = saturate(volmeZ);
				
  				col.a = alpha*1;

                return col;
            }
            
            ENDHLSL
        }
    }
    FallBack "Hidden/Shader Graph/FallbackError"
}