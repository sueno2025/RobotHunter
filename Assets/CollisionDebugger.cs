using UnityEngine;

public class CollisionDebugger : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("衝突検出: " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("トリガー検出: " + other.gameObject.name);
    }
}
