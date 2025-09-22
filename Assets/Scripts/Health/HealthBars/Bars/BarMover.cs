using UnityEngine;

public class BarMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();
    }

    private void FixedUpdate()
    {
        if (_canvas == null)
            return;

        _canvas.transform.position = _target.position + _offset;
    }
}
