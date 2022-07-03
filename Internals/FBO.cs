using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace InternalCombustion.Internals
{
    public class FBO
    {
        public int id;
        public RBO renderBuf = null;

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
        }

        public static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public FBO()
        {
            id = GL.GenFramebuffer();
        }
    }
}
