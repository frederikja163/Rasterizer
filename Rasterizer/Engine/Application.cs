using System;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer.Engine
{
    public sealed unsafe class Application : IDisposable
    {
        private readonly OpenGLRasterizer _openGlRasterizer;
        private readonly CustomPipelineBase _customPipelineBase;
        
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
            _customPipelineBase = new Pipeline("custom pipeline");
            
        }

        public void Run()
        {
            while (!_openGlRasterizer.ShouldClose() && !_customPipelineBase.ShouldClose())
            {
                //TODO: Pass triangles here
                _openGlRasterizer.Render();
                
                _customPipelineBase.Render();
                
                GLFW.PollEvents();
            }
        }

        public void Dispose()
        {
            _openGlRasterizer.Dispose();
            _customPipelineBase.Dispose();
            GLFW.Terminate();
        }
    }
}