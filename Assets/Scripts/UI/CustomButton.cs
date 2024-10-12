using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private bool isMouseOver;

    [Header("References")]
    public Image border;
    public Image fill;
    public TMP_Text text;

    [Header("Actions")]
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;
    public UnityEvent onClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        onPointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        onPointerExit?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}