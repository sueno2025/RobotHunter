using UnityEngine;
using UnityEngine.UI;

public class FloatMotion : MonoBehaviour
{
    [SerializeField] private float amplitude = 10f;  // 上下移動の幅
    [SerializeField] private float speed = 1f;       // 上下移動の速さ

    private RectTransform rectTransform;
    private Vector3 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * speed) * amplitude;
        rectTransform.anchoredPosition = startPos + new Vector3(0, offsetY, 0);
    }
}
