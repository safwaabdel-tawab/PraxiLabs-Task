using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controler : MonoBehaviour, IColor
{
    [SerializeField] 
    private ObjectData_SO objectData_SO;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    public Object_Model model { get; private set; }

    public Object_Controler()
    {
        model = new Object_Model();
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        Manager.Instance.OnColorChanged_Event += ChangeColor;
    }

    private void OnDisable()
    {
        Manager.Instance.OnColorChanged_Event -= ChangeColor;
    }
    public void ChangeColor(Color color)
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propBlock);

       // UpdateModelData()
    }

    public Color MyColor()
    {
        return _renderer.material.color;
    }
}
