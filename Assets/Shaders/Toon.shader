Shader "MyShaders/Toon" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Ramp("Ramp", 2D) = "white" {}
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
#pragma surface surf Ramp

		sampler2D _Ramp;

		half4 LightingRamp(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = max(0.0, dot(s.Normal, lightDir));
			half diff = NdotL * 0.5 + 0.5;
			half3 ramp = tex2D(_Ramp, float2(diff, diff)).rgb;
			half4 c;
			/*if (_WorldSpaceLightPos0.w == 0.0)
			{
				c.rgb = s.Albedo * _LightColor0.rgb * ramp;
			}
			else
			{
				c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
			}*/
			//c.rgb = s.Albedo * _LightColor0.rgb;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * atten;
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			//o.Albedo = 0;
		}
		ENDCG
	}
	Fallback "Diffuse"
}