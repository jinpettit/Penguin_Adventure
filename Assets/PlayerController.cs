using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "FallDetector"){
            //what will happen when player enters fall detector zone
            transform.position = respawnPoint;
        }
        else if(other.tag == "Checkpoint"){
            //what will happen when player enters checkpoint zone
            respawnPoint = other.transform.position;
        }
    }
}
