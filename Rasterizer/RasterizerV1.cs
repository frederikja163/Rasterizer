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
            var area = Area(vert1.pos, vert2.pos, vert3.pos);
            var sign = Math.Sign(area);
            for (int x = (int)min.X; x < max.X; x++)
            {
                for (int y = (int)min.Y; y < max.Y; y++)
                {
                    var a12 = Area(vert1.pos, vert2.pos, new Vector2(x, y));
                    var a23 = Area(vert2.pos, vert3.pos, new Vector2(x, y));
                    var a31 = Area(vert3.pos, vert1.pos, new Vector2(x, y));
                    if ((a12 >= sign && a23 >= sign && a31 >= sign) || (a12 <= sign && a23 <= sign && a31 <= sign))
                    {
                        var b1 = a23 / area;
                        var b2 = a31 / area;
                        var b3 = a12 / area;
                        var lerped = Lerp(b1, b2, b3,
                            vert1.vertToFrag, vert2.vertToFrag, vert3.vertToFrag);
                        _framebuffer.SetPixelColor(x, y, Config.FragmentShader(lerped));
                    }
                }
            }
        }

        private unsafe Config.VertexToFragment Lerp(float v1, float v2, float v3,
            Config.VertexToFragment v2f1, Config.VertexToFragment v2f2, Config.VertexToFragment v2f3)
        {
            var returnVal = new Config.VertexToFragment();
            for (int i = 0; i < sizeof(Config.VertexToFragment) / sizeof(float); i++)
            {
                returnVal.Data[i] = v2f1.Data[i] * v1 + v2f2.Data[i] * v2 + v2f3.Data[i] * v3;
            }
            return returnVal;
        }

        private float Area(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            p1 -= p3;
            p2 -= p3;
            return (p1.X * p2.Y) - (p1.Y * p2.X);
        }
    }
}