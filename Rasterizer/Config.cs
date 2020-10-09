using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace Rasterizer
{
    public static class Config
    {
        public const int TriangleCount = 3;
        public const int Width = 800;
        public const int Height = 600;
        public static readonly Color4 ClearColor = new Color4(1f, 0f, 1f, 1f);

        #region OpenGL shader
        public static readonly string VertexSrc = @"
            #version 330 core
            in layout(location = 0) vec2 vPos;

            void main()
            {
                gl_Position = vec4(vPos, 0, 1);
            }";

        public static readonly string FragmentSrc = @"
            #version 330 core
            
            out vec4 Color;
            void main()
            {
                Color = vec4(0, 1, 1, 1);
            }";
        #endregion OpenGL shader

        #region Custom cpu shader

        public static (Vector2 pos, VertexToFragment vertToFrag) VertexShader(VertexInput input)
        {
            return (input.Position, new VertexToFragment());
        }

        public static Color4 FragmentShader(VertexToFragment input)
        {
            return new Color4(0f, 1f, 1f, 1f);
        }
        
        public readonly struct VertexInput
        {
            public Vector2 Position { get; }

            public VertexInput(Vector2 pos)
            {
                Position = pos;
            }
        }

        public readonly struct VertexToFragment
        {
        }

        #endregion Custom cpu shader
    }
}