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
        public Color4 matColor = Color4.Black;
        /// <summary>
        /// To use this, you must have a "vertex.glsl", and "fragment.glsl".
        /// </summary>
        public static ICMaterial Default { 
            get
            {
                var shader = new Shader("vertex.glsl", "fragment.glsl");
                var icm = new ICMaterial(shader);

                return icm;
            } 
        }

        public ICMaterial(Shader matShader)
        {
            this.matShader = matShader;
        }
    }
}
