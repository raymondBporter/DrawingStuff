#version 400

layout(location = 0) in vec3 in_Position; 
layout(location = 1) in vec4 in_Color; 

varying vec4 ex_Color;

void main(void)
{ 
	ex_Color = in_Color;
	gl_Position = vec4(in_Position.xy, 1.0f, 1.0f);
}