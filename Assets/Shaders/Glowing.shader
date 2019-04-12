  Shader "Custom/Glowing" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
		_RimColor("Rim Color", Color) =(1,1,1,1)
		_RimPower("Rim Power", Range(1.0, 6.0)) = 3.0


    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
      CGPROGRAM
      #pragma surface surf Lambert
      
      struct Input {
          float2 uv_MainTex;
		  float3 viewDir;
      };
      
		sampler2D _MainTex;
		float4 _RimColor;
		float _RimPower;
      
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Alpha = pow(rim, _RimPower);
      }
      ENDCG
    }
    Fallback "Diffuse"
  }

/*
Shader "Custom/Glowing"
{
    Properties
    {
		_MainText("Base (RGB)", 2D) = "white"{}
		_RimColor("Rim Color", Color) =(1,1,1,1)
		_RimPower("Rim Power", Range(1.0, 6.0)) = 3.0
    }

    SubShader
    {

	  Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert

            struct Input
            {
				float2 uv_MainTex;
				float3 viewDir;
            };

            sampler2D _MainTex;
			float4 _RimColor;
			float _RimPower;

			void surf(Input IN, inout SurfaceOutput o)
			{
				o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				o.Alpha = _RimColor.rgb*pow(rim, _RimPower);
			}

            ENDCG
    }
	Fallback "Diffuese"
}
*/