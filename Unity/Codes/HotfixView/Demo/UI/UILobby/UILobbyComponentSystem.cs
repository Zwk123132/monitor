using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UILobbyComponentAwakeSystem: AwakeSystem<UILobbyComponent>
    {
        public override void Awake(UILobbyComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.enterMap = rc.Get<GameObject>("EnterMap");
            self.enterMap.GetComponent<Button>().onClick.AddListener(() => { self.EnterMap().Coroutine(); });
        }
    }

    public static class UILobbyComponentSystem
    {
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            Log.Debug("原版进入map开始");
            await EnterMapHelper.EnterMapAsync(self.ZoneScene());
            await UIHelper.Remove(self.ZoneScene(), UIType.UILobby);
        }
    }
}