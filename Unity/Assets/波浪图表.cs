using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ET
{
    public class 波浪图标 : MonoBehaviour
    {
        public static Material rootMaterial;
        public Text TextTitle;
        [Tooltip("是否显示文字")]
        public bool TextEnabled = true;
        [Range(0, 1)]
        public float fJianDianTime = 0.1f;
        private Material m_Material;
        private Coroutine m_cJianBian;

        [SerializeField]
        private string m_szMaterialName;

        void Start()
        {
            if (TextTitle==null&&TextEnabled)
            {
                throw new NullReferenceException("没有找到Text组件");
            }
            Image pImage = GetComponent<Image>();
            
            if (pImage==null)
            {
                throw new NullReferenceException("没有找到Image");
            }
            if (m_szMaterialName==string.Empty)
            {
                throw new NullReferenceException("没有指明要加载的材质名字");
            }
            if (rootMaterial==null)
            {
                rootMaterial = Resources.Load<Material>(m_szMaterialName);
            }
            m_Material = GameObject.Instantiate<Material>(rootMaterial);
            pImage.material = m_Material;
            m_Material.SetTexture("_Texture2D", pImage.mainTexture);
            m_cJianBian = null;
        }

    }
}
