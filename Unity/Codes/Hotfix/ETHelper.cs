using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ETHelper
    {
        //public static void TestName(Entity entity)
        //{
        //    var nameprop = entity.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);

        //    if (nameprop!=null)
        //    {

        //    } ;

        //    Scene s;
        //}
        private static void toETNode(ShowETNode node,Entity entity)
        {
            Type pType = entity.GetType();
            node.TypeName = pType.Name;
            node.IsScene = false;
            var scene = entity as Scene;
            if (scene!=null)
            {
                node.IsScene = true;
                node.Name = scene.Name;
            }
            else
            {
                node.Name = pType.Name;
            }
            node.Childs = new List<ShowETNode>();
            node.ChildComponent = new List<ShowETNode>();
            foreach (var item in entity.Children.Values)
            {
                ShowETNode srn = new ShowETNode();
                toETNode(srn, item);
                node.Childs.Add(srn);
            }
            foreach (var item in entity.Components.Values)
            {
                ShowETNode srn = new ShowETNode();
                toETNode(srn, item);
                node.ChildComponent.Add(srn);
            }
        }
        public static ShowETNode ToETNode(Entity root)
        {
            ShowETNode self = new ShowETNode();
            toETNode(self, root);
            return self;

        }

        public static void OutChildTree(Entity entity)
        {
            int nBepth = 0;
            Queue<Entity> queueEntity = new Queue<Entity>();
            queueEntity.Enqueue(entity);
            int count = 1;
            int zcount = 0;
            Console.Write($"第{0}层:");
            while (queueEntity.Count!=0)
            {
                var en = queueEntity.Dequeue();
                if (count==0)
                {
                    count = zcount;
                    zcount = 0;
                    nBepth++;
                    Console.Write($"\n第{nBepth}层:");
                }
                count--;
                Scene pScene = en as Scene;
                if (pScene != null)
                {
                    Console.Write("场景:" + pScene.Name + "   ");
                }
                else
                {
                    Console.Write(en.GetType().Name + "   ");
                    
                }
                OutComponentTree(en);
                Console.WriteLine(entity.GetType().Name + "   ");
                foreach (var item in en.Children.Values)
                {
                    queueEntity.Enqueue(item);
                    zcount++;
                }


                
            }

        }
        public static void OutComponentTree(Entity entity)
        {
            int nBepth = 0;
            Queue<Entity> queueEntity = new Queue<Entity>();
            queueEntity.Enqueue(entity);
            int count = 1;
            int zcount = 0;
            Console.Write($"挂载节点为:");
            while (queueEntity.Count != 0)
            {
                var en = queueEntity.Dequeue();
                if (count == 0)
                {
                    count = zcount;
                    zcount = 0;
                    nBepth++;
                }
                count--;
                Scene pScene = en as Scene;
                if (pScene!=null)
                {
                    Console.Write("场景:"+pScene.Name + "   ");
                }
                else
                {
                    Console.Write(en.GetType().Name + "   ");
                }
                    


                
                foreach (var item in en.Components.Values)
                {
                    queueEntity.Enqueue(item);
                    zcount++;
                }

                //private Dictionary<Type, Entity> components;
        //en.GetComponent
            }

        }
        private static void OutTree_BPS(Entity entity)
        {

            
        }

    }
}
