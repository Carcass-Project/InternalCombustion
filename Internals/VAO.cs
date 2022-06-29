using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion.Internals
{
    public class VAO
    {
        public uint id;

        public void Bind()
        {
            GL.BindVertexArray(id);
        }

        public void Destroy()
        {
            GL.DeleteBuffer(id);
        }

        public VAO()
        {
            GL.GenVertexArrays(1, out id);
            GL.BindVertexArray(id);
        }
    }
}
