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
            //var zoneScene = a.ZoneScene;
            //var address = ConstValue.LoginAddress;
            //try
            //{
            //    // 创建一个ETModel层的Session
            //    R2C_Login r2CLogin;
            //    Session session = null;
            //    try
            //    {
            //        session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
            //        {
            //            r2CLogin = (R2C_Login)await session.Call(new C2R_Login() { Account = "z", Password ="password" });
            //        }
            //    }
            //    finally
            //    {
            //        session?.Dispose();
            //    }

            //    // 创建一个gate Session,并且保存到SessionComponent中
            //    Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLogin.Address));
            //    gateSession.AddComponent<PingComponent>();
            //    zoneScene.AddComponent<SessionComponent>().Session = gateSession;

            //    G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await gateSession.Call(
            //        new C2G_LoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId });

            //    Log.Debug("登陆gate成功!");

            //    Game.EventSystem.Publish(new EventType.LoginFinish() { ZoneScene = zoneScene });
            //}
            //catch (Exception e)
            //{
            //    Log.Error(e);
            //}
            //return;
            try
            {
                Log.Debug("开始登录");
                Scene pZoneScene = a.ZoneScene;
                Session pSession = pZoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(ConstValue.LoginAddress));
                R2C_Login pR2C_Login = (R2C_Login)await pSession.Call(new C2R_Login() { Account = "zwk", Password = " " });
                pSession.Dispose();
                
                pZoneScene.AddComponent<SessionComponent>().Session = pZoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(pR2C_Login.Address));
                G2C_LoginGate pG2C_LoginGate = (G2C_LoginGate)await pZoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_LoginGate() { Key = pR2C_Login.Key, GateId = pR2C_Login.GateId });

                if (pG2C_LoginGate.Error==ErrorCode.ERR_Success)
                {
                    UITipsComponent.Instance.Tips("登录成功");
                }
                else
                {
                    UITipsComponent.Instance.Tips("登录失败");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

            await ETTask.CompletedTask;
        }
    }
}
