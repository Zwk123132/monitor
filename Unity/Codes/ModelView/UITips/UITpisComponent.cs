using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ComponentOf(typeof(UIManagerComponent))]
    public class UITpisComponent:Entity,IAwake,IDestroy
    {
        public Text m_TextTitle;
        public GameObject m_objWindow;
        public static UITpisComponent Instance;

        public string m_szText;
    }
}
