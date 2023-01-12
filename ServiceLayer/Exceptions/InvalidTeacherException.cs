using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Exceptions
{
    public class InvalidTeacherException :Exception
    {
        public InvalidTeacherException(string msg):base(msg) { }
    }
}
