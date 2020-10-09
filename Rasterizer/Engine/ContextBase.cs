using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer.Engine
{
    public abstract unsafe class ContextBase : IDisposable
    {
        private static bool _hasInitializedOpengl;
        private readonly Window* _window;
        
        protected ContextBase(string title)
        {
            _window = GLFW.CreateWindow(Config.Width, Config.Height, title, null, null);
            MakeCurrent();
            if (!_hasInitializedOpengl)
            {
                _hasInitializedOpengl = true;
                GL.LoadBindings(new GLFWBindingsContext());
            }
            GL.ClearColor(Config.ClearColor);
        }

        public void Render(Triangle[] triangles)
        {
            MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            OnRender(triangles);
            GLFW.SwapBuffers(_window);
        }

        protected abstract void OnRender(Triangle[] triangles);

        public bool ShouldClose()
        {
            return GLFW.WindowShouldClose(_window);
        }

        private void MakeCurrent()
        {
            GLFW.MakeContextCurrent(_window);
        }

        public virtual void Dispose()
        {
            GLFW.DestroyWindow(_window);
        }
    }
}