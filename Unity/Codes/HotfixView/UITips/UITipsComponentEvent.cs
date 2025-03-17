using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [UIComponentAttribute(nameof(UITpisComponent))]
    public class UITipsComponentEvent : UIComponentBase<UITpisComponent>
    {
        public override void CreateComponent(UIManagerComponent self)
        {
            Log.Debug("创建了tips组件");
            self.AddComponent<UITpisComponent>();
        }

        public override void OnDestroyWindows(UITpisComponent self)
        {
            
        }

        public override void OnHideWindows(UITpisComponent self)
        {
            Log.Debug("隐藏了");
        }


        public override void OnLoadResources(UITpisComponent self, GameObject LayerGameobject)
        {
            Log.Debug("加载开始");
            self.m_objWindow = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("dlg/dlg_UITips"),LayerGameobject.transform);
            self.m_objWindow.name = nameof(UITpisComponent);
            self.m_TextTitle = UITools.FindChildComponent<Text>(self.m_objWindow, "Title");
            UITools.FindChildComponent<Button>(self.m_objWindow, "BackButton").onClick.AddListenerAsync(self.OnBack);
            Log.Debug("加载完成");
        }

        public override void OnShowWindows(UITpisComponent self)
        {
            self.m_TextTitle.text = self.m_szText;
        }
    }
}
