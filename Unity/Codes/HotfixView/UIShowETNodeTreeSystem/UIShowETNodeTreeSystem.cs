using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
using UnityEngine;

namespace ET
{
    [FriendClass(typeof(UIShowETNodeTreeComponent))]
    
    [UIComponent(nameof(UIShowETNodeTreeComponent))]
    public class UIShowETNodeTreeEvent : UIComponentBase<UIShowETNodeTreeComponent>
    {
        public override void CreateComponent(UIManagerComponent self)
        {
            self.AddComponent<UIShowETNodeTreeComponent>();
        }

        public override void OnDestroyWindows(UIShowETNodeTreeComponent self)
        {
            
        }

        public override void OnHideWindows(UIShowETNodeTreeComponent self)
        {
            
        }

        public override void OnLoadResources(UIShowETNodeTreeComponent self, GameObject LayerGameobject)
        {
            self.m_window = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            self.m_window.name = nameof(UIShowETNodeTreeComponent);
            self.m_window.transform.SetParent(LayerGameobject.transform);

            self.m_NodeRes = Resources.Load<GameObject>("ETNode");
            if (self.m_NodeRes==null)
            {
                throw new Exception("加载ETNOde资源失败");
            }


        }

        public override void OnShowWindows(UIShowETNodeTreeComponent self, object parame)
        {
            if (parame==null)
            {
                throw new Exception("UIShowEtNodeTreeComponent界面传入参数不能为空");
            }
            ShowETNode root = parame as ShowETNode;
            if (root==null)
            {
                throw new Exception("UIShowEtNodeTreeComponent界面传入参数不能转showetnode");
            }
            self.ShowETNode(root);
        }
    }

    [FriendClass(typeof(UIShowETNodeTreeComponent))]
    public static class UIShowETNodeTreeSystem
    {
        public static void ShowETNode(this UIShowETNodeTreeComponent self,Entity entity)
        {
            ShowETNode(self, ETHelper.ToETNode(entity));
        }
        public static void ShowETNode(this UIShowETNodeTreeComponent self, ShowETNode root)
        {
            showETNode( self,GameObject.Instantiate<GameObject>( self.m_NodeRes,self.m_window.transform), root);   
        }
        private static void showETNode(this UIShowETNodeTreeComponent self, GameObject gameObject,ShowETNode node)
        {

            var eTNode = gameObject.GetComponent<ETNode>();
            Transform childTran = gameObject.transform.GetChild(0);
            Transform componentTran = gameObject.transform.GetChild(1);
            eTNode.Name = node.Name;
            eTNode.gameObject.name = node.Name;
            eTNode.ComponentName = node.TypeName;
            eTNode.IsScene = node.IsScene;
            eTNode.ChildComponentCount = node.ChildComponent.Count;
            eTNode.ChildCount = node.Childs.Count;

            foreach (var item in node.Childs)
            {
                showETNode(self, GameObject.Instantiate<GameObject>(self.m_NodeRes, childTran), item);
            }
            foreach (var item in node.ChildComponent)
            {
                showETNode(self, GameObject.Instantiate<GameObject>(self.m_NodeRes, componentTran), item);
            }
        }
    }


}
