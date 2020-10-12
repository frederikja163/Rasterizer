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
            in layout(location = 1) vec4 vColor;

            out vec4 fColor;
            void main()
            {
                gl_Position = vec4(vPos, 0, 1);
                fColor = vColor;
            }";

        public static readonly string FragmentSrc = @"
            #version 330 core
            in vec4 fColor;            

            out vec4 Color;
            void main()
            {
                Color = fColor;
            }";
        #endregion OpenGL shader

        #region Custom cpu shader

        public static (Vector2 pos, VertexToFragment vertToFrag) VertexShader(VertexInput input)
        {
            return (input.Position, new VertexToFragment(input.Position, input.Color));
        }

        public static Color4 FragmentShader(VertexToFragment input)
        {
            return input.Color;
        }
        
        public unsafe struct VertexInput
        {
            public fixed float Data[6];
            
            public Vector2 Position => new Vector2(Data[0], Data[1]);
            public Color4 Color => new Color4(Data[2], Data[3], Data[4], Data[5]);

            public VertexInput(Vector2 pos, Color4 color)
            {
                // Data = new[] {pos.X, pos.Y, color.R, color.G, color.B, color.A};
                Data[0] = pos.X;
                Data[1] = pos.Y;
                Data[2] = color.R;
                Data[3] = color.G;
                Data[4] = color.B;
                Data[5] = color.A;
            }

            public VertexInput(float x, float y, float r, float g, float b, float a) :
                this(new Vector2(x, y), new Color4(r, g, b, a))
            {
                
            }
        }

        
        public unsafe struct VertexToFragment
        {
            public fixed float Data[6];
            
            public Vector2 Position => new Vector2(Data[0], Data[1]);
            public Color4 Color => new Color4(Data[2], Data[3], Data[4], Data[5]);

            public VertexToFragment(Vector2 pos, Color4 color)
            {
                // Data = new[] {pos.X, pos.Y, color.R, color.G, color.B, color.A};
                Data[0] = pos.X;
                Data[1] = pos.Y;
                Data[2] = color.R;
                Data[3] = color.G;
                Data[4] = color.B;
                Data[5] = color.A;
            }

            public VertexToFragment(float x, float y, float r, float g, float b, float a) :
                this(new Vector2(x, y), new Color4(r, g, b, a))
            {
                
            }
        }

        #endregion Custom cpu shader
    }
}