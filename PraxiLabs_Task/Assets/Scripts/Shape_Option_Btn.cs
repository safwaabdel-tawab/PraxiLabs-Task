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
            Manager.Instance.ObjectSelected(object_SF.Value);
        });
    }
}
