
Shader "Custom/UISketchWithOutline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        //_Color ("Tint", Color) = (1,1,1,1)
		_SketchColor("Sketch Color", color) = (0.5,0.5,0.5,1)
		_OutlineColor("Outline Color", color) = (1,1,1,1)
		_OutlineWidth("Outline Width", Float) = 1
		_Alpha("Alpha", Float) = 0.6

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[Toggle(USE_GRAY)] _UseGray ("Use Gray", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
			#pragma multi_compile __ USE_GRAY

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                //fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                //float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            //fixed4 _Color;
            float4 _ClipRect;
			fixed4 _SketchColor;
			fixed4 _OutlineColor;
			float _OutlineWidth;
			float2 _MainTex_TexelSize;
			float _Alpha;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                //OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(v.vertex);

                OUT.texcoord = v.texcoord;

                //OUT.color = v.color * _Color;
                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                //half4 color = tex2D(_MainTex, IN.texcoord) * IN.color;

				float4 color = _SketchColor;
				// 采样周围4个点
				float2 up_uv = IN.texcoord + float2(0, 1) * _OutlineWidth * _MainTex_TexelSize.xy;
				float2 down_uv = IN.texcoord + float2(0, -1) * _OutlineWidth * _MainTex_TexelSize.xy;
				float2 left_uv = IN.texcoord + float2(-1, 0) * _OutlineWidth * _MainTex_TexelSize.xy;
				float2 right_uv = IN.texcoord + float2(1, 0) * _OutlineWidth * _MainTex_TexelSize.xy;
				// 如果有一个点透明度为0 说明是边缘
				float w = tex2D(_MainTex, up_uv).a * tex2D(_MainTex, down_uv).a * tex2D(_MainTex, left_uv).a * tex2D(_MainTex, right_uv).a;

				color.rgb = lerp(_OutlineColor, color.rgb, w);
				float alpha1 = max(tex2D(_MainTex, up_uv).a, tex2D(_MainTex, down_uv).a);
				float alpha2 = max(tex2D(_MainTex, left_uv).a , tex2D(_MainTex, right_uv).a);
				color.a = max(alpha1, alpha2) * _Alpha;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
        ENDCG
        }
    }
}
