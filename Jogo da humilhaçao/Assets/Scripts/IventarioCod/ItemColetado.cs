using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemColetado : Interact
{
    public Itens item;
    public CircleCollider2D col;
    public bool foiPego;


    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = raio;
        col.isTrigger = true;
        gameObject.tag = "item";
    }

    public override void InterecatM()
    {
        base.InterecatM();
        Coleta();
    }

    void Coleta(){
        Debug.Log("Pegando" + item.nome);

        foiPego = InventarioCod.instance.AdicionaItem(item);

        if(foiPego){
            Destroy(gameObject);
        }
    }
}
