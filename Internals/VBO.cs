using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion.Internals
{
    public class VBO
    {
        public uint id;

        public void SetData<T>(int dataSize, T[] data) where T : struct
        {
            GL.BufferData(BufferTarget.ArrayBuffer, dataSize, data, BufferUsageHint.StaticDraw);
        }
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        public void Destroy()
        {
            GL.DeleteBuffer(id);
        }

        public VBO()
        {
            GL.GenBuffers(1, out id);
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }
    }
}
