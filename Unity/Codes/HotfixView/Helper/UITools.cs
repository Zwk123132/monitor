using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


namespace ET
{
    public static partial class UITools
    {
        /// <summary>
        /// 销毁子孩子
        /// </summary>
        /// <param name="trans"></param>
        public static void DestroyChildren(GameObject trans)
        {
            for (int i = 0; i < trans.transform.childCount; i++)
            {
                Transform pChild = trans.transform.GetChild(i);
                if (pChild)
                {
                    GameObject.Destroy(pChild.gameObject);
                }
            }
        }
        /// <summary>
        /// 立即销毁子孩子
        /// </summary>
        /// <param name="trans"></param>
        public static void DestroyImmediateChildren(GameObject trans)
        {
            List<Transform> pChildren = new List<Transform>();
            for (int i = 0; i < trans.transform.childCount; i++)
            {
                pChildren.Add(trans.transform.GetChild(i));

            }
            for (int i = trans.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(pChildren[i].gameObject);
            }
        }
        /// <summary>
        /// 找子节点并获取节点的组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="pParent">父节点</param>
        /// <param name="szName">子节点名字</param>
        /// <param name="eType">深度搜索/广度搜索</param>
        /// <returns>组件</returns>
        public static T FindChildComponent<T>(GameObject pParent, string szName) where T : Component
        {
            GameObject resultTrs = UITools.FindChild(pParent, szName);
            T res = resultTrs.gameObject.GetComponent<T>();
            if (res == null)
            {
                throw new System.Exception("Component == null szName= " + szName);
            }
            return res;
        }

        /// <summary>
        /// 找子节点 
        /// </summary>
        /// <param name="pParent">父节点</param>
        /// <param name="szName">子节点名字</param>
        /// <param name="eType">深度搜索/广度搜索</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static GameObject TryFindChild(GameObject pParent, string szName)
        {
            GameObject child = null;

            child = FindChild_dps(pParent, szName);



            return child;
        }

        /// 返回指定子物体在父物体中的序号
        /// </summary>
        /// <param name="parent">父物体的 Transform</param>
        /// <param name="child">需要查找的子物体 Transform</param>
        /// <returns>子物体的序号，如果子物体不属于该父物体，返回 -1</returns>
        public static int FindChildIndex(Transform parent, Transform child)
        {
            if (parent == null || child == null)
            {
                Debug.LogWarning("Parent or Child is null.");
                return -1;
            }

            // 遍历父物体的所有子物体
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i) == child)
                {
                    return i; // 返回找到的序号
                }
            }

            Debug.LogWarning("The specified child is not a child of the given parent.");
            return -1; // 未找到，返回 -1
        }

        /// <summary>
        /// 找子节点 
        /// </summary>
        /// <param name="pParent">父节点</param>
        /// <param name="szName">子节点名字</param>
        /// <param name="eType">深度搜索/广度搜索</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static GameObject FindChild(GameObject pParent, string szName)
        {
            GameObject child = null;

            child = FindChild_dps(pParent, szName);


            if (child == null)
            {
                throw new System.Exception($"GameObject == null szName= " + szName);
            }
            return child;
        }
        /// <summary>
        /// 查询搜索 深度搜索
        /// </summary>
        /// <param name="pParent"></param>
        /// <param name="szName"></param>
        /// <returns></returns>
        private static GameObject FindChild_dps(GameObject pParent, string szName)
        {
            if (pParent == null)
            {
                throw new Exception($"传入参数为空 szName == {szName}，pParent = null");
            }

            if (szName == string.Empty)
            {
                throw new Exception($"传入参数为空 szName == {szName}，pParent = {pParent}");
            }

            if (pParent.name == szName)
            {
                return pParent;
            }
            GameObject[] arrayRootChildren = new GameObject[pParent.transform.childCount];
            for (int i = 0; i < arrayRootChildren.Length; i++)
            {
                arrayRootChildren[i] = pParent.transform.GetChild(i).gameObject;
            }
            foreach (GameObject go in arrayRootChildren)
            {
                if (go != null)
                {
                    GameObject res = FindChild_dps(go, szName);
                    if (res != null)
                    {
                        return res;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 查询子节点 广度搜索
        /// </summary>
        /// <param name="pParent"></param>
        /// <param name="szName"></param>
        /// <returns></returns>
        private static GameObject FindChild_bfs(GameObject pParent, string szName)
        {
            if (pParent == null || szName == string.Empty)
            {
                throw new Exception("传入参数为空");
            }

            // 队列
            Queue<GameObject> queueRootChildren = new Queue<GameObject>();
            queueRootChildren.Enqueue(pParent);

            while (queueRootChildren.Count > 0)
            {
                //判断名字是否正确
                GameObject go = queueRootChildren.Dequeue();
                if (go != null && go.name == szName)
                {
                    return go;
                }

                //遍历子物体加入队列
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    queueRootChildren.Enqueue(go.transform.GetChild(i).gameObject);
                }
            }

            return null;
        }
    }
}
