using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义节点
/// </summary>
public class Node : INode
{
    // 数据域
    public Bounds bound { get; set; }//管理的地图大小包围盒
    private List<ObjData> objList;//在你这个区域，被节点管理的物体
    // end 

    // 关系域
    private int depth;//深度, 到了第几层, 0开始的
    private Tree belongTree;//所属树Tree对象;
    private Node[] childList;//当前树节点的子节点
    // end

    
    public Node(Bounds bound, int depth, Tree belongTree)
    {
        this.belongTree = belongTree;
        this.bound = bound;
        this.depth = depth;
        //childList = new Node[belongTree.maxChildCount];
        objList = new List<ObjData>();
    }
    
    public void InsertObj(ObjData obj)//插入对象
    {
        Node node = null;
        bool bChild = false;
        
        if(depth < belongTree.maxDepth && childList == null)//如果深度小于数的最大子节点数    并且没有子节点
        {
            //如果还没到叶子节点，可以拥有儿子且儿子未创建，则创建儿子
            CerateChild();
        }

        if(childList != null) // 什么时候childList会为NULL， 到了叶子节点的时候;
        {
            for (int i = 0; i < childList.Length; ++i)
            {
                Node item = childList[i];
                if (item == null)
                {
                    break;
                }
                if (item.bound.Contains(obj.pos))
                {
                    // 表示前面已经有人管理了, 有属于另外一个孩子的管理区域
                    if (node != null)
                    {
                        bChild = false;
                        break;
                    }
                    node = item; // 第一次找到了一个孩子，可以管辖物体;
                    bChild = true;
                }
            }
        }
        
        if (bChild) { // 我们的物体，完全归属于一个孩子;
            node.InsertObj(obj);
        }
        else // 物体归属于多个孩子，所以我们加到自己这里来;
            // 最终到叶子节点，也会走这里;
        {
            objList.Add(obj);
        }
    }

    public void TriggerMove(Camera camera)
    {
        //刷新当前节点
        for(int i = 0; i < objList.Count; ++i)
        {
            //进入该节点中意味着该节点在摄像机内，把该节点保存的物体全部创建出来
            ResourcesManager.Instance.LoadAsync(objList[i]);
        }

        if(depth == 0)
        {
            ResourcesManager.Instance.RefreshStatus();
        }

        //刷新子节点
        if (childList != null)
        {
            for(int i = 0; i < childList.Length; ++i)
            {
                if (childList[i].bound.CheckBoundIsInCamera(camera))
                {
                    childList[i].TriggerMove(camera);
                }
            }
        }
    }
 
    private void CerateChild()//创建孩子
    {
        childList = new Node[belongTree.maxChildCount];
        int index = 0;
        for(int i = -1; i <= 1; i+=2)
        {
            for(int j = -1; j <= 1; j+=2)
            {
                Vector3 centerOffset = new Vector3(bound.size.x / 4 * i, 0, bound.size.z / 4 * j);
                Vector3 cSize = new Vector3(bound.size.x / 2, bound.size.y, bound.size.z / 2);
                Bounds cBound = new Bounds(bound.center + centerOffset, cSize);
                childList[index++] = new Node(cBound, depth + 1, belongTree);
            }
        }
    }
    
    public void DrawBound()//绘制包围盒
    {
        if(objList.Count != 0)//范围内有物体就画蓝色
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bound.center, bound.size - Vector3.one * 0.1f);
        }
        else//没有就画红色
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bound.center, bound.size - Vector3.one * 0.1f);
        }
        
        if(childList != null)//孩子节点也是一样的绘制
        {
            for(int i = 0; i < childList.Length; ++i)
            {
                childList[i].DrawBound();
            }
        }
        
    }
}
