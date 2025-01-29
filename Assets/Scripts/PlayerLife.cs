using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    bool dead = false;

    [SerializeField] AudioSource deathSound;

    private void Update()
    {
        // When the player dies by falling off the platform (watch for the Y position at a certain threshold)
        if (transform.position.y <= -8f && !dead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            Die();            
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 0.8f);
        dead = true;
        deathSound.Play();
    }

    void ReloadLevel()
    {
        // To reload the level you have to access Unity's scene manager using the namespace `using UnityEngine.SceneManagement;` at the top of the page
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
