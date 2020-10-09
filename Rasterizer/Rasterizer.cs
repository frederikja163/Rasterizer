using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Rasterizer
{
    public abstract unsafe class Rasterizer : IDisposable
    {
        private static bool _hasInitializedOpengl;
        private readonly Window* _window;
        
        protected Rasterizer(string title)
        {
            _window = GLFW.CreateWindow(800, 600, title, null, null);
            MakeCurrent();
            if (!_hasInitializedOpengl)
            {
                _hasInitializedOpengl = true;
                GL.LoadBindings(new GLFWBindingsContext());
            }
            GL.ClearColor(1, 0, 1, 1);
        }

        public void Render()
        {
            MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            OnRender();
            GLFW.SwapBuffers(_window);
        }

        protected abstract void OnRender();

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