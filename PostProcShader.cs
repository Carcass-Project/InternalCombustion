using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalCombustion
{
    public class PostProcShader
    {
        public Shader _shader;
        
        public PostProcShader(Shader _shader)
        {
            this._shader = _shader;
        }
    }
}
