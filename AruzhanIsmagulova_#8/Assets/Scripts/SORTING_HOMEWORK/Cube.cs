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
        RandomizeColor();
        RandomizeScale();
    }

    private void RandomizeScale()
    {
        _color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
        transform.localScale = new Vector3(transform.localScale.x, _scaleY, transform.localScale.z);
    }

    private void RandomizeColor()
    {
        _scaleY = Random.Range(1f, 30f);
        gameObject.GetComponent<Renderer>().material.color=_color;
    }
}
