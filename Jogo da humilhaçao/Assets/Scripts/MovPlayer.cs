using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dano*(vida/(vida+def))
public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class MovPlayer : MonoBehaviour
{
    public PlayerState currentState;
    public float speed; 
    private Animator anim;
    private Vector3 dir;
    private Rigidbody2D playerRb;
    private Vector2 direcaoPlayer;
    private SpriteRenderer playerR;
    private bool liberaCOr = false;
    public FloatValue currentHealth;
    public Signal playerHealth;
    public InventoryYo playerInventory;
    public SpriteRenderer receivedItem;

    void Start()
    {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        playerR = GetComponent<SpriteRenderer>();

        anim.SetFloat("x", 0);
        anim.SetFloat("y", -1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == PlayerState.interact){
            return ;
        }
        // inputPersonagem();
        dir = Vector3.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        if(Input.GetMouseButtonDown(0) && currentState != PlayerState.attack && currentState != PlayerState.stagger){
            StartCoroutine(Ataque());
        }

        else if(currentState == PlayerState.walk || currentState == PlayerState.idle){
            MovP();
            uptadeAnimationMov();
        }



        if(liberaCOr == true){
            playerR.color = Color.Lerp(Color.white , Color.red, Mathf.PingPong(8 *Time.deltaTime, 0.5f));
        }
      
    }

    void uptadeAnimationMov(){
        if(dir != Vector3.zero){
            anim.SetFloat("x",dir.x);
            anim.SetFloat("y",dir.y);
            anim.SetBool("walking", true);
        }else{
            anim.SetBool("walking",false);
        }
    }

    public IEnumerator Ataque(){
        anim.SetBool("Ataque", true); 
        currentState = PlayerState.attack;
        yield return null;
        anim.SetBool("Ataque",false);
        yield return new WaitForSeconds(.3f);

        if(currentState != PlayerState.interact)
            currentState = PlayerState.walk;

    }

    public void RaiseItem(){
        if(playerInventory.currentItem != null){
            if(currentState != PlayerState.interact){
                anim.SetBool("receberItem", true);
                currentState = PlayerState.interact;
                receivedItem.sprite = playerInventory.currentItem.itemSprite;
            }else{
                anim.SetBool("receberItem", false);
                currentState = PlayerState.idle;
                receivedItem.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    public void MovP(){
        dir.Normalize();
        playerRb.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){
            // StartCoroutine(KnockBack(1f, 50, direcaoPlayer));
            danoCor();
        }
    }

    public void Knock( float KnockTime, float damage){

        currentHealth.RuntimeValue-= damage;
        playerHealth.Raise();
        if(currentHealth.RuntimeValue > 0){
            StartCoroutine(KnockCo(KnockTime));
        }else {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float KnockTime){
        if(playerRb != null && currentState == PlayerState.stagger){
            yield return new WaitForSeconds(KnockTime);
            playerRb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRb.velocity = Vector2.zero;
        }
    }


    void danoCor(){
        liberaCOr = true;
        StartCoroutine(liberaCor());
    }

    IEnumerator liberaCor(){
        yield return new WaitForSeconds(0.5f);
        liberaCOr = false;
        playerR.color = new Color(1,1,1,1);
    }


}
