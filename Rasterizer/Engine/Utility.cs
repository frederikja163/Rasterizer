using System;
using OpenTK.Graphics.OpenGL4;

namespace Rasterizer.Engine
{
    public static class Utility
    {
        public static float Max(float v1, float v2, float v3)
        {
            return MathF.Max(v1, MathF.Max(v2, v3));
        }
        
        public static float Min(float v1, float v2, float v3)
        {
            return MathF.Min(v1, MathF.Min(v2, v3));
        }
        
        public static int CreateShader(in string vertexSrc, in string fragmentSrc)
        {
            int CreateShader(in string src, ShaderType type)
            {
                int shader = GL.CreateShader(type);
                GL.ShaderSource(shader, src);
                GL.CompileShader(shader);
                GL.GetShaderInfoLog(shader, out var il);
                if (!string.IsNullOrWhiteSpace(il))
                {
                    throw new Exception(il);
                }

                return shader;
            }

            int vert = CreateShader(in vertexSrc, ShaderType.VertexShader);
            int frag = CreateShader(in fragmentSrc, ShaderType.FragmentShader);
            
            var shader = GL.CreateProgram();
            GL.AttachShader(shader, vert);
            GL.AttachShader(shader, frag);
            GL.LinkProgram(shader);
            GL.GetProgramInfoLog(shader, out var il);
            if (!string.IsNullOrWhiteSpace(il))
            {
                throw new Exception(il);
            }
            GL.DetachShader(shader, vert);
            GL.DetachShader(shader, frag);
            GL.DeleteShader(vert);
            GL.DeleteShader(frag);
            return shader;
        }
    }
}