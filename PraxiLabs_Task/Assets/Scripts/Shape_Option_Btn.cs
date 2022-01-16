using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shape_Option_Btn : MonoBehaviour
{
    [SerializeField] StringField object_SF;
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(delegate
        {
            Manager.Instance.ViewObject(object_SF.Value);
        });
    }
}
