using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace ET
{
    public class UIManagerComponent_Awake : AwakeSystem<UIManagerComponent>
    {
        public override void Awake(UIManagerComponent self)
        {
            Log.Debug("UIManager启动!!!!!!!!");
            self.m_DictChild = new Dictionary<string, IUIEvent>();
            foreach (Type item in Game.EventSystem.GetTypes(typeof(UIComponentAttribute)))
            {
                IUIEvent uie = (IUIEvent)Activator.CreateInstance(item);
                uie.CreateComponent(self);
                var cas = (UIComponentAttribute[])item.GetCustomAttributes(typeof(UIComponentAttribute));
                if (cas.Length==0)
                {
                    throw new NullReferenceException("没有继承特性UIComponentAttribute");
                }
                self.m_DictChild.Add(cas[0].szName, uie);
            }
        }
    }
    public class UIManagerComponent_Destroy : DestroySystem<UIManagerComponent>
    {
        public override void Destroy(UIManagerComponent self)
        {
            foreach (var item in self.m_DictChild.Values)
            {
                item.DestroyWindows(self);
            }
        }
    }

    [FriendClass(typeof(UIManagerComponent))]
    public  static class UIManagerComponentSystem 
    {
        public static void ShowWindows(this UIManagerComponent self, string szName)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName,out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            uie.ShowWindows(self);
        }
        public static void HideWindows(this UIManagerComponent self,string szName)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName, out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            uie.HideWindows(self);
        }
        public static void DestroyWindows(this UIManagerComponent self,string szName)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName, out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            uie.DestroyWindows(self);
        }
    }
}
