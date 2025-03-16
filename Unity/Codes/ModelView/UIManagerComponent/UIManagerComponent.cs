using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UIManagerComponent:Entity,IAwake,IDestroy
    {
        public Dictionary<string, IUIEvent> m_DictChild;
    }


    public class UIComponentAttribute : Attribute
    {
        public string szName;
        public UIComponentAttribute(string szName)
        {
            this.szName = szName;
        }
    }


    public interface IUIEvent
    {
        void CreateComponent(UIManagerComponent self);
        void HideWindows(UIManagerComponent self);
        void ShowWindows(UIManagerComponent self);
        void DestroyWindows(UIManagerComponent self);
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

        public void ShowWindows(UIManagerComponent self)
        {
            T t = self.GetComponent<T>();
            if (t == null)
            {
                throw new NullReferenceException($"UIManagerComponent下没有挂载{t.ToString()} ");
            }
            OnShowWindows(t);
        }
        public abstract void OnShowWindows(T self);
    }

}
