// Philip Gergis

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// deletes bullets on contact to prevent them from existing forever
public class BulletDeleter : MonoBehaviour
{
    [SerializeField]
    private float duration = 5f; // duration before bullet is deleted

    [SerializeField]
    private Transform cord; // first point rectangle for vision

    [SerializeField]
    private Transform cord2;  // second point rectangle for vision

    // deletes bullets in the rectangle made by the coords
    private void Update()
    {
        Collider2D[] collisions = Physics2D.OverlapAreaAll(cord.position, cord2.position); // all objects in area
        // deletes object if it is a bullet
        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Bullet")
            {
                StartCoroutine(DelayedDestroy(coll));
            }
        }
    }

    private IEnumerator DelayedDestroy(Collider2D collision)
    {
        // disable bullet
        collision.GetComponent<SpriteRenderer>().enabled = false;
        collision.GetComponent<Collider2D>().enabled = false;

        // after an amount of time, destroys the object it collided with
        yield return new WaitForSeconds(duration);
        if(collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
