using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryYo : ScriptableObject
{
    

    public ItemYo currentItem;
    public List<ItemYo> items = new List<ItemYo>();
    public int numberOfKey;

    public void AddItem(ItemYo itemToAdd){
        if(itemToAdd.isKey){
            numberOfKey++;
        }else{
            if(!items.Contains(itemToAdd)){
                items.Add(itemToAdd);
            }
        }
    }
}
