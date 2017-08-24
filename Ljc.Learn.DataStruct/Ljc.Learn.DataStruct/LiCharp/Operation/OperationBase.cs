using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp.Operation
{
    public class OperationBase:Express.ExpressBase
    {
        public virtual string Symbol
        {
            get;
            private set;
        }

        public virtual OperationPrivilege Privilege
        {
            get;
            private set;
        }
    }
}
