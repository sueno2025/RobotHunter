using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float blinkSpeed = 1.5f;
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color highlightColor = new Color(0.8f, 0.8f, 0.8f);

    private TextMeshProUGUI tmp;
    private bool isHovered = false;

    private void OnEnable()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (tmp == null) return;

        Color targetColor = isHovered ? highlightColor : baseColor;
        float alpha = (Mathf.Sin(Time.unscaledTime * blinkSpeed) * 0.5f) + 0.5f;
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
}
