using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundsController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] SoundController _soundController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _soundController.PlayOnMouseClickUISound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _soundController.PlayOnMouseOverUISound();
    }
}
