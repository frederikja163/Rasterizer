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

        public Triangle(Config.VertexInput vert1, Config.VertexInput vert2, Config.VertexInput vert3)
        {
            Vert1 = vert1;
            Vert2 = vert2;
            Vert3 = vert3;
        }
    }
}