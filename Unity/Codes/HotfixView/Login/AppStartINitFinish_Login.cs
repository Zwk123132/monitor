using CommandLine;
using ET.EventType;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ET
{
    public class AppStartINitFinishAsync_Login :AEventAsync<AppStartInitFinishAsync>
    {


        protected override async ETTask Run(AppStartInitFinishAsync a)
        {

            Log.Debug("开始登录");
            Scene pZoneScene = a.ZoneScene;
            Session pSession = pZoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(ConstValue.LoginAddress));
            R2C_Login pR2C_Login = (R2C_Login)await pSession.Call(new C2R_Login() { Account = "zwk", Password = " " });
            pSession.Dispose();
            
            pZoneScene.AddComponent<SessionComponent>().Session = pZoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(pR2C_Login.Address));
            G2C_LoginGate pG2C_LoginGate = (G2C_LoginGate)await pZoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_LoginGate() { Key = pR2C_Login.Key, GateId = pR2C_Login.GateId });
            if (pG2C_LoginGate.Error == ErrorCode.ERR_Success)
            {
                Game.EventSystem.Publish<LoginGateFinish>(new LoginGateFinish() { ZoneScene = pZoneScene });
            }
            else
            {
                UITipsComponent.Instance.Tips($"登录失败+{pG2C_LoginGate.Message}");
            }
            
        }
    }
}
