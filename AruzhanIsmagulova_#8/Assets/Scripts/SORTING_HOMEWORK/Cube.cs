using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private Color _color;
    private float _scaleY;
    public float speed;

    private void Awake()
    {
        //initialize color and scale randomly
        RandomizeColor();
        RandomizeScale();
    }

    private void RandomizeColor()
    {
        _color= Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gameObject.GetComponent<Renderer>().material.color=_color;
    }

    private void RandomizeScale()
    {
        _scaleY = Random.Range(1f, 30f);
        transform.localScale = new Vector3(transform.localScale.x, _scaleY, transform.localScale.z);
    }
}
