#version 400

layout(location = 0) in vec3 in_Position; 
layout(location = 1) in vec4 in_Color; 
layout(location = 2) in vec2 in_TexCoords;

varying vec4 ex_Color;
varying vec2 ex_TexCoords;

void main(void)
{ 
	ex_Color = in_Color;
	ex_TexCoords = in_TexCoords;
	gl_Position = vec4(in_Position.xyz, 1.0f);
}