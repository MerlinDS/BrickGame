Shader "Hidden/PaleteSwapping"
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
				return o;
			}

            float _Intensivity;
            half4x4 _ColorMatrix;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
                fixed x = tex2D(_MainTex, i.uv).x;
                x = ( 1 - x ) * 3;//Ivertin color and get index of color in matrix
                fixed t = _Intensivity * i.uv.y; // get blackout intensivity
                float4 bl = _ColorMatrix[0]; // blackout color
                float4 col = _ColorMatrix[x]; // main color
                return (1 - t) * col + t * bl;//lerp(col, bl, t);
			}
			ENDCG
		}
	}
}
