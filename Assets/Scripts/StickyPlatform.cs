using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // we want to detect whenever the player is touching the moving platform by seeing if the colliders are touching each other
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // set the player to be a child of the moving platform
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // remove the player as a child of the moving platform
            collision.gameObject.transform.SetParent(null);
        }
    }
}
