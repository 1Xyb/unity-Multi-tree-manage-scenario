using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一棵完整的树
/// </summary>
public class Tree : INode
{
    public Bounds bound { get; set; }//地图的大小包围盒

    private Node root; // 指向这个树的根节点;

    // 将地图分成多少层级来进行管理;  [0, 1, 2, 3, 4, 5(4^5 = 1024个)]
    public int maxDepth { get; set;  } //最大深度


    public int maxChildCount { get; set; }//最大叶子数

    public Tree(Bounds bound)//构造函数 构造树
    {
        this.bound = bound;
        this.maxDepth = 5; // 32x32  根0, 5;  4^5 ====> 1024;
        this.maxChildCount = 4; // 4叉树;

        root = new Node(bound, 0, this);
    }

    // ObjData --->管理角色的数据;
    public void InsertObj(ObjData obj)
    {
        root.InsertObj(obj);
    }

    public void TriggerMove(Camera camera)
    {
        root.TriggerMove(camera);
    }

    public void DrawBound()
    {
        root.DrawBound();
    }
}
