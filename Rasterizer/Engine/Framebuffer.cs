using System;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;
using Rasterizer.Engine;

namespace Rasterizer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Framebuffer
    {
        private readonly Color4[] _colorBuffer;
        private readonly float[] _depthBuffer;
        private readonly int _width;
        private readonly int _height;

        public Framebuffer(int width, int height)
        {
            _width = width;
            _height = height;
            _colorBuffer = new Color4[_width * _height];
            _depthBuffer = new float[_width * _height];
        }

        public void SetPixelColor(int x, int y, Color4 color)
        {
            _colorBuffer[x + y * _width] = color;
        }

        public Color4 GetPixelColor(int x, int y)
        {
            return _colorBuffer[x + y * _width];
        }

        public void ClearColorBuffer()
        {
            Array.Fill(_colorBuffer, Config.ClearColor);
        }

        public void SetPixelDepth(int x, int y, float depth)
        {
            _depthBuffer[x + y * _width] = depth;
        }

        public float GetPixelDepth(int x, int y)
        {
            return _depthBuffer[x + y * _width];
        }

        public void ClearDepthBuffer()
        {
            Array.Fill(_depthBuffer, 0);
        }

        public ref Color4 GetColorPtr()
        {
            return ref _colorBuffer[0];
        }
    }
}