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

            float2 fishEye(float2 uv, float i)
            {
                uv -= float2(0.5,0.5);
                uv = uv * 1.2 * (0.833 + i * pow(uv.x, 2) * pow(uv.y, 2));
                uv += float2(0.5,0.5);
                return uv;
            }

			fixed4 frag (v2f i) : SV_Target
			{
                i.uv = fishEye(i.uv, 0.3);
                half2 x = 1 - tex2D(_MainTex, i.uv);
                fixed4 p0 = tex2D(_PaletteTex, x);
                fixed4 p1 = tex2D(_Palette2Tex, x);
                p0 = lerp(p0, p1, _Mixing);
			    p0 *= (1 - 1.1 * pow(i.uv.y - 0.5, 2)) * (1 - 1.1 * pow(i.uv.x - 0.5, 2));
                p0 *= (52 + (i.uv.y * 10 + _Time.x * 4) % 1) / (53);
                return p0;
			}
			ENDCG
		}
	}
}
