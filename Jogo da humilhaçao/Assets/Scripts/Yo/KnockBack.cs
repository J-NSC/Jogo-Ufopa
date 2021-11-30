using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float impulso;
    public float KnockTime;
    public float damage;
    public float damagePlayer;
    void Start()
    {
       damagePlayer =  PlayerClass.inst.dano;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("quebravel") && this.gameObject.CompareTag("player")){
            other.GetComponent<BreakPots>().Destroy();
        }

        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {

                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * impulso;
                hit.AddForce(difference, ForceMode2D.Impulse);
                
              
                if(other.gameObject.CompareTag("enemy") && other.isTrigger){
                    hit.GetComponent<Enemy>().enemyStats = EnemyStats.stagger;
                    other.GetComponent<Enemy>().Knock(hit,KnockTime,damagePlayer);
                    Debug.Log("damage");

                }

                if(other.gameObject.CompareTag("player")){
                    if(other.gameObject.GetComponent<MovPlayer>().currentState != PlayerState.stagger){
                        hit.gameObject.GetComponent<MovPlayer>().currentState = PlayerState.stagger;
                        other.GetComponent<MovPlayer>().Knock(KnockTime, damage);
                        Debug.Log(damage);
                    }
                   
                }
         
            }
        }
    }


}
