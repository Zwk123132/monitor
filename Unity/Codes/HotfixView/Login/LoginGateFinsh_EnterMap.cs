using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class LoginGateFinsh_EnterMap : AEvent<LoginGateFinish>
    {
        protected override void Run(LoginGateFinish a)
        {
            EnterMap(a.ZoneScene).Coroutine();

        }
        private async ETTask EnterMap(Scene pZoneScene)
        {
            Log.Debug("登录gate完成 开始进入地图");

            G2C_EnterMap pG2C_EnterMap = (G2C_EnterMap)await pZoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMap() { });

            //SceneFactory.CreateCurrentScene(pG2C_EnterMap.MyId)
        }
    }
}
