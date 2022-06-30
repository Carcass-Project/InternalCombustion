using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace InternalCombustion
{

    public struct ICVertex
    {

        public Vector3 Position;

        public Vector3 Normal;

        public Vector2 TexCoords;

        public ICVertex(Vector3 pos)
        {
            Position = pos;
            Normal = new Vector3();
            TexCoords = new Vector2();
        }
        public ICVertex(Vector3 pos, Vector3 norm)
        {
            Position = pos;
            Normal = norm;
            TexCoords = new Vector2();
        }

        /// <summary>
        /// Calculate your normals in your vertex shader if ur doing this.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="texCoords"></param>
        public ICVertex(Vector3 pos, Vector2 texCoords)
        {
            Position = pos;
            Normal = new Vector3();
            TexCoords = texCoords;
        }

        public ICVertex(Vector3 pos, Vector3 norm, Vector2 texCoords)
        {
            Position = pos;
            Normal = norm;
            TexCoords = texCoords;
        }
    }
}
