using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabanga : MonoBehaviour
{


    void Start()
    {
        DelegateExe.ordemDoChefe += RecebeOrdem;
    }

    void Update()
    {
        
    }

    void RecebeOrdem(string s){
        print("o chefe disse : " + s);
    }
}
