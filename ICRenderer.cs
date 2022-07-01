using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion
{
    public class ICRenderer
    {
        public System.Drawing.Color clearColor;
        public int? sizeX, sizeY;
        public Internals.FBO frameBuffer;
        public Internals.RBO renderBuffer;

        public PostProcShader shader;
        public Internals.VAO screenVAO;

        public void Clear()
        {
            frameBuffer.Bind();
            GL.ClearColor(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
        }

        public void DebindFBO()
        {
            Internals.FBO.BindDefault();
        }

        public void Draw(IRenderable rendr)
        {
            rendr.Draw();
        }

        public int GenRenderTexture()
        {
            renderBuffer = new Internals.RBO((int)sizeX, (int)sizeY);
            frameBuffer = new Internals.FBO();
            frameBuffer.renderBuf = renderBuffer;

            frameBuffer.Bind();

            int idf = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, idf);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, (int)sizeX, (int)sizeY, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (IntPtr)null);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, idf, 0);

            renderBuffer.Bind();

            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, renderBuffer.id);
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                Console.WriteLine("Framebuffer not complete!");
            Internals.FBO.BindDefault();

            return idf;
        }

        public void DrawScreen(int rndTxID)
        {
            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, rndTxID);

            shader._shader.SetInt("renderTexture", 0);
            shader._shader.Use();

            screenVAO.Bind();

            GL.Disable(EnableCap.DepthTest);
            GL.BindTexture(TextureTarget.Texture2D, rndTxID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
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

            screenVAO = new Internals.VAO();
        }

    }
}