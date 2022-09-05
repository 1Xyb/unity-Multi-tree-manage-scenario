using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏运行的一开始，先构造四叉树，并把场景物体的数据插入四叉树中由四叉树管理数据
/// </summary>
[System.Serializable]
public class Main : MonoBehaviour
{
    [SerializeField]
    public List<ObjData> objList = new List<ObjData>();//可观察物体列表
    public Bounds mainBound;//主包围盒(最大的范围)
    
    private Tree tree;//构建树
    private bool bInitEnd = false;//是否初始化完成

    private Role role;//人物(主角)
    public bool isDebug;
    public void Awake()
    {
        tree = new Tree(mainBound);//创建树根节点

        for(int i = 0; i < objList.Count; ++i)//把可观察的这些物体都插入到对应的树节点去
        // for (int i = 0; i < 1; ++i)
        {
            tree.InsertObj(objList[i]);
        }

        role = GameObject.Find("Role").GetComponent<Role>();
        bInitEnd = true;
    }

    private void Update()
    {
        if (role.bMove)
        {
            tree.TriggerMove(role.mCamera);
        }
    }
    private void OnDrawGizmos()
    {
        if (!isDebug)
        {
            return; 
        }
        if (bInitEnd)
        {
            tree.DrawBound();
        }
        else
        {
            Gizmos.DrawWireCube(mainBound.center, mainBound.size);//画出一个最大的包围盒
        }
    }

}
