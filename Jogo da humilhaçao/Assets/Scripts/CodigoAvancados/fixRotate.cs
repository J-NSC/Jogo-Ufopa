using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixRotate : MonoBehaviour
{
    Quaternion rotacao;

    private void Awake() {
        rotacao = transform.rotation;

    }

    private void LateUpdate() {
        transform.rotation = rotacao;
    }
}
