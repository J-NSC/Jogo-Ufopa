using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPots : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Destroy(){
        anim.SetBool("break", true);
        Destroy(gameObject,0.3f);
        NasCoin.inst.Nasc(transform.position);
    }
}
