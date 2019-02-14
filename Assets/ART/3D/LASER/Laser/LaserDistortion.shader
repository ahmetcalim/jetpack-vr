// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ONKU/LaserDistortion"
{
	Properties
	{
		_ExtrusionPoint("ExtrusionPoint", Float) = 0
		_ExtrusionAmount("Extrusion Amount", Range( -1 , 20)) = 0.5
		_Color("LaserColor", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float _ExtrusionPoint;
		uniform float _ExtrusionAmount;
		uniform float4 _Color;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			v.vertex.xyz += ( ase_vertexNormal * max( ( sin( ( ( ase_vertex3Pos.y + ( _Time.y / 2.0 ) ) / _ExtrusionPoint ) ) / _ExtrusionAmount ) , 0.0 ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 temp_output_28_0 = _Color;
			o.Albedo = temp_output_28_0.rgb;
			o.Emission = temp_output_28_0.rgb*2;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
2195;-9;1906;1010;1819.311;329.455;1;True;True
Node;AmplifyShaderEditor.TimeNode;25;-1560,271;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;18;-1312,128;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;29;-1225.311,288.545;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1056,160;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1056,256;Float;False;Property;_ExtrusionPoint;ExtrusionPoint;0;0;Create;True;0;0;False;0;0;0.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;19;-832,160;Float;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;20;-656,160;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-832,256;Float;False;Property;_ExtrusionAmount;Extrusion Amount;1;0;Create;True;0;0;False;0;0.5;17.4;-1;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;24;-464,160;Float;False;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;26;-304,160;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;2;-304,0;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-48,16;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;28;-383.311,-408.455;Float;False;Property;_Color;LaserColor;2;0;Create;False;0;0;False;0;0,0,0,0;0.3268526,1,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,-256;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;ONKU/LaserDistortion;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;29;0;25;2
WireConnection;22;0;18;2
WireConnection;22;1;29;0
WireConnection;19;0;22;0
WireConnection;19;1;21;0
WireConnection;20;0;19;0
WireConnection;24;0;20;0
WireConnection;24;1;3;0
WireConnection;26;0;24;0
WireConnection;4;0;2;0
WireConnection;4;1;26;0
WireConnection;0;0;28;0
WireConnection;0;2;28;0
WireConnection;0;11;4;0
ASEEND*/
//CHKSM=80814723222F9958DDE67189F891A6FCEB04C9BE