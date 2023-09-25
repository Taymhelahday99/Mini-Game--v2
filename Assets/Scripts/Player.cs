using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);

        // Normalize the movement vector to prevent faster diagonal movement
        movement.Normalize();

        // Move the player using Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;
    }
}
