Shader "Custom/CutOutMask" {
	SubShader {
		Tags {"Queue" = "Geometry+10" }
 
        ColorMask 0
        ZWrite On
 
        Pass {}
	}
}
