using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace InternalCombustion
{
    public interface IRenderable
    {
        public Vector3 position { get; set; }
        public Vector3 size { get; set; }
        public Matrix4 rotation { get; set; }
        public abstract void Draw();
    }
}
