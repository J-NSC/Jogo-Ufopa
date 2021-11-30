using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public Transform bag;
    public GameObject invUI;
    public InventarioSlots[] slots;


    void Start()
    {
        InventarioCod.instance.ItemAlteradoE += UIMetodo ;
        slots = bag.GetComponentsInChildren<InventarioSlots>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UIMetodo(){
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < InventarioCod.instance.itens.Count){
                slots[i].AdicionaItem(InventarioCod.instance.itens[i]);
            }else {
                slots[i].LimpaSlot();
            }
        }
    }
}
