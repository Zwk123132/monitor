using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AppStartInitFinish_FPSManagerCreate : AEvent<EventType.AppStartInitFinish>
    {
        protected override void Run(AppStartInitFinish a)
        {
            
            a.ZoneScene.AddComponent<FPSManagerComponent>();
        }
    }


}
