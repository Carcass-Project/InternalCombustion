using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace InternalCombustion
{
    public class ICMaterial
    {
        public Shader matShader;
        public static Shader _default;
        public Color4 matColor = Color4.White;

        /// <summary>
        /// Default Material, change _default Shader to your default object-rendering shader before using first.
        /// </summary>
        public static ICMaterial Default { 
            get
            {
                var icm = new ICMaterial(_default);

                return icm;
            } 
        }

        public ICMaterial(Shader matShader)
        {
            this.matShader = matShader;
        }
    }
}
