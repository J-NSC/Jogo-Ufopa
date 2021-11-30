using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textFade : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator MostraTexto(string nome){
        anim.Play("Text_Anim");
        txt.text = nome;
        yield return new WaitForSeconds(1);
        anim.Play("Text_Anim2");

    }
}
