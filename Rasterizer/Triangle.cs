using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace Rasterizer
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Triangle
    {
        public Vector2 Pos1 { get; }
        public Vector2 Pos2 { get; }
        public Vector2 Pos3 { get; }

        public Triangle(Vector2 pos1, Vector2 pos2, Vector2 pos3)
        {
            Pos1 = pos1;
            Pos2 = pos2;
            Pos3 = pos3;
        }

        public Triangle(float x1, float y1, float x2, float y2, float x3, float y3) :
            this(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3))
        { }
    }
}