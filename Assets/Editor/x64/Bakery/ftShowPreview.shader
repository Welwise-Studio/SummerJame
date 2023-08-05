Shader "Hidden/ftShowPreview"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader {

        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert noinstancing noshadow noforwardadd noambient

        sampler2D _ftPreviewTexture;
        float4x4 _ftPreviewViewProj;

        struct Input
        {
            float4 projUV;
        };

        struct vinput
        {
            float4 vertex : POSITION;

            // needed for Unity
            float2 texcoord1 : TEXCOORD1;
            float2 texcoord2 : TEXCOORD2;
            float3 normal : NORMAL0;
            float2 texcoord : TEXCOORD0;
            float4 tangent : TANGENT;
        };

        void vert (inout vinput v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);

            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
            o.projUV = mul(_ftPreviewViewProj, worldPos);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = 0;
            o.Smoothness = 0;
            o.Occlusion = 0;
            o.Metallic = 1;

            float4 color = 0;
            float2 uv = IN.projUV.xy / IN.projUV.w;
            if (uv.x < -1 || uv.x > 1 || uv.y < -1 || uv.y > 1)
            {
                color = 0;
            }
            else
            {
                color = tex2D(_ftPreviewTexture, uv * 0.5 + 0.5);
            }

            o.Emission = color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

