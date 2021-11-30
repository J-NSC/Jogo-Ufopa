using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float raio = 0.5f;
    public bool isFocus = false;
    public bool interagiu = false;


    void Start()
    {
        
    }

    private void Update() {

        if(isFocus && !interagiu){
            InterecatM();
            interagiu = true;
        }
    }

    public virtual void InterecatM(){
        Debug.Log("interagindo cm " + transform.name);
    }

    public void OnFocus(){
        isFocus = true;
        interagiu = false;
    }
}
