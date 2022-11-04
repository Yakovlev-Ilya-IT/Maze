using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementControlDisplay : MonoBehaviour, IMazeMovementControl
{
    private Transform _target;
    private readonly Vector3 _offset = Vector3.up / 10f;

    private Dictionary<CellDirections, DirectionArrow> _directionArrows;

    public event Action<CellDirections> DirectionSelected;

    private bool _isVisible;

    public virtual void Initialize(Transform target)
    {
        _directionArrows = GetDirectionArrows();
        _target = target;
    }

    protected abstract Dictionary<CellDirections, DirectionArrow> GetDirectionArrows();

    public void Hide()
    {
        if (!_isVisible)
        {
            Debug.LogError("control is already hidden");
            return;
        }

        foreach (KeyValuePair<CellDirections, DirectionArrow> arrow in _directionArrows)
        {
            if (arrow.Value.IsEnable)
            {
                arrow.Value.Hide();
                arrow.Value.Clicked -= OnDirectionArrowsClicked;
            }
        }  

        gameObject.SetActive(false);
        _isVisible = false;
    }

    private void Update()
    {
        if (_isVisible)
            SetToTarget();
    }

    public void Show(Dictionary<CellDirections, bool> currentCellWalls)
    {
        if (_isVisible)
        {
            Debug.LogError("control is already shown");
            return;
        }

        SetToTarget();
        gameObject.SetActive(true);
        _isVisible = true;

        foreach (KeyValuePair<CellDirections, DirectionArrow> arrow in _directionArrows)
        {
            if (currentCellWalls.ContainsKey(arrow.Key))
            {
                if (currentCellWalls[arrow.Key])
                {
                    arrow.Value.Initialize(false);
                    continue;
                }

                arrow.Value.Initialize(true);
                arrow.Value.Show();
                arrow.Value.Clicked += OnDirectionArrowsClicked;
            }
            else
            {
                Debug.LogError("no direction arrow needed");
            }
        }
    }

    private void OnDirectionArrowsClicked(CellDirections direction)
    {
        DirectionSelected?.Invoke(direction);
    }

    private void SetToTarget() => transform.position = _target.position + _offset;

    public void Disable()
    {
        Destroy(gameObject);
    }
}
