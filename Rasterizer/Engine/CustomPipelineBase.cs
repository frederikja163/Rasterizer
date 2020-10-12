using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Rasterizer.Engine
{
    public abstract class CustomPipelineBase : ContextBase
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

            uniform sampler2D uTexture;
            
            out vec4 Color;
            void main()
            {
                Color = texture(uTexture, fPos);
            }
";

        private readonly int _shader, _vbo, _vao, _texture;

        protected unsafe CustomPipelineBase(string title) : base(title)
        {
            _shader = Utility.CreateShader(in VertexSrc, in FragmentSrc);
            
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 2 * 3, new []{-1f, -1f, 3f, -1f, -1f, 3f}, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, 0);

            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Config.Width, Config.Height, 0,
                PixelFormat.Rgba, PixelType.Float, new Color4[Config.Width * Config.Height]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        protected void Draw(ref Framebuffer framebuffer)
        {
            GL.UseProgram(_shader);
            GL.Uniform1(1, 0);
            GL.BindVertexArray(_vao);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Config.Width, Config.Height, PixelFormat.Rgba, PixelType.Float, ref framebuffer.GetColorPtr());
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