using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Itens", menuName = "Inventario/Item")]
public class Itens : ScriptableObject
{
    public string nome;
    public Sprite icone;

    public int id;
    
    public virtual void Usar(){
        Debug.Log("usando" + nome);
    }

    public void RemoveI_Inv(){
        InventarioCod.instance.RemoveI(this);
    }
}
