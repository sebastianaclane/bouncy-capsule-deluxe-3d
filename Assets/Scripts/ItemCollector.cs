using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    int coins = 0;

    [SerializeField] Text coinsText;

    [SerializeField] AudioSource collectionSound;

    private void OnTriggerEnter(Collider other)
    {
        // OnCollisionEnter() works very similar to OnTriggerEnter()
        // OnCollisionEnter() works for collision colliders (when you set "Is Trigger" checkbox to false)
        // OnTriggerEnter() works for trigger colliders (when you set "Is Trigger" checkbox to true)

        // destroy the coin and increment a coin counter
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.text = "Coins: " + coins;
            collectionSound.Play();
        }
    }
}
