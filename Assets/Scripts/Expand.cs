using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对bound来说，它有8个点，当它的8个点同时处于摄像机裁剪块上方/下方/前方/后方/左方/右方，那么该bound不与摄像机可视范围交叉
/// </summary>
public static class Expand 
{
    public static bool CheckBoundIsInCamera(this Bounds bound, Camera camera)//检测物体是否在摄像机范围内
    {
        System.Func<Vector4, int> ComputeOutCode = (projectionPos) =>
        {
            int _code = 0;
            if (projectionPos.x < -projectionPos.w) _code |= 1;
            if (projectionPos.x > projectionPos.w) _code |= 2;
            if (projectionPos.y < -projectionPos.w) _code |= 4;
            if (projectionPos.y > projectionPos.w) _code |= 8;
            if (projectionPos.z < -projectionPos.w) _code |= 16;
            if (projectionPos.z > projectionPos.w) _code |= 32;
            return _code;
        };

        Vector4 worldPos = Vector4.one;
        int code = 63;
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    worldPos.x = bound.center.x + i * bound.extents.x;
                    worldPos.y = bound.center.y + j * bound.extents.y;
                    worldPos.z = bound.center.z + k * bound.extents.z;

                    code &= ComputeOutCode(camera.projectionMatrix * camera.worldToCameraMatrix * worldPos);
                }
            }
        }
        return code == 0 ? true : false;
    }
}
