using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LogEnemy : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D enemyRb;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosistion;
    public Animator anim;

    void Start()
    {   
        enemyStats = EnemyStats.idle;
        enemyRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("player").transform;
    }

    void FixedUpdate()
    {
        checkDinstance();
    }

    void checkDinstance(){
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius){
            if(enemyStats == EnemyStats.idle || enemyStats == EnemyStats.walk && enemyStats != EnemyStats.stagger){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                UptadadeAni(temp - transform.position);
                enemyRb.MovePosition(temp);
                ChangeState(EnemyStats.walk);
                anim.SetBool("WakeUp",true);
            }
        }else if(Vector3.Distance(target.position, transform.position) >chaseRadius ){
            anim.SetBool("WakeUp", false);
            ChangeState(EnemyStats.idle);
        }
    }


    private void SetAnimFloat(Vector2 vec){
        anim.SetFloat("x", vec.x);
        anim.SetFloat("y", vec.y);
    }

    private void UptadadeAni(Vector2 dir){
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y)){
            if(dir.x > 0){
               SetAnimFloat(Vector2.right);

            }else if(dir.x < 0){
               SetAnimFloat(Vector2.left);

            }
        }else if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y)) {
            if(dir.y > 0){
               SetAnimFloat(Vector2.up);

            }else if(dir.y < 0){

               SetAnimFloat(Vector2.down);
            }
            
        }
    }

    private void ChangeState(EnemyStats newStats){
        if(enemyStats != newStats){
            enemyStats = newStats;
        }
    }


     private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
