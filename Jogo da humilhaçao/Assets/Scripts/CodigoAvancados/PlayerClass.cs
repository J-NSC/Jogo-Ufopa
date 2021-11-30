using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerClasses{
    Guerreiro = 0,
    Arqueiro = 1,
    mago = 2

}

public class PlayerClass : CharacterBase
{

  
    public PlayerClasses ClassPLayer;
    public Interact focus;
    public static PlayerClass inst;
    public float dano;

    private void Awake() {
        if(inst == null){
            inst = this;
        }
    }

    void Start()
    {
       
       PlayerStats.inst.SetPersonagem(PlayerClasses.mago);
       ClassPLayer = PlayerStats.inst.getClassesPersona();
       
       BasicInfos infs = PlayerStats.inst.getBasicStats(ClassPLayer);

       mpInicail = infs.mpInicailSO;
       forca = infs.forcaSO;
       agilidade = infs.agilidadeSO;
       defBase = infs.defBaseSO;
       forcabase = infs.forcabaseSO;
       dano = infs.forcaSO;

    // basicClass = PlayerStats.inst.getBasicStats(ClassPLayer);

       
       
    }

    void Update()
    {
        // Debug.Log(vidaInicial.intialValue);
        if(Input.GetKeyDown(KeyCode.E)){
            if(focus != null){
                focus.OnFocus();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("item")){
            focus = other.GetComponent<Interact>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("item")){
            focus = null;
        }
    }
}
