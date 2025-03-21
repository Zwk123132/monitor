using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public partial class ShowETNode : Object
    {
        public override string ToString()
        {
            if (IsScene)
            {
                return $"类型是：{this.TypeName} 名字是{this.Name}";
               
            }
            else
            {
                return $"类型是：{this.TypeName}";
            }
               
        }
    }
}
