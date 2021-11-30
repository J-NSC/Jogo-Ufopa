using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreasureChests : Interactable
{

    public ItemYo contents;
    public InventoryYo playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && playerInRange){
            if(!isOpen){
                // abri o bau
                OpenBau();
            }else{
                ChestAlreadyOpen();
            }
        }
    }


    public void OpenBau(){
        // dialog window on
        // dialog text = contenst.text;
        // adiciona no invetario
        anim.SetBool("Abrindo", true);
        dialogBox.SetActive(true);
        dialogText.text = contents.ItemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        isOpen = true;
        contextOn.Raise();
    }

    public void ChestAlreadyOpen(){
        
        dialogBox.SetActive(false);
        raiseItem.Raise();
        contextOff.Raise(); 
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player") && !other.isTrigger && !isOpen){
            contextOn.Raise();
            playerInRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("player") && !other.isTrigger && !isOpen){
            contextOff.Raise();
            playerInRange = false;
            
        }
    }
}
