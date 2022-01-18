using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_MVC_Element : MonoBehaviour
{
    public Object_Controler controller { get; private set; }
    public Object_Model model { get; private set; }
    public Object_View view { get; private set; }

    public GameObject Initialize(GameObject prefab_View, Transform viewPoint, string object_name)
    {
        GameObject instance = GameObject.Instantiate<GameObject>(prefab_View, viewPoint);

        this.model = new Object_Model(object_name);
        this.view = instance.GetComponent<Object_View>();
        this.controller = new Object_Controler(model, view);

        return instance;
    }
}
