using System;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Rasterizer.Engine;

namespace Rasterizer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Before app.run we need to select the rasterizer to run.
            Application app = new Application();
            app.Run();
            app.Dispose();
        }
    }
}