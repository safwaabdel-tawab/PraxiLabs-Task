using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/ObjectData")]
public class ObjectData_SO : ScriptableObject
{
    public StringField objectName;
    public GameObject objectPrefab;
}
