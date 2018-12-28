﻿#version 400

layout(location = 0) in vec3 in_Position;
layout(location = 1) in vec4 in_Color;
layout(location = 2) in vec2 in_TexCoords;

uniform mat3 transform;

varying vec4 ex_Color;
varying vec2 ex_TexCoords;

void main(void)
{
	ex_Color = in_Color;
	ex_TexCoords = in_TexCoords;
	gl_Position = vec4((transform * vec3(in_Position.xy, 1.0f)).xy, in_Position.z, 1.0f);
}