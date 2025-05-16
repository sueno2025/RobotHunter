using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRoll : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f; // 回転速度（度/秒）

    private void Update()
    {
        if (RenderSettings.skybox.HasProperty("_Rotation"))
        {
            float rotation = Time.time * rotationSpeed;
            RenderSettings.skybox.SetFloat("_Rotation", rotation % 360f);
        }
    }
}

