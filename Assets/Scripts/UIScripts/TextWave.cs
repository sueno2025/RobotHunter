using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextWave : MonoBehaviour
{
    public float waveFrequency = 2f; // 波の周波数
    public float waveAmplitude = 5f; // 波の振幅
    public float waveSpeed = 2f;     // 波の速さ

    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] vertices;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.ForceMeshUpdate(); // メッシュの強制更新
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        int characterCount = textMesh.textInfo.characterCount;

        for (int i = 0; i < characterCount; i++)
        {
            var charInfo = textMesh.textInfo.characterInfo[i];

            if (!charInfo.isVisible) continue;

            int vertexIndex = charInfo.vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 offset = new Vector3(
                    0,
                    Mathf.Sin(Time.time * waveSpeed + (charInfo.origin * waveFrequency)) * waveAmplitude,
                    0);
                vertices[vertexIndex + j] += offset;
            }
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
}
