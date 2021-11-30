using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class InfosBasicClass{
//     public float vidaInicial;
//     public float mpInicail;
//     public int forca;
//     public int agilidade;
//     public int defBase;
//     public int forcabase;
// }


public abstract class CharacterBase : MonoBehaviour
{

  
    public int levelAtual;
    public FloatValue vidaInicial;
    public float mpInicail;
    public int forca;
    public int agilidade;
    public int defBase;
    public int forcabase;

    // public InfosBasicClass basicClass;


    private void Start() {
        
    }
}
