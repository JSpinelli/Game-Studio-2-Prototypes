// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/TvScreen"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Curvature("Curvature", Vector) = (0,0,0,0)
		_Resolution("Resolution", Vector) = (512,512,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float2 _Curvature;
		uniform float2 _Resolution;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_cast_0 = (1.0).xx;
			float2 temp_output_47_0 = ( ( i.uv_texcoord * 2.0 ) - temp_cast_0 );
			float2 temp_cast_1 = (2.0).xx;
			float2 temp_cast_2 = (1.0).xx;
			float2 temp_cast_3 = (1.0).xx;
			float4 tex2DNode1 = tex2D( _MainTex, ( ( ( ( pow( ( abs( temp_output_47_0 ) / _Curvature ) , temp_cast_1 ) * temp_output_47_0 ) + temp_output_47_0 ) * 0.5 ) + 0.5 ) );
			o.Emission = ( tex2DNode1 * ( ( tex2DNode1.a * ( ( 0.5 + ( sin( ( i.uv_texcoord.x * _Resolution.x * 2.0 ) ) * 0.5 ) ) * 1.38 ) ) * ( ( 0.5 + ( sin( ( 2.0 * i.uv_texcoord.y * _Resolution.y ) ) * 0.5 ) ) * 1.38 ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
201;73;1235;655;767.0067;812.7073;1.552456;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;44;-1653.204,-366.9346;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-1349.392,-316.4701;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-1163.266,-263.042;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1168.607,-411.532;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;47;-968.2658,-373.5421;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;50;-995.5657,-578.9415;Inherit;False;Property;_Curvature;Curvature;1;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.AbsOpNode;49;-1165.867,-640.0415;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-839.5656,-494.4412;Inherit;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;51;-742.0651,-646.5413;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;52;-665.3656,-512.6411;Inherit;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;63;-1576.943,-156.6888;Inherit;False;Property;_Resolution;Resolution;2;0;Create;True;0;0;0;False;0;False;512,512;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-495.0823,-490.9678;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-1136.809,-94.69022;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;67;-945.4453,-63.31927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-670.3772,-260.1343;Inherit;False;Constant;_Float3;Float 3;2;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-418.7292,-333.2412;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-1144.652,54.32204;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-255.9262,-190.6516;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SinOpNode;68;-943.877,51.18488;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-709.8757,-102.6515;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-425.6255,157.2344;Inherit;False;Constant;_Float4;Float 4;4;0;Create;True;0;0;0;False;0;False;1.38;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-422.0631,-75.49362;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-697.3748,63.28544;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;-114.866,-470.739;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;72;-429.1876,43.24532;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;169.0501,-438.2104;Inherit;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-179.8361,-50.55861;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-177.4614,45.61992;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-3.61412,-122.3641;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;270.8003,-136.1208;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;542.5074,-239.7787;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;838.9464,-270.1496;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Unlit/TvScreen;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;44;0
WireConnection;45;1;46;0
WireConnection;47;0;45;0
WireConnection;47;1;48;0
WireConnection;49;0;47;0
WireConnection;51;0;49;0
WireConnection;51;1;50;0
WireConnection;52;0;51;0
WireConnection;52;1;53;0
WireConnection;55;0;52;0
WireConnection;55;1;47;0
WireConnection;65;0;44;1
WireConnection;65;1;63;1
WireConnection;65;2;46;0
WireConnection;67;0;65;0
WireConnection;54;0;55;0
WireConnection;54;1;47;0
WireConnection;66;0;46;0
WireConnection;66;1;44;2
WireConnection;66;2;63;2
WireConnection;56;0;54;0
WireConnection;56;1;57;0
WireConnection;68;0;66;0
WireConnection;69;0;67;0
WireConnection;69;1;57;0
WireConnection;71;0;57;0
WireConnection;71;1;69;0
WireConnection;70;0;68;0
WireConnection;70;1;57;0
WireConnection;58;0;56;0
WireConnection;58;1;57;0
WireConnection;72;0;57;0
WireConnection;72;1;70;0
WireConnection;1;1;58;0
WireConnection;74;0;71;0
WireConnection;74;1;73;0
WireConnection;75;0;72;0
WireConnection;75;1;73;0
WireConnection;76;0;1;4
WireConnection;76;1;74;0
WireConnection;77;0;76;0
WireConnection;77;1;75;0
WireConnection;79;0;1;0
WireConnection;79;1;77;0
WireConnection;0;2;79;0
ASEEND*/
//CHKSM=4E8375C4944A40CEEA25BE2BDA372A720CCC1C7C