Shader "SlinDev/Planet"
{
    Properties
    {
        _MainTex("Texture (RGB)", 2D) = "black" {}
        _Color("Color", Color) = (0, 0.7, 0.8, 1)
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _AtmoColor("Atmosphere Color", Color) = (0.5, 0.5, 1.0, 1)
        _Size("Size", Float) = 0.05
        _Falloff("Falloff", Float) = 3
        _FalloffPlanet("Falloff Planet", Float) = 4
        _Transparency("Transparency", Float) = 12
        _TransparencyPlanet("Transparency Planet", Float) = 0.3
    }
   
	SubShader
    {
        Pass
        {
            Name "PlanetBase"
            Tags {"LightMode" = "Always"}
            Cull Back
           
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
               
                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
               
                #include "UnityCG.cginc"
               
                uniform sampler2D _MainTex;
 //              uniform sampler2D _BumpMap;
                uniform float4 _MainTex_ST;
                uniform float4 _Color;
                uniform float4 _AtmoColor;
                uniform float _FalloffPlanet;
                uniform float _TransparencyPlanet;
               

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 normal : TEXCOORD0;
                    float3 worldvertpos : TEXCOORD1;
                    float2 texcoord : TEXCOORD2;
                };

                v2f vert(appdata_base v)
                {
                    v2f o;
                   
                    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                    o.normal = v.normal;
                    o.worldvertpos = mul(_Object2World, v.vertex).xyz;
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                   
                    return o;
                }
              
                float4 frag(v2f i) : COLOR
                {
                    i.normal = normalize(i.normal);
                    float3 viewdir = normalize(_WorldSpaceCameraPos-i.worldvertpos);
                   
                    float4 atmo = _AtmoColor;
                    atmo.a = pow(1.0-saturate(dot(viewdir, i.normal)), _FalloffPlanet);
                    atmo.a *= _TransparencyPlanet*_Color;
               
                    float4 color = tex2D(_MainTex, i.texcoord)*_Color;
                    color.rgb = lerp(color.rgb, atmo.rgb, atmo.a);
               
                    return color*dot(normalize(i.worldvertpos-_WorldSpaceLightPos0), i.normal);
                }
            ENDCG
        }
   
        Pass
        {
            Name "AtmosphereBase"
            Tags {"LightMode" = "Always"}
            Cull Front
            Blend SrcAlpha One
           
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
               
                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
               
                #include "UnityCG.cginc"
               
                uniform float4 _Color;
                uniform float4 _AtmoColor;
                uniform float _Size;
                uniform float _Falloff;
                uniform float _Transparency;
               
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float3 normal : TEXCOORD0;
                    float3 worldvertpos : TEXCOORD1;
                };

                v2f vert(appdata_base v)
                {
                    v2f o;
                   
                    v.vertex.xyz += v.normal*_Size;
                    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                    o.normal = v.normal;
                    o.worldvertpos = mul(_Object2World, v.vertex);
                   
                    return o;
                }
              
                float4 frag(v2f i) : COLOR
                {
                    i.normal = normalize(i.normal);
                    float3 viewdir = normalize(i.worldvertpos-_WorldSpaceCameraPos);
                   
                    float4 color = _AtmoColor;
                    color.a = pow(saturate(dot(viewdir, i.normal)), _Falloff);
                    color.a *= _Transparency*_Color*dot(normalize(i.worldvertpos-_WorldSpaceLightPos0), i.normal);
                    return color;
                }
            ENDCG
        }
        
    }
   SubShader {
						Tags { "RenderType"="Self Illuminated" }
						LOD 600
					
					CGPROGRAM
					#pragma surface surf Lambert
					
					sampler2D _MainTex;
					sampler2D _BumpMap;
					fixed4 _Color;
					
					struct Input {
						float2 uv_MainTex;
						float2 uv_BumpMap;
					};
					
					void surf (Input IN, inout SurfaceOutput o) {
						fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
						o.Albedo = c.rgb;
						o.Alpha = c.a;
						o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
					}
					ENDCG  
				}
    FallBack "Diffuse"
}