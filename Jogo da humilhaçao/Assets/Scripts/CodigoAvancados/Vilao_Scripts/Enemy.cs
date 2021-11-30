using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfosEnemyBase{
    public Vector3 posIni, target, dir;
    public float raioVisao, raioAtaque, fov, meshRes;
    public LayerMask layer;
    public GameObject player;
    public Rigidbody2D rb2D;
    public bool liberaR = false;
    public Animator enemyAnim;
    public MeshFilter viewMeshFilter;
    public Mesh viewMesh;
    public Transform[] anims;
    public Rigidbody2D bala;

}

public enum typeEnemy{
    atirador = 0,
    guerreiro = 1
}

public enum EnemyStats{
    idle,
    walk,
    attack,
    stagger
}

public abstract class Enemy : MonoBehaviour
{

   
    public float health;
    public string enemyName;
    public int baseAttack;
    public float speed;
    public FloatValue maxLife;

    public InfosEnemyBase infEn;
    public EnemyStats enemyStats;
    public typeEnemy tEnmy;

    private WaitForSeconds tempo = new WaitForSeconds(1.5f);
    private bool attack;


    private void Awake() {
        health = maxLife.intialValue;
    }



    public virtual void Start() {

        infEn.player = GameObject.FindWithTag("player");
        infEn.posIni = transform.position;
        infEn.rb2D = GetComponent<Rigidbody2D>();
        infEn.target = infEn.posIni;
        infEn.viewMesh = new Mesh();
        infEn.viewMeshFilter.mesh = infEn.viewMesh;


        setTypeEnemy((int)tEnmy);

    }
    
    public virtual void Update() {
        checkDinstanceAnim();
    }

// calcDano

    private void TakeDamage(float damage){
        health -= damage;
        if(health <=0)
            this.gameObject.SetActive(false);
    }
   
    public void Knock(Rigidbody2D objRb, float KnockTime, float damage){
        StartCoroutine(KnockCo(objRb,KnockTime));
        TakeDamage(damage);
    }    

    private IEnumerator KnockCo(Rigidbody2D objRb, float KnockTime){
        if(objRb != null){
            yield return new WaitForSeconds(KnockTime);
            objRb.velocity = Vector2.zero;
            objRb.GetComponent<Enemy>().enemyStats = EnemyStats.idle;
            // Debug.Log("log");
        }
    }

// calcDano

// barra De vida
    protected virtual void MostraBarEnemy(){
        
    }
// barra De vida


// selcionar o inimigo

    void setTypeEnemy(int t){
        if(t == 0){
            Transform auxEnemy = Instantiate(infEn.anims[0], transform, false) as Transform;
            auxEnemy.localPosition = new Vector3(0,0,0);
            infEn.enemyAnim = GetComponentInChildren<Animator>();
        }else if (t == 1){
            Transform auxEnemy = Instantiate(infEn.anims[1], transform, false) as Transform;
            auxEnemy.localPosition = new Vector3(0,0,0);
            infEn.enemyAnim = GetComponentInChildren<Animator>();
        }
    }

// selcionar o inimigo

    
// metodo pra desenha o campo de visão
    protected void DrawFieldOfView(){
        int stepCount = Mathf.RoundToInt(infEn.fov * infEn.meshRes);
        float stepAngle = infEn.fov /stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++){
            float angulo = transform.eulerAngles.y - infEn.fov /2 + stepAngle *i;
            ViewStruct auxView = View(angulo);

            Debug.DrawLine(transform.position, transform.position - DirFAngle(angulo) * infEn.raioVisao, Color.green);
            viewPoints.Add(auxView.pos);
        }

