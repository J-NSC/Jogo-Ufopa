using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioCod : MonoBehaviour
{


    public static InventarioCod instance;

    public GameObject player;
    public GameObject[]objectCol;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    public delegate void ItemAlterado();
    public event ItemAlterado ItemAlteradoE;
    [SerializeField]
    private int quantiSlots;
    public List<Itens> itens = new List<Itens>();


    public bool AdicionaItem(Itens i){

        if(itens.Count >= quantiSlots){
            print("sem espaco");
            return false;
        }

        itens.Add(i);
        Debug.Log(i.id);

        if(ItemAlteradoE !=null){
            ItemAlteradoE();
        }

        return true;
    }

    public void RemoveI(Itens i){
        itens.Remove(i);

        Debug.Log(i.id);
        Instantiate(objectCol[i.id], new Vector2(player.transform.position.x + Random.Range(-2,2),player.transform.position.y + Random.Range(-2,2)), Quaternion.identity);

        if(ItemAlteradoE !=null){
            ItemAlteradoE();
        }
        
    }

    public void RemoveI(Itens i, bool usando){
        if(usando){
            itens.Remove(i);

            if(ItemAlteradoE != null){
                ItemAlteradoE();
            }
        }
    }


}
