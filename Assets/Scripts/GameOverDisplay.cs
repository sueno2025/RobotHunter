using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    public CharacterMovement player;
    public UIManager uiManager; // ← UIManager への参照を追加

    private bool alreadyShown = false;

    void Update()
    {
        if (player.IsDead && !alreadyShown)
        {
            alreadyShown = true;

            // UIManager に処理を任せる
            uiManager.ShowGameOverUI();
        }
    }
}