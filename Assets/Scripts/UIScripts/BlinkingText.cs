using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float blinkSpeed = 1.5f;
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color highlightColor = new Color(0.8f, 0.8f, 0.8f); // ホバー時
    [SerializeField] private Color clickedColor = new Color(0.5f, 0.5f, 0.5f);   // クリック時

    private TextMeshProUGUI tmp;
    private bool isHovered = false;
    private bool isClicked = false;
    private float clickResetTime = 0.2f;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Color targetColor = baseColor;

        if (isClicked)
        {
            targetColor = clickedColor;
        }
        else if (isHovered)
        {
            targetColor = highlightColor;
        }

        float alpha = (Mathf.Sin(Time.time * blinkSpeed) * 0.5f) + 0.5f;
        tmp.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = true;
        Invoke(nameof(ResetClick), clickResetTime);
    }

    private void ResetClick()
    {
        isClicked = false;
    }
}