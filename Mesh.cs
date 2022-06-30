using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace InternalCombustion
{
    public class Mesh : IRenderable
    {
        public uint[] indices;
        public ICVertex[] vertices;

        public Internals.EBO elementBuffer;
        public Internals.VBO vertexBuffer;
        public Internals.VAO vertexArrObj;

        public ICMaterial material;

        public Vector3 position { get; set; }
        public Vector3 size { get; set; }

        public Matrix4 rotation { get; set; }

        #region InternalWorks
        public void Draw()
        {
            vertexArrObj.Bind();

            material.matShader.SetMatrix4("model", Matrix4.CreateTranslation(position) * rotation * Matrix4.CreateScale(size));
            
            material.matShader.SetVector3("modelColor", ((Vector4)material.matColor).Xyz);

            material.matShader.Use();

            elementBuffer.Bind();

            GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0); 
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            elementBuffer.Destroy();
            vertexBuffer.Destroy();
        }

        public unsafe Mesh(uint[] Indices, ICVertex[] Vertices)
        {
            indices = Indices;
            vertices = Vertices;

            vertexBuffer = new Internals.VBO();
            vertexBuffer.SetData(System.Runtime.InteropServices.Marshal.SizeOf(typeof(ICVertex)) * Vertices.Length, Vertices);

            elementBuffer = new Internals.EBO();
            elementBuffer.SetData(Indices.Length * sizeof(uint), indices);

            vertexArrObj = new Internals.VAO();
            vertexArrObj.Bind();

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(ICVertex), 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(ICVertex), Marshal.OffsetOf(typeof(ICVertex), "Normal"));

            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, sizeof(ICVertex), Marshal.OffsetOf(typeof(ICVertex), "TexCoords"));

            GL.BindVertexArray(0);
        }
        #endregion

        #region StaticFunctions
        public static Mesh GenMeshCube()
        {
            Mesh msh;

            List<ICVertex> vertices = new List<ICVertex>();
            vertices.Add(new ICVertex(new Vector3(-1, 1, -1), new Vector3(0, 0, 1), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(1, 1, 1), new Vector3(0, 0, 1), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(1, 1, -1), new Vector3(0, 0, 1), new Vector2(1, 1)));
            vertices.Add(new ICVertex(new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(1, 1)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, 1), new Vector3(0, 0, 0), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(1, -1, 1), new Vector3(0, 0, 0), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, 1, 1), new Vector3(-1, -1, 0), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, -1), new Vector3(-1, -1, 0), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, 1), new Vector3(-1, -1, 0), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(1, -1, -1), new Vector3(0, 0, -1), new Vector2(1, 1)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, 1), new Vector3(0, 0, -1), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, -1), new Vector3(0, 0, -1), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(1, 1, -1), new Vector3(1, 1, 0), new Vector2(1, 1)));
            vertices.Add(new ICVertex(new Vector3(1, -1, 1), new Vector3(1, 1, 0), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(1, -1, -1), new Vector3(1, 1, 0), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, 1, -1), new Vector3(0, 0, 0), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(1, -1, -1), new Vector3(0, 0, 0), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, -1, -1), new Vector3(0, 0, 0), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, 1, 1), new Vector3(0, 0, 1), new Vector2(0, 0)));
            vertices.Add(new ICVertex(new Vector3(-1, 1, 1), new Vector3(0, 0, 0), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(-1, 1, -1), new Vector3(-1, -1, 0), new Vector2(1, 1)));
            vertices.Add(new ICVertex(new Vector3(1, -1, 1), new Vector3(0, 0, -1), new Vector2(1, 0)));
            vertices.Add(new ICVertex(new Vector3(1, 1, 1), new Vector3(1, 1, 0), new Vector2(0, 1)));
            vertices.Add(new ICVertex(new Vector3(1, 1, -1), new Vector3(0, 0, 0), new Vector2(1, 1)));
            List<uint> indices = new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 18, 1, 3, 19, 4, 6, 20, 7, 9, 21, 10, 12, 22, 13, 15, 23, 16 };

            msh = new Mesh(indices.ToArray(), vertices.ToArray());

            msh.position = new Vector3(0, 0, -5);
            msh.size = new Vector3(0.1f, 0.1f, 0.1f);
            msh.rotation = Matrix4.CreateRotationX(0) * Matrix4.CreateRotationY(0) * Matrix4.CreateRotationZ(0);

            return msh;
        }
        #endregion
    }
}
