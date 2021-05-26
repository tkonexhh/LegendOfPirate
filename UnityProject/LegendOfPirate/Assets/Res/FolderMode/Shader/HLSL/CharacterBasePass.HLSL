#ifndef CHARRACTER_BASE_PASS_INCLUDED
    #define CHARRACTER_BASE_PASS_INCLUDED


    CBUFFER_START(UnityPerMaterial)
    float4 _BaseColor, _ShadowColor;

    float _ShadowMid, _ShadowSmooth;
    float _MainLightIgnoreCelShade;

    CBUFFER_END

    sampler2D _MainTex;
    
    struct Attributes
    {
        float4 positionOS: POSITION;
        float2 uv: TEXCOORD0;
        float3 normalOS: NORMAL;
        float4 color: COLOR;
    };


    struct Varyings
    {
        float4 positionCS: SV_POSITION;
        float3 positionWS: TEXCOORD1;
        float2 uv: TEXCOORD0;
        float4 color: COLOR;
        float3 normalWS: NORMAL;
    };

    struct ToonSurfaceData
    {
        half3 albedo;
        half alpha;
    };

    struct LightingData
    {
        float3 normalWS;
        float3 viewWS;
    };

    Varyings vert(Attributes input)
    {
        Varyings output;
        output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
        output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
        output.normalWS = TransformObjectToWorldNormal(input.normalOS);
        output.uv = input.uv;
        output.color = input.color;

        return output;
    }


    ///////////
    half4 GetFinalBaseColor(Varyings input)
    {
        return tex2D(_MainTex, input.uv) * _BaseColor;
    }

    ToonSurfaceData InitSurfaceData(Varyings input)
    {
        ToonSurfaceData output;
        float4 baseColor = GetFinalBaseColor(input);
        output.albedo = baseColor.rgb;
        output.alpha = baseColor.a;
        return output;
    }

    LightingData InitLightintData(Varyings input)
    {
        LightingData output;
        output.normalWS = normalize(input.normalWS);
        output.viewWS = normalize(_WorldSpaceCameraPos.xyz - input.positionWS.xyz);
        return output;
    }

    half3 ShadeGI(float3 normalWS)
    {
        real4 SHCoefficients[7];
        SHCoefficients[0] = unity_SHAr;
        SHCoefficients[1] = unity_SHAg;
        SHCoefficients[2] = unity_SHAb;
        SHCoefficients[3] = unity_SHBr;
        SHCoefficients[4] = unity_SHBg;
        SHCoefficients[5] = unity_SHBb;
        SHCoefficients[6] = unity_SHC;
        float3 Color = SampleSH9(SHCoefficients, normalWS);
        return Color;
    }

    half3 ShadeLight(ToonSurfaceData surfaceData, LightingData lightingData, Light light)
    {
        half3 N = lightingData.normalWS;
        half3 L = light.direction;
        half3 V = lightingData.viewWS;

        half NdotL = dot(N, L);
        half lightAttenutaion = 1;
        //根据NdotL 柔化 阴影边缘
        half celShadeResult = smoothstep(_ShadowMid - _ShadowSmooth, _ShadowMid + _ShadowSmooth, NdotL);
        lightAttenutaion *= lerp(celShadeResult, 1, _MainLightIgnoreCelShade);


        half f = 1.0 - saturate(dot(N, V));
        
        return lightAttenutaion * light.color ;
    }

    half3 ShadeMainlight(ToonSurfaceData surfaceData, LightingData lightingData, Light light)
    {
        return ShadeLight(surfaceData, lightingData, light);
    }

    half3 ShadeLights(ToonSurfaceData surfaceData, LightingData lightingData)
    {
        half3 indirectResult = ShadeGI(lightingData.normalWS);
        // return indirectResult;
        Light mainLight = GetMainLight();
        half3 mainLightResult = ShadeMainlight(surfaceData, lightingData, mainLight);
        return indirectResult + mainLightResult;
    }


    float4 frag(Varyings input): SV_Target
    {
        // return input.color;
        ToonSurfaceData surfaceData = InitSurfaceData(input);
        LightingData lightingData = InitLightintData(input);

        half3 color = surfaceData.albedo * ShadeLights(surfaceData, lightingData);
        return float4(color, surfaceData.alpha);
    }
    

#endif