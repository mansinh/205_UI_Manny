Shader "Unlit/RadialBarShader"
{

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Percentage("Percentage", Float) = 0
		_InnerRadius("InnerRadius", Float) = 0.2
		_Shadow("Shadow", Color) = (1,1,1,1)
		_Color("Color", Color) = (1,1,1,1)
		_BackgroundColor("Background Color", Vector) = (0,0,0,1)
		_BorderWidth("Border Width", Float) = 0.05
		_Glow("Glow", Float) = 0

	}

		SubShader
		{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			//Cull front
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _InnerRadius;
				float _Percentage;
				float4 _BackgroundColor;
				float4 _Color;
				float4 _Shadow;
				float _BorderWidth;
				float _Glow;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 uv = i.uv;
					float x = uv.x - 0.5;
					float y = uv.y - 0.5;
					fixed4 col = tex2D(_MainTex, i.uv);
					float sqR = x * x + y * y;
					float innerSqR = _InnerRadius * _InnerRadius;
					float sqBorder = _BorderWidth * _BorderWidth;

					

					if (sqR <= innerSqR - sqBorder || sqR > 0.25) {
						col = float4(0, 0, 0, 0);
					}

					
					else {
						float angle = atan2(y, x);
						float angleDiff = angle / 6.28 - (0.5 - _Percentage);
						if (angleDiff >= 0) {
							//col = lerp(_Color, _Shadow, (1 - angleDiff));
							col = _Color;
							float glow = lerp(0, _Glow*2, max(1-angleDiff*10,0 ));
							col = lerp(col, float4(1, 1, 1, 1), glow);
							col = lerp(col, _Shadow, pow(1-(sqR-innerSqR) / (0.25- innerSqR),4));
						}
						else {
							col = _BackgroundColor;
						}
						if (sqR < innerSqR ) {
							
							col = lerp(col, _BackgroundColor, (innerSqR-sqR)/sqBorder);
						}
						else if (sqR > 0.25 - sqBorder) {

							col = lerp(col, _BackgroundColor, 1-(0.25-sqR) / sqBorder);
						}
					}
					return col;
				}
				ENDCG
			}
		}
}
