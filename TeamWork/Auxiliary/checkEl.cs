using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    public class checkEl<T>
    {
        public bool isCheck { get; set; }
        public T chClass { get; set; }
        public checkEl()
        {
            isCheck = false;
        }
        public checkEl(T chClass)
        {
            isCheck = false;
            this.chClass = chClass;
        }
        public checkEl(bool isCheck, T chClass)
        {
            this.isCheck = isCheck;
            this.chClass = chClass;

        }

    }
}
