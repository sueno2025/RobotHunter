using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class ObjectGenerator : MonoBehaviour
{
    //Zako関連
    public GameObject zakoSlavePrefab;
    public int frontRowCount;      // リーダーの左右に何体出すか（合計: 1 + 2*この数 + リーダー）
    public int rearColumns;        // 後列の横数
    public int rearRows;
    public float spacingX = 2f;
    public float spacingZ = 2f;

    public float generateInterval = 2f;
    public GameObject Zako;
    public float minX = -5f;
    public float maxX = 5f;
    public float y = 5f;
    public float generateTime = 5f;
    public float stopTime = 2f;

    //岩関連
    public GameObject RocksPrefab;
    public float rocksGenerateInterbal = 10f;
    Vector3 rockSpawnPosition;
    



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateLoop());
        StartCoroutine(SpawnRocks());
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
        frontRowCount = Random.Range(1, 3);      
        rearColumns = Random.Range(2, 5);        
        rearRows = Random.Range(2, 5);
        float randomX = Random.Range(minX, maxX);
        Vector3 leaderPos = new Vector3(randomX, y, 0f);

        // Zako（リーダー）生成
        GameObject zakoObj = Instantiate(Zako, leaderPos, Quaternion.identity);
        Transform zakoTransform = zakoObj.transform;

        // 【前列】リーダーと同じZ位置、左右に展開
        for (int i = -frontRowCount; i <= frontRowCount; i++)
        {
            if (i == 0) continue; // 真ん中はリーダー本人

            GameObject slaveObj = Instantiate(zakoSlavePrefab);
            ZakoSlave slaveScript = slaveObj.GetComponent<ZakoSlave>();
            slaveScript.leader = zakoTransform;

            Vector3 offset = new Vector3(i * spacingX, 0f, 0f);
            slaveScript.offsetFromLeader = offset;
            slaveObj.transform.position = zakoTransform.TransformPoint(offset);
        }

        // 【後列】ZakoのZ後方に格子状に配置
        for (int z = 1; z <= rearRows; z++)
        {
            for (int x = -rearColumns / 2; x <= rearColumns / 2; x++)
            {
                GameObject slaveObj = Instantiate(zakoSlavePrefab);
                ZakoSlave slaveScript = slaveObj.GetComponent<ZakoSlave>();
                slaveScript.leader = zakoTransform;

                Vector3 offset = new Vector3(x * spacingX, 0f, -z * spacingZ);
                slaveScript.offsetFromLeader = offset;
                slaveObj.transform.position = zakoTransform.TransformPoint(offset);
            }
        }
    }
    IEnumerator SpawnRocks(){
        while(true){
            SpawnRock();
            yield return new WaitForSeconds(rocksGenerateInterbal);
        }
    }
    void SpawnRock(){
        rockSpawnPosition = new Vector3(3.3f,10,-100);
        Instantiate(RocksPrefab,rockSpawnPosition,Quaternion.identity);
    }

}
