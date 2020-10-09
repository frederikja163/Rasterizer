using OpenTK.Graphics.OpenGL4;

namespace Rasterizer
{
    public class CustomRasterizer : Rasterizer
    {
        private static readonly string VertexSrc = @"
            #version 330 core
            in layout(location = 0) vec2 vPos;

            out vec2 fPos;
            void main()
            {
                gl_Position = vec4(vPos, 0, 1);
                fPos = (vPos + vec2(1)) / 2;
            }
";

        private static readonly string FragmentSrc = @"
            #version 330 core
            in vec2 fPos;
            
            out vec4 Color;
            void main()
            {
                Color = vec4(fPos, 0, 1);
            }
";

        private readonly int _shader, _vbo, _vao,
            _triangleCount;
        
        
        public unsafe CustomRasterizer(Triangle[] triangles) : base("Custom rasterizer")
        {
            var triangle = new Triangle(-1, -1, 3, -1, -1, 3);
            _shader = Utility.CreateShader(in VertexSrc, in FragmentSrc);
            _triangleCount = triangles.Length;
            
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(Triangle),ref triangle, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(Triangle) / 3, 0);
        }

        protected override void OnRender()
        {
            GL.UseProgram(_shader);
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
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