using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sing : Interactable
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && playerInRange){
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }   
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){
            contextOff.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
