using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VilaoVisao : Enemy
{

    public float raioVisao, raioAtaque;
    public LayerMask layer;
    public float fov;
    public Animator enemyAnim;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 posIni;

    [SerializeField]
    private Rigidbody2D rb2D;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private Vector3 dir;


    // capo visão
    
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public float meshRes;
    //  capo visão


    private bool liberaR = false;
    void Start()
    {
        enemyStats = EnemyStats.idle;
        player =GameObject.FindGameObjectWithTag("player");
        posIni = transform.position;
        rb2D = GetComponent<Rigidbody2D>();
        target = posIni;

        // 
        viewMesh = new Mesh();
        viewMeshFilter.mesh = viewMesh;
    }

    private void Update() {
       checkDinstanceAnim();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if((Vector2.Angle(player.transform.position - transform.position,-transform.up))<= fov * 0.5){
            RaioPlayer();
        }else {
            voltar();
        }

    }

    private void LateUpdate() {


        if(enemyStats == EnemyStats.walk){
            DrawFieldOfView();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
        Gizmos.DrawWireSphere(transform.position, raioAtaque);
    }

    void RaioPlayer(){

        float distTemp = Vector3.Distance(target, transform.position);
        dir = (target - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,player.transform.position - transform.position, raioVisao , layer);
        Vector3 temp = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, temp, Color.red);


        if(hit.collider != null){
            if(hit.collider.tag== "player"){
                target = player.transform.position;
                liberaR = true;
            }else {
                target = posIni;
            }
        }else {
            target = posIni;
            liberaR = false;
        }
        if(target != posIni && distTemp < raioAtaque){
            // Ajusta animacoes de ataque 

        }else {
            rb2D.MovePosition(transform.position + dir * speed * Time.deltaTime );
        }

        Debug.DrawLine(transform.position, target, Color.green);

        if(liberaR){
            transform.up =(player.transform.position - transform.position)* -1;
            if(distTemp > raioAtaque){
                rb2D.MovePosition(transform.position + dir * speed * Time.deltaTime);
            }
        }else {
            transform.up = (posIni - transform.position) * -1;
        }
    }


    public Vector3 DirFAngle(float angDeg){
        angDeg -=transform.eulerAngles.z;
        return new Vector3(Mathf.Sin(angDeg * Mathf.Deg2Rad), Mathf.Cos(angDeg * Mathf.Deg2Rad),0);
    }


    void voltar(){
        dir = (target - transform.position).normalized;
        float distTemp = Vector3.Distance(target , transform.position);   

        if (target == posIni && distTemp< 0.02f)
        {
            transform.position = posIni;
            transform.up = Vector3.zero;
            ChangeState(EnemyStats.idle);
            enemyAnim.SetBool("WakeUp",false);
        }

        if(transform.position  != posIni){
            rb2D.MovePosition(transform.position + dir * speed * Time.deltaTime);
            UptadadeAni(dir - transform.position);
        }else {
            dir.x = 0;
            dir.y = 0;
        }
    }

     public void checkDinstanceAnim(){
        if(Vector3.Distance(target, transform.position) <= raioVisao && Vector3.Distance(target, transform.position) > raioAtaque){
            if(enemyStats == EnemyStats.idle || enemyStats == EnemyStats.walk && enemyStats != EnemyStats.stagger){

                Vector3 temp = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                UptadadeAni(temp - transform.position);
                ChangeState(EnemyStats.walk);
                enemyAnim.SetBool("WakeUp",true);
            }
        }
    }

// Animaçoes 

   

    private void SetAnimFloat(Vector2 vec){
        enemyAnim.SetFloat("x", vec.x);
        enemyAnim.SetFloat("y", vec.y);
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

// Animaçoes 


// metodo pra desenha o campo de visão
    void DrawFieldOfView(){
        int stepCount = Mathf.RoundToInt(fov * meshRes);
        float stepAngle = fov /stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++){
            float angulo = transform.eulerAngles.y - fov /2 + stepAngle *i;
            ViewStruct auxView = View(angulo);

            Debug.DrawLine(transform.position, transform.position - DirFAngle(angulo) * raioVisao, Color.green);
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

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }



    public struct ViewStruct{
        public Vector3 pos;

        public ViewStruct(Vector3 pos){
            this.pos = pos;
        }
    }

    ViewStruct View(float globalAngle){
        Vector3 dir = -DirFAngle(globalAngle);

        return new ViewStruct(transform.position + dir * raioVisao);
    }
// metodo pra desenha o campo de visão

}
