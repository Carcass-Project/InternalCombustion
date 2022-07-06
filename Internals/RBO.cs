using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace InternalCombustion.Internals
{
    public class RBO
    {
        public int id;

        public int x, y;

        public void Bind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, id);
            //GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 1, RenderbufferStorage.Depth24Stencil8, x, y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }

        public RBO(int X, int Y)
        {
            id = GL.GenRenderbuffer();
            x = X;
            y = Y;
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, id);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth24Stencil8, x, y);
            //GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 1, RenderbufferStorage.Depth24Stencil8, x, y);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
        }
    }
}
