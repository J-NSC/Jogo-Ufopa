using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LifeManeger : MonoBehaviour
{
    
    public Image[] bar;
    public Sprite fullBar;
    public Sprite emptyBar;
    public FloatValue barContainers;
    public FloatValue playerCurrentBar;

    // Start is called before the first frame update
    void Start()
    {
        initLife();
    }

    public void initLife()
    {
        for (int i= 0;i < barContainers.intialValue;i++)
        {
            bar[i].gameObject.SetActive(true);
            bar[i].sprite = fullBar;
        }

    }


    public void UpdateLife()
    {
        float tempLife = playerCurrentBar.RuntimeValue;

        for (int i = 0; i < barContainers.intialValue;i++)
        {   
            if (i <tempLife)
            {
              
                //vida cheia
                bar[i].sprite = fullBar;

            }else if ( i >= tempLife)
            {
                Debug.Log("i :" + i);
                Debug.Log("temp : " + tempLife);
                // vaida vazia
                bar[i].sprite = emptyBar;
            }
        }
    }
}
