Shader "BrickGame/PaletteSwapping"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _PaletteTex ("Texture of a first color palette", 2D) = "white" {}
        _Palette2Tex ("Texture of a second color palette", 2D) = "white" {}
        _Color("Blackout color", Color) = (0, 0, 0, 1.0)
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

            float _Mixing;
            float _Intensivity;
            fixed4 _Color;
			sampler2D _MainTex;
            sampler2D _PaletteTex;
            sampler2D _Palette2Tex;

			fixed4 frag (v2f i) : SV_Target
			{
                half2 x = 1 - tex2D(_MainTex, i.uv);
                fixed4 p0 = tex2D(_PaletteTex, x);
                fixed4 p1 = tex2D(_Palette2Tex, x);
                return lerp(p0, p1, _Mixing) * ( 1 - _Intensivity * i.uv.y);
			}
			ENDCG
		}
	}
}
