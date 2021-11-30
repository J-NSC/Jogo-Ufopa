using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{

    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Healer(){
        playerHealth.RuntimeValue += amountToIncrease;

        if(playerHealth.intialValue > heartContainers.RuntimeValue ){
            playerHealth.intialValue =heartContainers.RuntimeValue;
        }

        powerUpSignal.Raise();
        // Destroy(this.gameObject);
    }

    
    
}
