using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public Signal contextOn;
    public Signal contextOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){
            contextOn.Raise();
            playerInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){
            contextOff.Raise();
            playerInRange = false;
            
        }
    }
}
