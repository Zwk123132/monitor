using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [UIEvent(nameof(UIHardwardComponent))]
    [ComponentOf(typeof(UIManagerComponent))]
    public class UIHardwardComponent :Entity ,IAwake,IDestroy
    {
    }
}
