﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Button;


namespace ET
{
    
    public class UIManagerComponent_Awake : AwakeSystem<UIManagerComponent>
    {
        public override void Awake(UIManagerComponent self)
        {
            try
            {
                UIManagerComponent.Instance = self;
                Log.Debug("UIManager启动!!!!!!!!");
                self.m_dictName2Gameobject = new Dictionary<string, GameObject>();
                self.m_dictAsyncLock = new Dictionary<ButtonClickedEvent, bool>();
                self.m_dictUILayer = new Dictionary<EUILayer, GameObject>();
                self.m_DictChild = new Dictionary<string, IUIEvent>();
                foreach (Type item in Game.EventSystem.GetTypes(typeof(global::UIComponentAttribute)))
                {
                    IUIEvent uie = (IUIEvent)Activator.CreateInstance(item);
                    uie.CreateComponent(self);
                    object[] cas = item.GetCustomAttributes(typeof(global::UIComponentAttribute),false);
                    if (cas.Length == 0)
                    {
                        throw new NullReferenceException("没有继承特性UIComponentAttribute");
                    }
                    self.m_DictChild.Add((cas[0] as UIComponentAttribute).szName, uie);
                }
                
                GameObject pobjUI = GameObject.Find("/Global/UI");
                if (pobjUI == null)
                {
                    throw new NullReferenceException("没有在场景中找到/Global/UI！");
                }
                for (int i = 0; i < pobjUI.transform.childCount; i++)
                {
                    if (pobjUI.transform.GetChild(i).name != ((EUILayer)i).ToString())
                    {
                        throw new NullReferenceException("没有在场景中找到/Global/UI" + "/" + ((EUILayer)i).ToString());
                    }
                    self.m_dictUILayer.Add((EUILayer)i, pobjUI.transform.GetChild(i).gameObject);
                }
                foreach (var item in self.m_dictUILayer.Values)
                {
                    Log.Debug(item.name);
                }

            }
            catch (Exception ex)
            {
                Log.Warning(ex.ToString());
                throw;
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
        public static void ShowWindows(this UIManagerComponent self, string szName,EUILayer pEUILayer)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName,out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            GameObject pChild = null;
            if (self.m_dictName2Gameobject.ContainsKey(szName))
            {
                pChild = self.m_dictName2Gameobject[szName];
            }
            else
            {
                uie.LoadResources(self, self.m_dictUILayer[pEUILayer]);
                Log.Warning("到这里没问题");
                pChild = UITools.FindChild(GameObject.Find("/Global/UI"), szName);
                if (pChild == null)
                {
                    throw new Exception($"在{self.m_dictUILayer[EUILayer.Hidden].transform.parent.name}中没有找到名字为{szName}的物体");
                }
                self.m_dictName2Gameobject.Add(szName, pChild);
            }
            uie.ShowWindows(self);
            pChild.SetActive(true);
        }
        public static void HideWindows(this UIManagerComponent self,string szName)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName, out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            if (false == self.m_dictName2Gameobject.TryGetValue(szName, out GameObject pChild))
            {
                throw new Exception($"{szName}不在名字到物体的字典里");
            }
            uie.HideWindows(self);
            pChild.SetActive(false);
        }
        public static void SetLock(this UIManagerComponent self, ButtonClickedEvent pButtonClickedEvent)
        {
            if (false == self.m_dictAsyncLock.TryGetValue(pButtonClickedEvent,out bool lockState))
            {
                self.m_dictAsyncLock.Add(pButtonClickedEvent, true);
            }
            else
            {
                if (isLock(self, pButtonClickedEvent))
                {
                    throw new Exception("此OnClick已被上锁,请在上锁前前检查锁定状态");
                }
                self.m_dictAsyncLock[pButtonClickedEvent] = true;
            }


        }
        public static bool isLock(this UIManagerComponent self, ButtonClickedEvent pButtonClickedEvent)
        {
            if (false==self.m_dictAsyncLock.ContainsKey(pButtonClickedEvent))
            {
                Log.Debug("没有所以没被锁定");
                return false;
            }

            return self.m_dictAsyncLock[pButtonClickedEvent];
        }
        public static void SetUnLock(this UIManagerComponent self, ButtonClickedEvent pButtonClickedEvent)
        {
            if (false == self.m_dictAsyncLock.ContainsKey(pButtonClickedEvent))
            {
                throw new Exception("无需解锁!");
            }
            self.m_dictAsyncLock[pButtonClickedEvent] = false;
        }
        public static void DestroyWindows(this UIManagerComponent self,string szName)
        {
            IUIEvent uie = null;
            if (!self.m_DictChild.TryGetValue(szName, out uie))
            {
                throw new NullReferenceException($"{szName}不在UIManager的字典里");
            }
            if (false == self.m_dictName2Gameobject.TryGetValue(szName, out GameObject pChild))
            {
                throw new Exception($"{szName}不在名字到物体的字典里");
            }
            GameObject.DestroyImmediate(pChild);
            self.m_dictName2Gameobject.Remove(szName);
            self.m_DictChild.Remove(szName);
            uie.DestroyWindows(self);
        }
    }
}
