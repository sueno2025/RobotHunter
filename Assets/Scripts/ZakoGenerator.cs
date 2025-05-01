using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ZakoGenerator : MonoBehaviour
{
    public float generateInterval = 2f;
    public GameObject Zako;
    public float minX = -5f;
    public float maxX = 5f;
    public float y = 0f;
    public float generateTime = 5f;
    public float stopTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator GenerateLoop()
    {
        while (true)
        {
            float t = 0;
            while (t < generateTime)
            {
                Generate();
                yield return new WaitForSeconds(generateInterval);
                t += generateInterval;
            }
            yield return new WaitForSeconds(stopTime);
        }
    }
    void Generate()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, y, 0f);
        Instantiate(Zako, spawnPos, Quaternion.identity);
    }

}
