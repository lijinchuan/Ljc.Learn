using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ljc.Learn.DataStruct.LiCharp.Operation
{
    public class MultOperation:OperationBase
    {
        public override string Symbol
        {
            get
            {
                return "*";
            }
        }

        public override OperationPrivilege Privilege
        {
            get
            {
                return OperationPrivilege.MultAndDevi;
            }
        }
    }
}
