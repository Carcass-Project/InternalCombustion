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

        public Vector3 TexCoords;

        public ICVertex(Vector3 pos)
        {
            Position = pos;
            Normal = new Vector3();
            TexCoords = new Vector3();
        }
    }
}
