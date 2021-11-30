using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Teletransport : MonoBehaviour
{
    [SerializeField]
    private Transform alvo;
    [SerializeField]
    private Tilemap tileAlvo;
    [SerializeField]
    private Image fundoP;
    [SerializeField]
    private GameObject animatext;
    [SerializeField]
    private bool needName;
    
   private void Awake() {
       fundoP.enabled = false;
   }

    IEnumerator OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){

            fundoP.enabled = true;
            Animator anim = fundoP.GetComponent<Animator>();
            anim.Play("FundoAnim");
            other.GetComponent<MovPlayer>().enabled = false;
            other.GetComponent<Animator>().enabled = false;

            yield return new WaitForSeconds(1);
            other.transform.position = alvo.transform.GetChild(0).position;
            CameraFollow.instance.tileM = tileAlvo;
            CameraFollow.instance.StartMap();

            anim.Play("FundoAnim_inv");
            if(!needName)
                StartCoroutine(animatext.GetComponent<textFade>().MostraTexto(null));
            else
                StartCoroutine(animatext.GetComponent<textFade>().MostraTexto(tileAlvo.tag));

            other.GetComponent<MovPlayer>().enabled = true;
            other.GetComponent<Animator>().enabled = true;


        }
    }
}