        int vertexCount = viewPoints.Count + 1;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int [(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;


        for (int i = 0; i < vertexCount - 1; i++){
            vertices[i] = transform.InverseTransformPoint(viewPoints[i]);

            if(i < vertexCount - 2){
                triangles[i * 3] = 0;
                triangles[i * 3+ 1] = i + 1;
                triangles[i * 3+ 2] = i + 2;
            }
        }

        infEn.viewMesh.Clear();
        infEn.viewMesh.vertices = vertices;
        infEn.viewMesh.triangles = triangles;
        infEn.viewMesh.RecalculateNormals();
    }

    private struct ViewStruct{
        public Vector3 pos;

        public ViewStruct(Vector3 pos){
            this.pos = pos;
        }
    }

    ViewStruct View(float globalAngle){
        Vector3 dir = -DirFAngle(globalAngle);

        return new ViewStruct(transform.position + dir * infEn.raioVisao);
    }
// metodo pra desenha o campo de visão

// moviementos
    public Vector3 DirFAngle(float angDeg){
        angDeg -=transform.eulerAngles.z;
        return new Vector3(Mathf.Sin(angDeg * Mathf.Deg2Rad), Mathf.Cos(angDeg * Mathf.Deg2Rad),0);
    }

    protected void RaioPlayer(){

        float distTemp = Vector3.Distance(infEn.target, transform.position);
        infEn.dir = (infEn.target - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,infEn.player.transform.position - transform.position, infEn.raioVisao , infEn.layer);
        Vector3 temp = infEn.player.transform.position - transform.position;
        Debug.DrawRay(transform.position, temp, Color.red);


        if(hit.collider != null){
            if(hit.collider.tag == "player" ||hit.collider.tag == "Tiro"){
                infEn.target = infEn.player.transform.position;
                infEn.liberaR = true;
            }else {
                infEn.target = infEn.posIni;
            }
        }else {
            infEn.target = infEn.posIni;
            infEn.liberaR = false;
        }
        if(infEn.target != infEn.posIni && distTemp < infEn.raioAtaque){
            // Ajusta animacoes de ataque 

            infEn.enemyAnim.SetBool("Ataque", true);

            if(!attack){
                StartCoroutine(Tiro());
            }

        }else {
            infEn.enemyAnim.SetBool("Ataque", false);
            infEn.rb2D.MovePosition(transform.position + infEn.dir * speed * Time.deltaTime );
        }

        Debug.DrawLine(transform.position, infEn.target, Color.green);

        if(infEn.liberaR){
            transform.up =(infEn.player.transform.position - transform.position)* -1;
            if(distTemp > infEn.raioAtaque){
                infEn.rb2D.MovePosition(transform.position + infEn.dir * speed * Time.deltaTime);
            }
        }else {
            transform.up = (infEn.posIni - transform.position) * -1;
        }
    }

    protected void voltar(){
        infEn.dir = (infEn.target - transform.position).normalized;
        float distTemp = Vector3.Distance(infEn.target , transform.position);   

        if (infEn.target == infEn.posIni && distTemp< 0.02f)
        {
            transform.position = infEn.posIni;
            transform.up = Vector3.zero;
            ChangeState(EnemyStats.idle);
            infEn.enemyAnim.SetBool("WakeUp",false);
        }

        if(transform.position  != infEn.posIni){
            infEn.rb2D.MovePosition(transform.position + infEn.dir * speed * Time.deltaTime);
            UptadadeAni(infEn.dir - transform.position);
        }else {
            infEn.dir.x = 0;
            infEn.dir.y = 0;
        }
    }

// moviementos

// animacao
    protected void SetAnimFloat(Vector2 vec){
        infEn.enemyAnim.SetFloat("x", vec.x);
        infEn.enemyAnim.SetFloat("y", vec.y);
    }

    protected void UptadadeAni(Vector2 dir){
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

    protected void ChangeState(EnemyStats newStats){

        if(enemyStats != newStats){
            enemyStats = newStats;
        }
    }

    protected void checkDinstanceAnim(){
        if(Vector3.Distance(infEn.target, transform.position) <= infEn.raioVisao && Vector3.Distance(infEn.target, transform.position) > infEn.raioAtaque){
            if(enemyStats == EnemyStats.idle || enemyStats == EnemyStats.walk && enemyStats != EnemyStats.stagger){

                Vector3 temp = Vector3.MoveTowards(transform.position, infEn.target, speed * Time.deltaTime);

                UptadadeAni(temp - transform.position);
                ChangeState(EnemyStats.walk);
                infEn.enemyAnim.SetBool("WakeUp",true);
            }
        }
    }
// animacao


// Tiro
    IEnumerator Tiro(){
        attack = true;
        Instantiate(infEn.bala, transform.position, Quaternion.identity);
        print("tiro");
        yield return tempo;
        attack = false;
    }
// Tiro



}