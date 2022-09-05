using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 定义节点的接口
/// </summary>
public interface INode
{
    Bounds bound { get; set; }
    /// <summary>
    /// 初始化插入一个场景物体
    /// </summary>
    /// <param name="obj"></param>
    void InsertObj(ObjData obj);
    /// <summary>
    /// 当触发者（主角）移动时显示/隐藏物体
    /// 当role走动的时候，需要从四叉树中找到并创建摄像机可以看到的物体
    /// </summary>
    /// <param name="camera"></param>
    void TriggerMove(Camera camera);
    void DrawBound();
}
