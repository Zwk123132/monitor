using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ETNode : MonoBehaviour
{
    public string Name;
    public string ComponentName;
    [Header("是场景吗")]
    public bool IsScene;
    [Header("子孩子数")]
    public int ChildCount;
    [Header("挂载的组件数")]
    public int ChildComponentCount;

    public new string ToString;



}

