using OpenTK.Mathematics;

namespace Rasterizer.Engine
{
    public static class Config
    {
        public const int TriangleCount = 3;
        public const int Width = 800;
        public const int Height = 600;
        public static readonly Color4 ClearColor = new Color4(1f, 0f, 1f, 1f);
    }
}