using System;
using Rasterizer.Engine;

namespace Rasterizer
{
    [Flags]
    public enum ClearMask
    {
        None = 0x00,
        ColorBuffer = 0x01,
        DepthBuffer = 0x02,
        All = None | ColorBuffer | DepthBuffer
    }
    
    public sealed class Pipeline : CustomPipelineBase
    {
        private Framebuffer _framebuffer;
        
        public Pipeline(string title) : base(title)
        {
            _framebuffer = new Framebuffer(Config.Width, Config.Height);
        }

        protected override void OnRender(Triangle[] triangles)
        {
            Clear(ClearMask.ColorBuffer);
            
            SwapBuffers();
        }

        public void Clear(ClearMask mask)
        {
            if ((mask & ClearMask.ColorBuffer) == ClearMask.ColorBuffer)
            {
                _framebuffer.ClearColorBuffer();
            }
            if ((mask & ClearMask.DepthBuffer) == ClearMask.DepthBuffer)
            {
                _framebuffer.ClearColorBuffer();
            }
        }

        public void SwapBuffers()
        {
            base.Draw(ref _framebuffer);
        }
    }
}