using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion.Internals
{
    public class EBO
    {
        public uint id;

        public void SetData<T>(int dataSize, T[] data) where T : struct
        {
            GL.BufferData(BufferTarget.ElementArrayBuffer, dataSize, data, BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        }

        public void Destroy()
        {
            GL.DeleteBuffer(id);
        }

        public EBO()
        {
            GL.GenBuffers(1, out id);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
        }
    }
}
