using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovBalaEn : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bala;
    private float speed;
    private Transform target;
    private Vector2 dir;
    private float angulo;

    void Start()
    {
        bala = GetComponent<Rigidbody2D>();    
        target = GameObject.FindWithTag("player").GetComponent<Transform>();
        speed = 4;
        dir = target.position - transform.position;
        angulo = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        transform.rotation =  Quaternion.AngleAxis(angulo, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        bala.velocity = dir.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("player")){
            Destroy(gameObject);
        }
    }
}
