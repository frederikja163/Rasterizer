using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer
{
    public sealed unsafe class Application : IDisposable
    {
        private readonly Window* _window1;
        private readonly Window* _window2;
        private readonly OpenGLRasterizer _openGlRasterizer;
        
        public Application(int triangleCount)
        {
            if (!GLFW.Init())
            {
                throw new Exception("Failed to init glfw!");
            }

            _window1 = GLFW.CreateWindow(800, 600, "Opengl sample rasterizer", null, null);
            _window2 = GLFW.CreateWindow(800, 600, "Custom rasterizer", null, null);
            GLFW.MakeContextCurrent(_window1);
            GL.LoadBindings(new GLFWBindingsContext());
            
            var random = new Random(DateTime.Now.Millisecond + DateTime.Now.Hour * 10000 + DateTime.Now.Day * 1000000);
            var triangles = new Triangle[triangleCount];

            float Rand()
            {
                return (random.Next() / (float)int.MaxValue) * 2 - 1;
            }
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = new Triangle(Rand(), Rand(), Rand(), Rand(), Rand(), Rand());
            }
            _openGlRasterizer = new OpenGLRasterizer(triangles);
            
            GL.ClearColor(1, 0, 1, 1);
        }

        public void Run()
        {
            while (!GLFW.WindowShouldClose(_window1) && !GLFW.WindowShouldClose(_window2))
            {
                GLFW.PollEvents();
                
                GL.Clear(ClearBufferMask.ColorBufferBit);
                
                _openGlRasterizer.Render();
                
                GLFW.SwapBuffers(_window1);
            }
        }

        public void Dispose()
        {
            GLFW.DestroyWindow(_window1);
            GLFW.DestroyWindow(_window2);
            GLFW.Terminate();
        }
    }
}