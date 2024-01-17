using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private RectTransform _outerCirle;
    [SerializeField] private RectTransform _innerCircle;
    [SerializeField] private Camera cam;
    private Vector2 _firstPoint;
    private Vector2 _secondPoint;
    private bool _startMoving=false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,cam.transform.position.z));
            
            _innerCircle.transform.position = cam.WorldToScreenPoint((_firstPoint*-1));
            _outerCirle.transform.position = cam.WorldToScreenPoint((_firstPoint*-1));
        }

        if (Input.GetMouseButton(0))
        {
            _startMoving = true;
            _secondPoint= Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cam.transform.position.z));
        }
        else
        {
            _startMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (_startMoving)
        {
            var offset = _secondPoint - _firstPoint;
            Vector3 direction = Vector3.ClampMagnitude(offset, 1.0f);
            PlayerController.Player.MovePlayer(direction*-1);
            _innerCircle.transform.position = cam.WorldToScreenPoint(new Vector2(_firstPoint.x+direction.x, _firstPoint.y+direction.y)*-1);
            
        }
    }
}