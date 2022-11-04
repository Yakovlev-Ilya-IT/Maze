using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionArrow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CellDirections _direction;
    private bool _isEnable;

    public event Action<CellDirections> Clicked;

    public bool IsEnable => _isEnable;

    public void Initialize(bool enable)
    {
        _isEnable = enable;
        gameObject.SetActive(enable);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(_direction);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
