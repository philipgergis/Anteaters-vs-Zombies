// Amanda

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// keeps players from leaving the screen
public class PlayerBoundaries : MonoBehaviour
{
    // object's rigid body
    private Rigidbody2D playerBody;

    // upper limit
    [SerializeField] float upperY;

    // lower limit
    [SerializeField] float lowerY;

    // right limit x
    [SerializeField] float rightX = 8.5f;

    // left limit x
    [SerializeField] float leftX = -8.5f;

    // retrieves the rigid body on the player
    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // if they pass a cordinate, they get moved back to where they were before
    void FixedUpdate()
    {
        if (transform.position.y < lowerY)
        {
            transform.position = new Vector3(transform.position.x, lowerY, transform.position.z);
        }
        else if (playerBody.position.y > upperY)
        {
            transform.position = new Vector3(transform.position.x, upperY, transform.position.z);

        }
        else if (playerBody.position.x < leftX)
        {
            transform.position = new Vector3(leftX, transform.position.y, transform.position.z);

        }
        else if (playerBody.position.x > rightX)
        {
            transform.position = new Vector3(rightX, transform.position.y, transform.position.z);
        }
    }
}
