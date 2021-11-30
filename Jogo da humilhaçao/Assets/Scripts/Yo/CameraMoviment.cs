using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoviment : MonoBehaviour
{


    public static CameraMoviment instance;


    private void Awake() {

        if(instance == null){
            instance = this;

        }
    }
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition, mimPosition;

   
    void Start()
    {
        StartMap();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position){

            Vector3 targetPosistion =  new Vector3(target.position.x, target.position.y,transform.position.z);
            transform.position = Vector3.Lerp(transform.position,targetPosistion,smoothing);
        }
    }


    public void StartMap(){
    }
}
