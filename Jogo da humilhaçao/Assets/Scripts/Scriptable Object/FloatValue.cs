using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{

    public float intialValue;

    [HideInInspector]
    public float RuntimeValue;

    public void OnBeforeSerialize(){
        RuntimeValue = intialValue;
    }

    public void OnAfterDeserialize(){}

}
