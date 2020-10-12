using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

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
        
        public static Vector2 Max(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            return new Vector2(Max(v1.X, v2.X, v3.X), Max(v1.Y, v2.Y, v3.Y));
        }
        
        public static Vector2 Min(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            return new Vector2(Min(v1.X, v2.X, v3.X), Min(v1.Y, v2.Y, v3.Y));
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