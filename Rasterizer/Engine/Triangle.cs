using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace Rasterizer.Engine
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Triangle
    {
        public Config.VertexInput Vert1 { get; }
        public Config.VertexInput Vert2 { get; }
        public Config.VertexInput Vert3 { get; }

        public Triangle(Vector2 pos1, Vector2 pos2, Vector2 pos3)
        {
            Vert1 = new Config.VertexInput(pos1);
            Vert2 = new Config.VertexInput(pos2);
            Vert3 = new Config.VertexInput(pos3);
        }

        public Triangle(float x1, float y1, float x2, float y2, float x3, float y3) :
            this(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3))
        { }
    }
}