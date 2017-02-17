Shader "Hidden/GhostingEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		 _BTex ("Output Render Texture", 2D) = "black" {}
        _Intensivity ("_Intensivity", Range (0,1)) = 0.5
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

            uniform sampler2D _MainTex;
            uniform sampler2D _BTex;
            uniform float _Intensivity;

            float4 frag(v2f i) : COLOR{
                float4 c1 = tex2D(_MainTex, i.uv);
                //DRX - fix
                //i.uv.y = 1 - i.uv.y;
                float4 c2 = tex2D(_BTex, i.uv);
                float4 c3 = lerp(c1, c2, _Intensivity);
                return c3;
            }
            ENDCG
		}
	}
}
