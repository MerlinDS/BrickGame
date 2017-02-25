Shader "Hidden/PaleteSwapping"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _PaletteTex ("Texture", 2D) = "white" {}
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
			sampler2D _MainTex;
            sampler2D _PaletteTex;

			fixed4 frag (v2f i) : SV_Target
			{
                float p = float2(0, 0);
                fixed4 bl = tex2D(_PaletteTex, p);
                p.x = 1 - tex2D(_MainTex, i.uv).r;
                float4 col = tex2D(_PaletteTex, p);
                return  lerp(col, bl, _Intensivity * i.uv.y);
			}
			ENDCG
		}
	}
}
