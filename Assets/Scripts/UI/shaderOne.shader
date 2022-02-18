Shader "Custom/shaderOne"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand_1_05(in float2 uv)
            {
                float2 noise = (frac(sin(dot(uv, float2(12.9898, 78.233) * 2.0)) * 43758.5453));
                return abs(noise.x + noise.y) * 0.5;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                // 
                // darkening
                col.rgb = col.rgb - 0.1;
                // 
                // to one color
                //col.r = 0.8 + col.r * 0.2;
                //col.g = 0.5 + col.g * 0.2;
                //col.b = 0.3 + col.b * 0.2;
                // 
                // photo filter
                //col.rgb = col.rgb * col.rgb;
                col.rgb = atan(col.rgb - 0.6) + 0.6;
                // 
                // cell noise
                //float2 cuv = float2(asfloat(round(i.uv.x * 80.0)) % 80.0, asfloat(round(i.uv.y * 45.0)) % 45.0);
                //col.rgb = col.rgb + rand_1_05(cuv * _Time) * 0.1;
                // 
                // repeating fade
                //col.r = col.r + col.r * sin(_Time * 20) * 0.3 - 0.1;
                //col.g = col.g + col.g * sin(_Time * 20) * 0.3 - 0.1;
                //col.b = col.b + col.b * sin(_Time * 20) * 0.3 - 0.1;
                return col;
            }
            ENDCG
        }
    }
}
