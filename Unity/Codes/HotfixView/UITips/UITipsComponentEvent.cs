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
    [FriendClass(typeof(UITipsComponent))]
    [UIComponentAttribute(nameof(UITipsComponent))]
    public class UITipsComponentEvent : UIComponentBase<UITipsComponent>
    {
        public override void CreateComponent(UIManagerComponent self)
        {
            self.AddComponent<UITipsComponent>();
        }

        public override void OnDestroyWindows(UITipsComponent self)
        {
            
        }

        public override void OnHideWindows(UITipsComponent self)
        {

        }


        public override void OnLoadResources(UITipsComponent self, GameObject LayerGameobject)
        {
            self.m_objWindow = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("dlg/dlg_UITips"),LayerGameobject.transform);
            self.m_objWindow.name = nameof(UITipsComponent);
            self.m_TextTitle = UITools.FindChildComponent<Text>(self.m_objWindow, "Title");
            UITools.FindChildComponent<Button>(self.m_objWindow, "BackButton").onClick.AddListenerAsync(self.OnBack);
        }


        public override void OnShowWindows(UITipsComponent self, object parame)
        {
            self.m_TextTitle.text = self.m_szText;
        }
    }
}
