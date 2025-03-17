using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UITipsComponent_Awake : AwakeSystem<UITpisComponent>
    {
        public override void Awake(UITpisComponent self)
        {
            UITpisComponent.Instance = self;
            self.m_TextTitle = null;
        }
    }
    public class UITpisComponent_Destroy : DestroySystem<UITpisComponent>
    {
        public override void Destroy(UITpisComponent self)
        {
            self.m_TextTitle = null;
        }
    }

    public static class UITipsComponentSystem
    {
        public static void Tips(this UITpisComponent self, string szTitle)
        {

            self.m_szText = szTitle;
            UIManagerComponent.Instance.ShowWindows(nameof(UITpisComponent), EUILayer.Tips);

            
        }
        public static async ETTask OnBack(this UITpisComponent self)
        {

            await TimerComponent.Instance.WaitAsync(2000);
            Log.Warning("测试完后记得移除");
            UIManagerComponent.Instance.HideWindows(nameof(UITpisComponent));
        }
    }
}
