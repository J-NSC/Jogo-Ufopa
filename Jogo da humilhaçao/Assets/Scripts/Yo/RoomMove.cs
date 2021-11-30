using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 cameraChange;
    public Vector3 playerChange;
    private CameraFollow cam;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("player")){

            cam.minTile += cameraChange;
            cam.maxTile += cameraChange;
            CameraFollow.instance.StartMap();
            other.transform.position += playerChange;
        }
    }
}
