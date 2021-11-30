using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClasseDestrutiva : MonoBehaviour
{   
    public float resAtual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void AdicionaDano(float dano){

        resAtual -= dano;

        if(resAtual <= 0){
           StartCoroutine(Destruido());
        }
    }

    public abstract IEnumerator Destruido();
}
