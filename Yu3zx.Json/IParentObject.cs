using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.Json
{
    public interface IParentObject
    {
        object GetParentObject();
        void SetParentObject(object parentObject);
    }
}
