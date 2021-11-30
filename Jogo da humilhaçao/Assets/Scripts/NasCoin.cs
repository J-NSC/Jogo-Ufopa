using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasCoin : MonoBehaviour
{
    public static NasCoin inst;
    [SerializeField]
    private GameObject rup;
    [SerializeField]
    private int nasc = 0;

  


    private void Awake() {
        if(inst == null){
            inst = this;
        }
    }

    
  

    public void Nasc(Vector3 pos){
        nasc = Random.Range(0,3);
        if(nasc == 1){
            Instantiate(rup,pos,Quaternion.identity);
        }
    }
}
