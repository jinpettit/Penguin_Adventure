using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollider : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1")
        {
            //what will happen when player enters fish zone

            audioSource.Play();
            Destroy(gameObject);
            FishCounter.instance.IncreaseFishCount(1);
        }
    }
}
