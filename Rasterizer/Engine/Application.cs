using System;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer.Engine
{
    public sealed unsafe class Application : IDisposable
    {
        private readonly OpenGLRasterizer _openGlRasterizer;
        private readonly Pipeline _customPipelineBase;
        private readonly Triangle[] _triangle;
        
        public Application()
        {
            if (!GLFW.Init())
            {
                throw new Exception("Failed to init glfw!");
            }

            GLFW.WindowHint(WindowHintBool.Resizable, false);
            
            var random = new Random(DateTime.Now.Millisecond + DateTime.Now.Hour * 10000 + DateTime.Now.Day * 1000000);
            _triangle = new Triangle[Config.TriangleCount];

            float Rand()
            {
                return (random.Next() / (float)int.MaxValue) * 2 - 1;
            }
            Config.VertexInput RandVert()
            {
                return new Config.VertexInput(Rand(), Rand(), Rand(), Rand(), Rand(), 1);
            }
            for (int i = 0; i < _triangle.Length; i++)
            {
                _triangle[i] = new Triangle(RandVert(), RandVert(), RandVert());
            }
            _openGlRasterizer = new OpenGLRasterizer();
            _customPipelineBase = new Pipeline("custom pipeline");
            
        }

        public void Run()
        {
            while (!_openGlRasterizer.ShouldClose() && !_customPipelineBase.ShouldClose())
            {
                _openGlRasterizer.Render(_triangle);
                
                _customPipelineBase.Render(_triangle);
                
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