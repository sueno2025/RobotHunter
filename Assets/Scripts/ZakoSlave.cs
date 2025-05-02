using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoSlave : MonoBehaviour
{
    public Transform leader;
    public Vector3 offsetFromLeader; // 最初に決める相対位置
    public float followSpeed = 5f;

    void Update()
    {
        if (leader == null) return;

        Vector3 targetPos = leader.TransformPoint(offsetFromLeader); // リーダーのローカル座標系での目標位置
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}