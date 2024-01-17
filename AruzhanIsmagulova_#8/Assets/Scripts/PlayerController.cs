using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _movementSpeed;
    private float _horizontalInput;
    private float verticalInput;
    public static PlayerController Player { get; private set; }

    private void Awake()
    {
        Player = this;
    }
    // Update is called once per frame
        
    public void MovePlayer(Vector3 direction)
    {
        Vector3 movementDirection = new Vector3(direction.x, 0, direction.y);
        movementDirection.Normalize();
        Vector3 movement = Vector3.MoveTowards(transform.position, transform.position + movementDirection,
            Time.deltaTime * _movementSpeed);
        
        transform.position = movement;
        RotatePlayer(movement);
    }

    public void RotatePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            Debug.Log(rotation);
        }
    }
}
