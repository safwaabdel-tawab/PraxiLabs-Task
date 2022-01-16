using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/ObjectData")]
public class ObjectData_SO : ScriptableObject
{
    public string objectName;
    public GameObject objectPrefab;
}
