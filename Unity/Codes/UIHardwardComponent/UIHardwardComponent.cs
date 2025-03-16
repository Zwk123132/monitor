using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(UIManagerComponent))]
    public class UIHardwardComponent:Entity,IAwake,IDestroy
    {
    }
}
