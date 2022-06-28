using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion
{
    public class ICRenderer
    {
        public System.Drawing.Color clearColor;
        public int? sizeX, sizeY;

        public void Clear()
        {
            GL.ClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void VertexDataI(int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
            GL.EnableVertexAttribArray(0);
        }

        public ICRenderer(System.Drawing.Color ClearColor, int width, int height)
        {
            clearColor = ClearColor;
            sizeX = width;
            sizeY = height;

            GL.Viewport(0, 0, sizeX.Value, sizeY.Value);
        }

    }
}