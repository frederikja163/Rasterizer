using System;
using OpenTK.Graphics.OpenGL4;

namespace Rasterizer
{
    public sealed class OpenGLRasterizer : IDisposable
    {
        private static readonly string VertexSrc = @"
            #version 330 core
            in layout(location = 0) vec2 vPos;

            void main()
            {
                gl_Position = vec4(vPos, 0, 1);
            }
";

        private static readonly string FragmentSrc = @"
            #version 330 core
            
            out vec4 Color;
            void main()
            {
                Color = vec4(0, 1, 1, 1);
            }
";

        private readonly int _shader, _vbo, _vao,
            _triangleCount;
        
        
        public unsafe OpenGLRasterizer(Triangle[] triangles)
        {
            _shader = Utility.CreateShader(in VertexSrc, in FragmentSrc);
            _triangleCount = triangles.Length;
            
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(Triangle) * triangles.Length, triangles, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(Triangle) / 3, 0);
        }

        public void Render()
        {
            GL.UseProgram(_shader);
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _triangleCount * 3);
        }

        public void Dispose()
        {
            GL.DeleteProgram(_shader);
            GL.DeleteVertexArray(_vao);
            GL.DeleteBuffer(_vbo);
        }
    }
}