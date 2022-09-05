using UnityEngine;

/// <summary>
/// 场景物体的数据类
/// </summary>
[System.Serializable]
public class ObjData
{
    [SerializeField]
    public string sUid;//独一无二的id，通过guid创建
    [SerializeField]
    public string resPath;//prefab路径
    [SerializeField]
    public Vector3 pos;//位置
    [SerializeField]
    public Quaternion rotation;//旋转
    public ObjData(string resPath, Vector3 pos, Quaternion rotation)
    {
        this.sUid = System.Guid.NewGuid().ToString();
        this.resPath = resPath;
        this.pos = pos;
        this.rotation = rotation;
    }
}
