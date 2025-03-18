using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UITipsComponent_Awake : AwakeSystem<UITipsComponent>
    {
        public override void Awake(UITipsComponent self)
        {
            UITipsComponent.Instance = self;
            self.m_TextTitle = null;
        }
    }
    public class UITipsComponent_Destroy : DestroySystem<UITipsComponent>
    {
        public override void Destroy(UITipsComponent self)
        {
            self.m_TextTitle = null;
        }
    }

    public static class UITipsComponentSystem
    {
        public static void Tips(this UITipsComponent self, string szTitle)
        {

            self.m_szText = szTitle;
            UIManagerComponent.Instance.ShowWindows(nameof(UITipsComponent), EUILayer.Tips);

            
        }
        public static async ETTask OnBack(this UITipsComponent self)
        {
            UIManagerComponent.Instance.HideWindows(nameof(UITipsComponent));
            await ETTask.CompletedTask;

        }
    }
}
