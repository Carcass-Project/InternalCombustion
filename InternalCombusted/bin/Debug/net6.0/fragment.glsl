#version 330 core
out vec4 FragColor;
uniform vec3 modelColor;
void main()
{
    FragColor = vec4(modelColor, 1.0f);
} 