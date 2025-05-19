using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioClip bgm;
    public GameObject bossRobot;
    public GameObject objectGenerator;
    public CharacterMovement playerController;
    public UIManager2 ui;
    //05/19追記
    public static int playerLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        SoundManager.Instance.playBGM(bgm, 0.7f);
        bossRobot.SetActive(false);
        objectGenerator.SetActive(true);
        playerController = FindObjectOfType<CharacterMovement>();
        //05/19追記
        playerLevel = 0;
    }
    void Update()
    {
        if (playerController.IsDead)
        {
            IsGameOver();
        }
    }

    //敵生成ストップとボスの登場
    public void OnRockGenerationComplete()
    {
        Debug.Log("岩生成終了");
        objectGenerator.SetActive(false);
        StartCoroutine(ActivateBossAfterDelay(2f));
    }
    //ボスの登場を遅らせる処理
    private IEnumerator ActivateBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        bossRobot.SetActive(true);
    }
    public void OnBossDefeated()
    {
        Debug.Log("Boss撃破");
    
        BulletShooter bullet = playerController.GetComponent<BulletShooter>();
        //発射を無効化
        bullet.enabled = false;
        StartCoroutine(DelayGameClearUI());
    }
    private IEnumerator DelayGameClearUI()
    {
        yield return new WaitForSeconds(1.5f);
        ui.ShowGameClear();
    }
    public void IsGameOver()
    {
        if (playerController.IsDead)
        {
            StartCoroutine(DelayGameOverUI());
        }

    }
    private IEnumerator DelayGameOverUI()
    {
        yield return new WaitForSeconds(0.8f);
        ui.ShowGameOver();
    }
}
