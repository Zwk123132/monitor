using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [MessageHandler]
    public class UIShowETTreeNodeComponent_Netlogic : AMHandler<G2C_TreeNode>
    {
        protected override void Run(Session session, G2C_TreeNode message)
        {
            Log.Warning("收到et消息");
            //UIManagerComponent.Instance.ShowWindows("UIShowETNodeTreeComponent", message.RootETNode);
            UIManagerComponent.Instance.ShowWindows("UIShowETNodeTreeComponent", ETHelper.ToETNode(Game.Scene));
        }
    }
}
