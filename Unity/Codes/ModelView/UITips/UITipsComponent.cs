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
    public class UITipsComponent:Entity,IAwake,IDestroy
    {
        public Text m_TextTitle;
        public GameObject m_objWindow;
        public static UITipsComponent Instance;

        public string m_szText;
    }
}
