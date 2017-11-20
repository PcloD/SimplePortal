Shader "Custom/PortalShader"
{
	Properties
	{
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _DistortionTex ("Distortion Texture (RG)", 2D) = "white" {}
        _IntensityAndScrolling ("Intensity And Scrolling (Intensity : x, y  Scrolling Speed : z, w)", Vector) = (0.01, 0.01, 0.2, 0.1)
    }

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DistortionTex;
			fixed4 _IntensityAndScrolling;

			struct a2v
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
			};
			
			struct v2f {
				float4 vertex : SV_POSITION;
				float4 grabUV : TEXCOORD0;
			};
			
			v2f vert(a2v v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.grabUV = ComputeGrabScreenPos(o.vertex);

				return o;
			}
			
			half4 frag(v2f i) : SV_Target
			{
				fixed2 uv_distort = i.grabUV + _Time.y * _IntensityAndScrolling.zw;
				fixed4 distort = tex2D(_DistortionTex, uv_distort);

				fixed4 offset = (distort * 2 - 1) * _IntensityAndScrolling.xyxy;
				i.grabUV += offset;


				fixed4 col = tex2Dproj(_MainTex, i.grabUV);
				return col;
			}
			ENDCG
		}
	}
}