using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateExe : MonoBehaviour
{

    public delegate void Chefao(string s);
    public static event Chefao ordemDoChefe;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(ordemDoChefe != null){
                ordemDoChefe("matem todos eles ");
            }
        }
    }
}
