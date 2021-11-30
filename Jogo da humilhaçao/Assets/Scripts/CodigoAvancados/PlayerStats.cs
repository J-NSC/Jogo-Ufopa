using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseInfoChar{
    public BasicInfos basicInfs;
    public PlayerClasses classChar;
}

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerStats inst;

    public List<BaseInfoChar> baseInfoChars;

    public int xpMulti = 2;
    public float xpLevel =100;
    public float fatDifcult = 1.8f;
    private float xpProxLevel;
    // public Text txt,txt2,txt3;

    private void Awake() {
        if(inst == null){
            inst = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {   
    //         AddXp(100);
    //         txt.text = getXpAtual().ToString();
    //         txt2.text = getLevelAtual().ToString();
    //         txt3.text = getProxXp().ToString();
    //     }

    //    if (Input.GetKeyDown(KeyCode.R))
    //     {   
    //         PlayerPrefs.DeleteAll();
    //     }
    }


    public void AddXp(float xpAdd){
        float novoXp = PlayerPrefs.GetFloat("xpAtual") + xpAdd * xpMulti;

        // if(novoXp >=getProxXp()){
        //     AddLevel();
        //     novoXp = 0;
        // }

        while (novoXp >= getProxXp())
        {   
            novoXp -= getProxXp();
            AddLevel();
        }

        PlayerPrefs.SetFloat("xpAtual",novoXp);
    }

    public float getXpAtual(){
        return PlayerPrefs.GetFloat("xpAtual");
    }

    public int getLevelAtual(){
        return PlayerPrefs.GetInt("LevelAtual");
    }

    public void AddLevel(){
        int novolevel = getLevelAtual()+1;
        PlayerPrefs.SetInt("LevelAtual",novolevel);
    }

    public float getProxXp(){
        return xpProxLevel = xpLevel * (getLevelAtual()+1)* fatDifcult;
    }


    public void SetPersonagem(PlayerClasses pc){
        PlayerPrefs.SetInt("ClassePlayerSaveGame", (int)pc);
    }

    public PlayerClasses getClassesPersona(){
        int aux  = PlayerPrefs.GetInt("ClassePlayerSaveGame"); 
        switch (aux)
        {
            case 0:
                return PlayerClasses.Guerreiro;
                break;
            case 1:
                return PlayerClasses.Arqueiro;
                break;
            case 2 :
                return PlayerClasses.mago;
                break;

            default:
                return PlayerClasses.Guerreiro;
                break;
        }
    }

    public BasicInfos getBasicStats(PlayerClasses type){
        foreach (BaseInfoChar info in baseInfoChars)
        {
            if (info.classChar == type)
            {   
                return info.basicInfs;
            }
        }

        return baseInfoChars[0].basicInfs;
    }
}
