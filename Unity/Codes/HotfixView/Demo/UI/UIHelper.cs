﻿using System;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace ET
{
    public static class UIHelper
    {
        public static async ETTask<UI> Create(Scene scene, string uiType, UILayer uiLayer)
        {
            UI ui = await scene.GetComponent<UIComponent>().Create(uiType, uiLayer);
            return ui;
        }
        
        public static async ETTask Remove(Scene scene, string uiType)
        {
            scene.GetComponent<UIComponent>().Remove(uiType);
            await ETTask.CompletedTask;
        }

        public static void Lock(this ButtonClickedEvent self)
        {
            if (UIManagerComponent.Instance == null)
            {
                throw new System.Exception("UIManagerComponent.Instance为空！UImanagerComponent为创建!");
            }
            UIManagerComponent.Instance.SetLock(self);
        }
        public static void UnLock(this ButtonClickedEvent self)
        {
            if (UIManagerComponent.Instance == null)
            {
                throw new System.Exception("UIManagerComponent.Instance为空！UImanagerComponent为创建!");
            }
            UIManagerComponent.Instance.SetUnLock(self);
        }
        public static void AddListenerAsync(this ButtonClickedEvent self,Func<ETTask> etTask)
        {
            if (UIManagerComponent.Instance==null)
            {
                throw new Exception("UIManagerComponent.Instance为空！UImanagerComponent未创建!");
            }
            async ETTask clickActionAsync()
            {
                if (UIManagerComponent.Instance.isLock(self))
                {
                    Log.Warning("重复点击一个被锁定的按钮");
                    return;
                }
                UIManagerComponent.Instance.SetLock(self);
                await etTask();
                UIManagerComponent.Instance.SetUnLock(self);
            }
  
            self.AddListener(() => {
                clickActionAsync().Coroutine();

            });
        }

    }
}