using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageParallax : MonoBehaviour
{
    private RectTransform _transform;
    [SerializeField]
    private float _shiftAmount;
    private float _originalX;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _originalX = _transform.position.x;
    }

    void Update()
    { 
        float delta = Input.mousePosition.x / Screen.width;
        _transform.position = new Vector3(_originalX + ((delta - .5f) * _shiftAmount), transform.position.y, transform.position.z);
    }
}
