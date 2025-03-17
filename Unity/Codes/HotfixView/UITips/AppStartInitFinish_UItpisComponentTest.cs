using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    //AppStartInitFinish_CreateLoginUI
    public class AppStartInitFinish_UItpisComponentTest : AEvent<AppStartInitFinish>
    {

        protected override void Run(AppStartInitFinish a)
        {
            Log.Warning("测试完删掉");
            if (UITpisComponent.Instance == null)
            {
                Log.Warning("null!");
            }
            else
            {
                UITpisComponent.Instance.Tips("大大大发 发");
            }
        }
    }
}
