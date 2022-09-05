using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
//[ExecuteAlways] 
public class Role : MonoBehaviour
{
    public bool bMove = false;
    public Vector3 lastPos = Vector3.zero;
    public float moveSpeed = 5;
    public float rotSpeed = 3;

    public Camera mCamera;

    private void Awake()
    {
        mCamera = transform.Find("Camera").GetComponent<Camera>();
    }

    
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            transform.localEulerAngles -= new Vector3(0, rotSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            transform.localEulerAngles += new Vector3(0, rotSpeed, 0);
        }

        if (lastPos != transform.position)
        {
            bMove = true;
            lastPos = transform.position;
        }
        else
        {
            bMove = false;
        }
        
    }
    

    private void OnDrawGizmos()//根据摄像机的投影模式来画出摄像机的投射范围
    {
        Gizmos.color = Color.yellow;
        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);//创建一个基本矩阵
        if (mCamera.orthographic)
        {
            float spread = mCamera.farClipPlane - mCamera.nearClipPlane;
            float center = (mCamera.farClipPlane + mCamera.nearClipPlane) * 0.5f;
            //画一个线框
            Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(mCamera.orthographicSize * 2 * mCamera.aspect, mCamera.orthographicSize * 2, spread));
        }
        else
        {
            //使用当前设置的gizmos.matrix绘制相机平截头体的位置
            Gizmos.DrawFrustum(Vector3.zero, mCamera.fieldOfView, mCamera.farClipPlane, mCamera.nearClipPlane, mCamera.aspect);
        }
        //设置用于绘制所有小控件的小控件矩阵
        Gizmos.matrix = temp;
    }
}
