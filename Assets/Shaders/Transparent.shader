Shader "Custom/Transparent" {

	Properties{
		_Color("Color", Color) = (1,1,1,1)
	}

		SubShader{
		LOD 200
		Tags{"Queue"="Transparent" "RenderType"="Transparent"}

		CGINCLUDE

		#pragma target 5.0

		#include "UnityCG.cginc"

		fixed4 _Color;

		struct appdata {
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		v2f vert(appdata IN) {
			v2f OUT;

			OUT.pos = mul(UNITY_MATRIX_MVP, IN.vertex);

			OUT.uv = IN.uv;

			return OUT;
		}

		ENDCG

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			fixed4 frag(v2f IN) : SV_Target{
				return _Color;
			}

			ENDCG
		}

	}
}