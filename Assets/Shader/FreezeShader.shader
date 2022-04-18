Shader "Unlit/FreezeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Texture", 2D) = "white" {}
        _EmissionColor("IceColor", Color) = (1,1,1,1)
        _TimeParameter("Time", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            float _TimeParameter;
            fixed4 _EmissionColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_noise:TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv_noise : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv_noise=TRANSFORM_TEX(v.uv_noise, _NoiseTex);
                return o;
            }

            void Unity_ColorMask_float(float3 In, float3 MaskColor, float Range, float Fuzziness, out float4 Out)
            {
                float Distance = distance(MaskColor, In);
                Out = saturate(1 - (Distance - Range) / max(Fuzziness, 1e-5));
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 noise = tex2D(_NoiseTex, i.uv_noise);
                fixed time = _TimeParameter;
                fixed4 noiseFreeze = step(noise, time);
                fixed4 edgeMask1 = step(noise, time+0.02);
                fixed4 edgeMask2 = step(noise, time-0.02);
                fixed4 invertNoiseFreeze = noiseFreeze;
                edgeMask2 = abs(1.0-edgeMask2);
                fixed4 emissionColor = invertNoiseFreeze*_EmissionColor;
                float4 edge1;
                float4 edge2;
                Unity_ColorMask_float(edgeMask1, float3(1,1,1), 0, 0, edge1);
                Unity_ColorMask_float(edgeMask2, float3(0,0,0), 0, 0, edge2);
                edge1 = edge1 - edge2;
                edge1*=emissionColor+0.1;
                emissionColor+=edge1;
                fixed4 col = tex2D(_MainTex, i.uv);
                col+=emissionColor;
                return col;
            }
            
            ENDCG
        }
    }
}
