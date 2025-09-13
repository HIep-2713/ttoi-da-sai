using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float scaleFactor = 0.9f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
