using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer
{
    public sealed unsafe class Application : IDisposable
    {
        private readonly OpenGLRasterizer _openGlRasterizer;
        private readonly CustomRasterizer _customRasterizer;
        
        public Application()
        {
            if (!GLFW.Init())
            {
                throw new Exception("Failed to init glfw!");
            }

            GLFW.WindowHint(WindowHintBool.Resizable, false);
            
            var random = new Random(DateTime.Now.Millisecond + DateTime.Now.Hour * 10000 + DateTime.Now.Day * 1000000);
            var triangles = new Triangle[Config.TriangleCount];

            float Rand()
            {
                return (random.Next() / (float)int.MaxValue) * 2 - 1;
            }
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = new Triangle(Rand(), Rand(), Rand(), Rand(), Rand(), Rand());
            }
            _openGlRasterizer = new OpenGLRasterizer(triangles);
            _customRasterizer = new CustomRasterizer(triangles);
            
        }

        public void Run()
        {
            while (!_openGlRasterizer.ShouldClose() && !_customRasterizer.ShouldClose())
            {
                _openGlRasterizer.Render();
                
                _customRasterizer.Render();
                
                GLFW.PollEvents();
            }
        }

        public void Dispose()
        {
            _openGlRasterizer.Dispose();
            _customRasterizer.Dispose();
            GLFW.Terminate();
        }
    }
}