using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentAttribute :BaseAttribute
{
    public string szName;
    public UIComponentAttribute(string szName)
    {
        this.szName = szName;
    }
}
namespace ET
{

    [ComponentOf(typeof(Scene))]
    public class UIManagerComponent:Entity,IAwake,IDestroy
    {
        /// <summary>
        /// 为按钮提供异步锁
        /// </summary>
        public Dictionary<Button.ButtonClickedEvent, bool> m_dictAsyncLock;
        public static UIManagerComponent Instance;
        /// <summary>
        /// 由界面的名字得到 接口 直接操作接口就可以
        /// </summary>
        public Dictionary<string, IUIEvent> m_DictChild;
        /// <summary>
        /// 由golde/ui/层级得到对应的gameobject
        /// </summary>
        public Dictionary<EUILayer, GameObject> m_dictUILayer;
        /// <summary>
        /// 子界面名字到对应的gameobj
        /// </summary>
        public Dictionary<string, GameObject> m_dictName2Gameobject;
        /// <summary>
        /// 帮子界面Hold住物体 用string来加载
        /// </summary>
        public Dictionary<string, GameObject> m_HoldGameobjcet;
    }
    public enum EUILayer
    {
        Hidden,
        Low,
        Mid,
        High,
        Tips
    }




    public interface IUIEvent
    {
        /// <summary>
        /// 在uiManager Awake中调用 用于子界面加入到uimanager的子组件
        /// </summary>
        /// <param name="self"></param>
        void CreateComponent(UIManagerComponent self);
        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="self"></param>
        void HideWindows(UIManagerComponent self);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="self"></param>
        void ShowWindows(UIManagerComponent self,object parame);
        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="self"></param>
        void DestroyWindows(UIManagerComponent self);
        /// <summary>
        /// 显示时发现没有加载资源则调用
        /// </summary>
        /// <param name="self"></param>
        /// <param name="LayerGameobject"></param>
        void LoadResources(UIManagerComponent self,GameObject LayerGameobject);
    }
    public abstract class UIComponentBase<T> :IUIEvent where T:Entity
    {
        /// <summary>
        /// 将自己挂载在UIManagerComponent 下
        /// </summary>
        /// <param name="self"></param>
        public abstract void CreateComponent(UIManagerComponent self);

        public void DestroyWindows(UIManagerComponent self)
        {
            T t = self.GetComponent<T>();
            if (t==null)
            {
                throw new NullReferenceException($"UIManagerComponent下没有挂载{t.ToString()} ");
            }
            OnDestroyWindows(t);

        }
        public abstract void OnDestroyWindows(T self);

        public void HideWindows(UIManagerComponent self)
        {
            T t = self.GetComponent<T>();
            if (t == null)
            {
                throw new NullReferenceException($"UIManagerComponent下没有挂载{t.ToString()} ");
            }
            OnHideWindows(t);
        }
        public abstract void OnHideWindows(T self);

        public void ShowWindows(UIManagerComponent self,object parame)
        {
            T t = self.GetComponent<T>();
            if (t == null)
            {
                throw new NullReferenceException($"UIManagerComponent下没有挂载{t.ToString()} ");
            }
            OnShowWindows(t,parame);
        }
        public abstract void OnShowWindows(T self, object parame);

        public void LoadResources(UIManagerComponent self,GameObject LayerGameobject)
        {
            T t = self.GetComponent<T>();
            if (t == null)
            {
                throw new NullReferenceException($"UIManagerComponent下没有挂载{t.ToString()} ");
            }
            OnLoadResources(self.GetComponent<T>(), LayerGameobject);
        }
        public abstract void OnLoadResources(T self, GameObject LayerGameobject);
    }

}
