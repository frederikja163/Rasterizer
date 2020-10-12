using System;
using OpenTK.Graphics.OpenGL4;

namespace Rasterizer.Engine
{
    public sealed class OpenGLRasterizer : ContextBase
    {
        private readonly int _shader, _vbo, _vao;
        
        
        public unsafe OpenGLRasterizer() : base("Opengl rasterizer")
        {
            _shader = Utility.CreateShader(in Config.VertexSrc, in Config.FragmentSrc);
            
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            int size = sizeof(Triangle) * Config.TriangleCount;
            GL.BufferData(BufferTarget.ArrayBuffer, size, new float[size], BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(Triangle) / 3, 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, sizeof(Triangle) / 3, sizeof(float) * 2);
        }

        protected override unsafe void OnRender(Triangle[] triangles)
        {
            GL.UseProgram(_shader);
            GL.BindVertexArray(_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, sizeof(Triangle) * triangles.Length, triangles);
            GL.DrawArrays(PrimitiveType.Triangles, 0, triangles.Length * 3);
        }

        public override void Dispose()
        {
            GL.DeleteProgram(_shader);
            GL.DeleteVertexArray(_vao);
            GL.DeleteBuffer(_vbo);
            base.Dispose();
        }
    }
}