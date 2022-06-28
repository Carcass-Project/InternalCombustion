using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace InternalCombustion
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ICVertex
    {
        [FieldOffset(0)]
        public Vector3 Position;
        [FieldOffset(1)]
        public Vector3 Normal;
        [FieldOffset(2)]
        public Vector3 TexCoords;

        public ICVertex(Vector3 pos)
        {
            Position = pos;
            Normal = new Vector3();
            TexCoords = new Vector3();
        }
    }
}
