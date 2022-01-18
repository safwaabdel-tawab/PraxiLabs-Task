using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Object_View : InputHandler, IColor
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;

    Action AfterEnable;
    Action AfterDisable;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    public void Init(Action AfterEnable, Action AfterDisable, Action<float[]> ObjectRotated)
    {
        this.AfterEnable = AfterEnable;
        this.AfterDisable = AfterDisable;

        OnMouseDragRotate_Event += ObjectRotated;
    }

    private void OnEnable()
    {
        if (AfterEnable == null) return;
        AfterEnable();
    }

    private void OnDisable()
    {
        if (AfterDisable == null) return;
        AfterDisable();
    }

    public void ChangeColor(Color color)
    {
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_Color", color);
        _renderer.SetPropertyBlock(_propBlock);
    }

    public Color MyColor()
    {
        return _renderer.material.color;
    }
}