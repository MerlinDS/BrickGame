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
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform sampler2D _BTex;
            uniform float _Intensivity;

            float4 frag(v2f_img i) : COLOR{
                float2 uv = i.uv;
                float4 c1 = tex2D(_MainTex, uv);
                uv.y = 1 - uv.y;
                float4 c2 = tex2D(_BTex, uv);
                float4 c3 = lerp(c1, c2, _Intensivity);
                return c3;
            }
            ENDCG
		}
	}
}
