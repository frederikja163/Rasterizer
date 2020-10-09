using System;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            app.Run();
            app.Dispose();
        }
    }
}