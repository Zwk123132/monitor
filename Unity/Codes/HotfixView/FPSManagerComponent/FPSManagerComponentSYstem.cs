using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class FPSManagerComponent_Awake : AwakeSystem<FPSManagerComponent>
    {
        public override void Awake(FPSManagerComponent self)
        {
            self.m_nMaxFPS = 30;
            Application.targetFrameRate = self.m_nMaxFPS;
            Log.Debug("帧数限制为30");
        }
    }

    [FriendClass(typeof(FPSManagerComponent))]
    public static class FPSManagerComponentSystem
    {
        public static void SetNewFPS(this FPSManagerComponent self,int nNew)
        {
            if (nNew<0)
            {
                throw new Exception("要限制的FPS不能小于0");
            }
            self.m_nMaxFPS = nNew;
            Application.targetFrameRate = self.m_nMaxFPS;
            Log.Debug($"帧数限制为{nNew}");
        }
    }
}
