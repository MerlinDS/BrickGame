Shader "BrickGame/ShakeScreenEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _Intensivity("Shake intensivityt", Vector) = (1.0, 1.0, 0.0, 0.0)
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
				return o;
			}

            float4 _Intensivity;
            sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
                float r = 2 * frac(sin(dot(_Time.x* 0.01,float2(12.9898,78.233))) * 43758.5453) - 1;
                i.uv += _Intensivity * r;
                return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}
