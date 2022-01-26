using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyLog : Enemy
{
    // [SerializeField]
    // private CanvasGroup  cGroup;
    // [SerializeField]
    // private Image barLife;
    // Start is called before the first frame update
    
    
    public override void Start()
    {
        base.Start();
        // cGroup = GetComponentInChildren<CanvasGroup>();
        // barLife = transform.Find(getEnemy().name).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((Vector2.Angle(infEn.player.transform.position - transform.position,-transform.up))<= infEn.fov * 0.5){
            RaioPlayer();
        }else {
            voltar();
        }
        
        MostraBarEnemy();

    }

    private void LateUpdate() {


        DrawFieldOfView();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, infEn.raioVisao);
        Gizmos.DrawWireSphere(transform.position, infEn.raioAtaque);
        
    }

  
    #region barra de vida do enemy
    // protected override void MostraBarEnemy()
    // {
    //     if(Vector2.Distance(transform.position, infEn.player.transform.position) < infEn.raioVisao && cGroup.alpha == 0){
    //         cGroup.alpha = 1;
    //     }else if(Vector2.Distance(transform.position, infEn.player.transform.position) > infEn.raioVisao && cGroup.alpha == 1) {
    //         cGroup.alpha = 0;
    //     }
    // }


    // public void uiVida(){
    //     barLife.fillAmount -= (health * 0.01f);
    // }

    #endregion



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "player"){
            transform.up = (infEn.player.transform.position - transform.position) * -1;
        }
    }

}