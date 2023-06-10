using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private RectTransform rectTransform;
    private Vector3 normalScale;
    [SerializeField] AudioSource buttonSound;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        normalScale = rectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Boyutu küçült
        rectTransform.localScale = normalScale * 0.9f;
        buttonSound.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Eski boyuta geri getir
        rectTransform.localScale = normalScale;
    }
}
