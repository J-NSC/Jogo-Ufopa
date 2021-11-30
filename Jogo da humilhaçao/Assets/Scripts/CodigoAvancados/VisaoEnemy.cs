using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoEnemy : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Perseguição();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("player")){
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("player")){
            target = null;
        }
    }

    void Perseguição(){
        if(target != null){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            float dist = Vector2.Distance(transform.position, target.position);

            if(dist <= 1.8f){
                speed = 0;
            }else{
                speed = 1.5f;
            }
        }else {
            transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }
    }
}
