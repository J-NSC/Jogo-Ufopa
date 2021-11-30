using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioSlots : MonoBehaviour
{
    public Image iconeS; 
    public Button removeBtn;
    public Itens item;
    public Heart heart;


    // vida 
    // public FloatValue playerHealth;
    // public FloatValue heartContainers;
    // public float amountToIncrease;
    // vida


    private void Start() {
        iconeS = transform.GetChild(1).GetComponent<Image>();
        removeBtn = transform.GetChild(0).GetComponent<Button>();
    }


    public void AdicionaItem(Itens i){
        item = i;
        iconeS.sprite = item.icone;
        iconeS.enabled = true;
        removeBtn.interactable = true;
    }

    public void LimpaSlot(){
        item = null;
        iconeS.sprite = null;
        iconeS.enabled = false;
        removeBtn.interactable = false;
    }

    public void ChamaRemoveBtn(){
        InventarioCod.instance.RemoveI(item);
    }

    public void UsaItem(){
        if(item != null){
            item.Usar();
            
            heart.Healer();

            
            InventarioCod.instance.RemoveI(item,true);
        }
    }
}
