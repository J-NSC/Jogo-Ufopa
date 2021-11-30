using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjusteP : MonoBehaviour
{
    public bool ajustUptade;
    [SerializeField]
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "player";
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if(ajustUptade){
            sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
