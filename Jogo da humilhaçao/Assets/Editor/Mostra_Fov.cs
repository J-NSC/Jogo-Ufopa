using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(EnemyLog))]
public class Mostra_Fov : Editor
{

    private void OnSceneGUI() {
        EnemyLog fovM = (EnemyLog)target;

        Handles.color = Color.black;
        Handles.DrawWireArc(fovM.transform.position, Vector2.up,Vector2.up,360,fovM.infEn.fov);

        Vector3 vA = fovM.DirFAngle(-fovM.infEn.fov / 2);
        Vector3 vB = fovM.DirFAngle(fovM.infEn.fov / 2);

        Handles.DrawLine(fovM.transform.position, fovM.transform.position + vA * -fovM.infEn.raioVisao);
        Handles.DrawLine(fovM.transform.position, fovM.transform.position + vB * -fovM.infEn.raioVisao);
    }
}
