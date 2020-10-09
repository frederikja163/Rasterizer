using System;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Application app = new Application(2);
            app.Run();
            app.Dispose();
        }
    }
}