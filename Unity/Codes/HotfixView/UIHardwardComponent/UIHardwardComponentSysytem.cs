using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [UIEvent(nameof(UIHardwardComponent))]
    public class UIHardwardComponentEvent : UIComponentBase<UIHardwardComponent>
    {
        public override void CreateComponent(UIManagerComponent self)
        {
            self.AddComponent<UIHardwardComponent>();
            Log.Debug("我被创建了 我是uihardwardcomponent");
        }

        public override void OnDestroyWindows(UIHardwardComponent self)
        {
            
        }

        public override void OnHideWindows(UIHardwardComponent self)
        {
            throw new NotImplementedException();
        }

        public override void OnLoadResources(UIHardwardComponent self, GameObject LayerGameobject)
        {
            throw new NotImplementedException();
        }


        public override void OnShowWindows(UIHardwardComponent self, object parame)
        {
            throw new NotImplementedException();
        }
    }


    public class UIHardwardComponent_Awake : AwakeSystem<UIHardwardComponent>
    {
        public override void Awake(UIHardwardComponent self)
        {
            
        }
    }
    public class UIHardwardComponent_Destroy : DestroySystem<UIHardwardComponent>
    {

        public override void Destroy(UIHardwardComponent self)
        {
            throw new NotImplementedException();
        }
    }

    public static class UIHardwardComponentSysytem
    {

    }
}
