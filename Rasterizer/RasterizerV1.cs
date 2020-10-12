using System;
using OpenTK.Mathematics;
using Rasterizer.Engine;

namespace Rasterizer
{
    public sealed class RasterizerV1
    {
        private Framebuffer _framebuffer;

        public RasterizerV1(ref Framebuffer framebuffer)
        {
            _framebuffer = framebuffer;
        }

        public void Rasterize((Vector2 pos, Config.VertexToFragment vertToFrag) vert1,
            (Vector2 pos, Config.VertexToFragment vertToFrag)  vert2,
            (Vector2 pos, Config.VertexToFragment vertToFrag)  vert3)
        {
            var min = Utility.Min(vert1.pos, vert2.pos, vert3.pos);
            var max = Utility.Max(vert1.pos, vert2.pos, vert3.pos);
            var sign = MathF.Sign(Area(vert1.pos, vert2.pos, vert3.pos));
            for (int x = (int)min.X; x < max.X; x++)
            {
                for (int y = (int)min.Y; y < max.Y; y++)
                {
                    var a1 = MathF.Sign(Area(vert1.pos, vert2.pos, new Vector2(x, y)));
                    var a2 = MathF.Sign(Area(vert2.pos, vert3.pos, new Vector2(x, y)));
                    var a3 = MathF.Sign(Area(vert3.pos, vert1.pos, new Vector2(x, y)));
                    if (a1 == sign && a2 == sign && a3 == sign)
                    {
                        _framebuffer.SetPixelColor(x, y, Config.FragmentShader(vert1.vertToFrag));
                    }
                }
            }
        }

        private float Area(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            p1 -= p3;
            p2 -= p3;
            return (p1.X * p2.Y) - (p1.Y * p2.X);
        }
    }
}