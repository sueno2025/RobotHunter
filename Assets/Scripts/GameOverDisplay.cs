using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverDisplay : MonoBehaviour
{
    public CharacterMovement player;
    public Image gameOverImage;

    bool alreadyShown = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverImage.color = new Color(1, 1, 1, 0); // 最初は透明
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead && !alreadyShown)
        {
            alreadyShown = true;

            // Alpha 0 → 1 を1秒かけてフェードイン
            gameOverImage.DOFade(1f, 1f).SetDelay(0.8f)
                .SetEase(Ease.InOutQuad);
        }
    }
}