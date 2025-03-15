Shader "Unlit/Healthbar"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _Health ("Health", Range(0,1)) = 1
        _BorderSize ("Border Size", Range(0,0.75)) = 0.4
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha // alpha blending

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Health;
            float _BorderSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float InverseLerp(float a, float b, float v)
            {
                return (v-a)/(b-a);
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample only vertical slice
                // need to set texture wrap mode to clamp inside unity
                // default wrap mode is repeat, which also blends the edges and causes sampling problems
                float4 healthColor = tex2D(_MainTex, float2(_Health, i.uv.y));

                // box sdf
                float2 coords = i.uv - float2(0.5, 0.5);
                coords.x *= 8;
                float2 b = float2(4.0, 0.5);
                float2 d = abs(coords)-b;
                float sdf = length(max(d,0.0)) + min(max(d.x,d.y),0.0);

                float borderSDF = sdf + _BorderSize;
                float pd = fwidth(borderSDF); // screen space partial derivative
                float borderMask = 1 - saturate(borderSDF / pd); // anti-aliasing

                float mask = _Health > i.uv.x;

                // health flashing
                if (_Health < 0.2)
                {
                    float flash = cos(_Time.y * 4) * 0.2;
                    healthColor *= (flash + 0.8);
                }

                return float4(healthColor.xyz * mask * borderMask, 1);
            }
            ENDCG
        }
    }
}
