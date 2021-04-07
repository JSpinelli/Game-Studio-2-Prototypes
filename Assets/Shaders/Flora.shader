// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Flora"
{
	Properties
	{
		_MainColor("Main Color", Color) = (0.3921569,0.3921569,0.3921569,1)
		_SpecularColor("Specular Color", Color) = (0.3921569,0.3921569,0.3921569,1)
		_Shininess("Shininess", Range( 0.01 , 1)) = 0.1
		_GreaterThan("Greater Than", Color) = (0,0,0,0)
		_LessThan("Less Than", Color) = (0,0,0,0)
		_TimeScaleOne("Time Scale One", Float) = 1
		_TimeScaleTwo("Time Scale Two", Float) = 1
		_ThirdColor("Third Color", Color) = (0,0,0,0)
		_Clamp2("Clamp 2", Range( 0 , 1)) = 0
		_TimeScaleOscilationOne("Time Scale Oscilation One", Float) = 0
		_TimeScaleOscilationTwo("Time Scale Oscilation Two", Float) = 0
		_Clamp1("Clamp 1", Range( 0 , 1)) = 0
		_TextureNormal("Texture Normal", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _SpecularColor;
		uniform sampler2D _TextureNormal;
		uniform float4 _TextureNormal_ST;
		uniform float _Shininess;
		uniform float4 _MainColor;
		uniform float _TimeScaleTwo;
		uniform float _TimeScaleOscilationTwo;
		uniform float _Clamp1;
		uniform float4 _ThirdColor;
		uniform float _TimeScaleOne;
		uniform float _TimeScaleOscilationOne;
		uniform float _Clamp2;
		uniform float4 _GreaterThan;
		uniform float4 _LessThan;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float4 temp_output_43_0_g5 = _SpecularColor;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 normalizeResult4_g6 = normalize( ( ase_worldViewDir + ase_worldlightDir ) );
			float2 uv_TextureNormal = i.uv_texcoord * _TextureNormal_ST.xy + _TextureNormal_ST.zw;
			float3 normalizeResult64_g5 = normalize( (WorldNormalVector( i , tex2D( _TextureNormal, uv_TextureNormal ).rgb )) );
			float dotResult19_g5 = dot( normalizeResult4_g6 , normalizeResult64_g5 );
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 temp_output_40_0_g5 = ( ase_lightColor * ase_lightAtten );
			float dotResult14_g5 = dot( normalizeResult64_g5 , ase_worldlightDir );
			UnityGI gi34_g5 = gi;
			float3 diffNorm34_g5 = normalizeResult64_g5;
			gi34_g5 = UnityGI_Base( data, 1, diffNorm34_g5 );
			float3 indirectDiffuse34_g5 = gi34_g5.indirect.diffuse + diffNorm34_g5 * 0.0001;
			float4 temp_output_42_0_g5 = _MainColor;
			float grayscale2 = Luminance(( ( float4( (temp_output_43_0_g5).rgb , 0.0 ) * (temp_output_43_0_g5).a * pow( max( dotResult19_g5 , 0.0 ) , ( _Shininess * 128.0 ) ) * temp_output_40_0_g5 ) + ( ( ( temp_output_40_0_g5 * max( dotResult14_g5 , 0.0 ) ) + float4( indirectDiffuse34_g5 , 0.0 ) ) * float4( (temp_output_42_0_g5).rgb , 0.0 ) ) ).rgb);
			float dotResult4_g4 = dot( float2( 0,0 ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g4 = lerp( 0.0 , 1.0 , frac( ( sin( dotResult4_g4 ) * 43758.55 ) ));
			float mulTime26 = _Time.y * ( lerpResult10_g4 + _TimeScaleTwo );
			float clampResult31 = clamp( sin( ( mulTime26 * 6.28318548202515 * _TimeScaleOscilationTwo ) ) , 0.0 , _Clamp1 );
			float dotResult4_g3 = dot( float2( 0,0 ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g3 = lerp( 0.0 , 1.0 , frac( ( sin( dotResult4_g3 ) * 43758.55 ) ));
			float mulTime18 = _Time.y * ( lerpResult10_g3 + _TimeScaleOne );
			float clampResult33 = clamp( sin( ( mulTime18 * 6.28318548202515 * _TimeScaleOscilationOne ) ) , 0.0 , _Clamp2 );
			float4 ifLocalVar3 = 0;
			if( grayscale2 >= clampResult33 )
				ifLocalVar3 = _GreaterThan;
			else
				ifLocalVar3 = _LessThan;
			float4 ifLocalVar13 = 0;
			if( grayscale2 <= clampResult31 )
				ifLocalVar13 = ifLocalVar3;
			else
				ifLocalVar13 = _ThirdColor;
			c.rgb = ifLocalVar13.rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
201;73;1290;655;2016.652;328.1198;1.549629;True;False
Node;AmplifyShaderEditor.FunctionNode;16;-1777.105,185.0536;Inherit;False;Random Range;-1;;3;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1761.706,335.8534;Inherit;False;Property;_TimeScaleOne;Time Scale One;6;0;Create;True;0;0;0;False;0;False;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1795.717,-465.0148;Inherit;False;Property;_TimeScaleTwo;Time Scale Two;7;0;Create;True;0;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;22;-1811.116,-615.8146;Inherit;False;Random Range;-1;;4;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1543.703,294.2536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1577.714,-506.6146;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;18;-1411.802,355.3534;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;19;-1443.002,437.2532;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1503.419,539.4565;Inherit;False;Property;_TimeScaleOscilationOne;Time Scale Oscilation One;10;0;Create;True;0;0;0;False;0;False;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1236.303,447.6531;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1537.43,-261.4117;Inherit;False;Property;_TimeScaleOscilationTwo;Time Scale Oscilation Two;11;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;-1474.399,-126.3135;Inherit;True;Property;_TextureNormal;Texture Normal;13;0;Create;True;0;0;0;False;0;False;-1;1a3df453886534b47a4f53833e187bea;1a3df453886534b47a4f53833e187bea;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;26;-1445.813,-445.5148;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TauNode;25;-1477.013,-363.615;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-1100.605,-60.84381;Inherit;False;Blinn-Phong Light;0;;5;cf814dba44d007a4e958d2ddd5813da6;0;3;42;COLOR;0,0,0,0;False;52;FLOAT3;0,0,0;False;43;COLOR;0,0,0,0;False;2;COLOR;0;FLOAT;57
Node;AmplifyShaderEditor.SinOpNode;14;-1032.82,272.9571;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1068.128,520.0319;Inherit;False;Property;_Clamp2;Clamp 2;9;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1107.374,96.17713;Inherit;False;Constant;_Float1;Float 1;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1270.314,-353.2151;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;29;-975.8579,-326.1181;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;33;-752.2437,266.1873;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1092.418,-451.9849;Inherit;False;Property;_Clamp1;Clamp 1;12;0;Create;True;0;0;0;False;0;False;0;0.33;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-542.9427,270.8455;Inherit;False;Property;_GreaterThan;Greater Than;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3113208,0.1980448,0.116011,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-541.64,450.1662;Inherit;False;Property;_LessThan;Less Than;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3490566,0.2458028,0.07409222,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCGrayscale;2;-782.2458,-97.74156;Inherit;True;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;31;-631.4962,-302.0896;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;3;-423.5695,-56.80815;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;10;-181.0988,132.8985;Inherit;False;Property;_ThirdColor;Third Color;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4528302,0.2542966,0.1559274,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;13;-215.897,-249.4905;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;148.4272,-281.9181;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;Flora;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;0
WireConnection;17;1;15;0
WireConnection;24;0;22;0
WireConnection;24;1;23;0
WireConnection;18;0;17;0
WireConnection;21;0;18;0
WireConnection;21;1;19;0
WireConnection;21;2;20;0
WireConnection;26;0;24;0
WireConnection;1;52;9;0
WireConnection;14;0;21;0
WireConnection;28;0;26;0
WireConnection;28;1;25;0
WireConnection;28;2;27;0
WireConnection;29;0;28;0
WireConnection;33;0;14;0
WireConnection;33;1;32;0
WireConnection;33;2;5;0
WireConnection;2;0;1;0
WireConnection;31;0;29;0
WireConnection;31;1;32;0
WireConnection;31;2;12;0
WireConnection;3;0;2;0
WireConnection;3;1;33;0
WireConnection;3;2;6;0
WireConnection;3;3;6;0
WireConnection;3;4;7;0
WireConnection;13;0;2;0
WireConnection;13;1;31;0
WireConnection;13;2;10;0
WireConnection;13;3;3;0
WireConnection;13;4;3;0
WireConnection;0;13;13;0
ASEEND*/
//CHKSM=9874CA4180DDAB6D4E2D745CDEC25D0C5D7141F2