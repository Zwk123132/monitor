using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(UIManagerComponent))]
    public class UIShowETNodeTreeComponent:Entity,IAwake,IDestroy
    {
        public GameObject m_window;
        public GameObject m_NodeRes;
    }
}
