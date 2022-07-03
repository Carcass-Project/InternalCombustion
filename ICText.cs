using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Core;
using SharpFont;

namespace InternalCombustion
{
    public struct Character
    {
        public int TextureID { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Bearing { get; set; }
        public int Advance { get; set; }
    }

    public struct Font
    {
        public Dictionary<uint, Character> chars { get; set; }
        public Guid? fontId { get; set; }
        public string Name { get; set; }
    }

    public static class ICText
    {
        public static Shader shader;
        public static Vector2i screenSize;
        static Matrix4 projectionM = Matrix4.CreateScale(new Vector3(1f / screenSize.X, 1f / screenSize.Y, 1.0f));

        public static void Init(Vector2i scrSize)
        {
            screenSize = scrSize;
            InitVBOVAO();
            shader = new Shader("textVertex.glsl", "textFrag.glsl");
            shader.Use();
        }

        public static Font LoadFontFromFile(string path)
        {
            Font font = new Font();
            font.fontId = new Guid();

            Library lib = new Library();
            Face face = new Face(lib, path);
            face.SetPixelSizes(0, 32);

            font.Name = Path.GetFileNameWithoutExtension(path);

            // set 1 byte pixel alignment 
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            font.chars = new Dictionary<uint, Character>();

            for (uint c = 0; c < 128; c++)
            {
                try
                {

                    //face.LoadGlyph(c, LoadFlags.Render, LoadTarget.Normal);
                    face.LoadChar(c, LoadFlags.Render, LoadTarget.Normal);
                    GlyphSlot glyph = face.Glyph;
                    FTBitmap bitmap = glyph.Bitmap;

                    int texObj = GL.GenTexture();
                    GL.BindTexture(TextureTarget.Texture2D, texObj);
                    GL.TexImage2D(TextureTarget.Texture2D, 0,
                                  PixelInternalFormat.R8, bitmap.Width, bitmap.Rows, 0,
                                  PixelFormat.Red, PixelType.UnsignedByte, bitmap.Buffer);

                    GL.TextureParameter(texObj, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                    GL.TextureParameter(texObj, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                    GL.TextureParameter(texObj, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                    GL.TextureParameter(texObj, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

                
                    Character ch = new Character();
                    ch.TextureID = texObj;
                    ch.Size = new Vector2(bitmap.Width, bitmap.Rows);
                    ch.Bearing = new Vector2(glyph.BitmapLeft, glyph.BitmapTop);
                    ch.Advance = (int)glyph.Advance.X;
                    
                    font.chars.Add(c, ch);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }               
            }
            face.Dispose();
            lib.Dispose();
            return font;
        }

        public static void DrawText(Font font, string text, float x, float y, float scale, Vector2 dir, Color4 color)
        {
            projectionM = Matrix4.CreateOrthographicOffCenter(0.0f, screenSize.X, screenSize.Y, 0.0f, -1.0f, 1.0f);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);


            shader.Use();


            shader.SetMatrix4("projection", projectionM);
            shader.SetVector3("textColor", ((Vector4)color).Xyz);

            RenderText(font, text, x, y, scale, dir);

            GL.Disable(EnableCap.Blend);
        }

        static int _vao = 0, _vbo = 0;

        public static void InitVBOVAO()
        {

            GL.BindTexture(TextureTarget.Texture2D, 0);


            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

            float[] vquad =
            {
                // x      y      u     v    
                0.0f, -1.0f,   0.0f, 0.0f,
                0.0f,  0.0f,   0.0f, 1.0f,
                1.0f,  0.0f,   1.0f, 1.0f,
                0.0f, -1.0f,   0.0f, 0.0f,
                1.0f,  0.0f,   1.0f, 1.0f,
                1.0f, -1.0f,   1.0f, 0.0f
            };

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, 4 * 6 * 4, vquad, BufferUsageHint.StaticDraw);

            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * 4, 0);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * 4, 2 * 4);
        }

        public static void RenderText(Font font, string text, float x, float y, float scale, Vector2 dir)
        {
            
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindVertexArray(_vao);

            float angle_rad = (float)Math.Atan2(dir.Y, dir.X);
            Matrix4 rotateM = Matrix4.CreateRotationZ(angle_rad);
            Matrix4 transOriginM = Matrix4.CreateTranslation(new Vector3(x, y, 0f));

            float char_x = 0.0f;
            foreach (var c in text)
            {
                if (font.chars.ContainsKey(c) == false)
                    continue;
                Character ch = font.chars[c];

                float w = ch.Size.X * scale;
                float h = ch.Size.Y * scale;
                float xrel = char_x + ch.Bearing.X * scale;
                float yrel = (ch.Size.Y - ch.Bearing.Y) * scale;

                char_x += (ch.Advance >> 6) * scale; 

                Matrix4 scaleM = Matrix4.CreateScale(new Vector3(w, h, 1.0f));
                Matrix4 transRelM = Matrix4.CreateTranslation(new Vector3(xrel, yrel, 0.0f));

                Matrix4 modelM = scaleM * transRelM * rotateM * transOriginM; 
                shader.SetMatrix4("model", modelM);


                GL.BindTexture(TextureTarget.Texture2D, ch.TextureID);


                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            }

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
