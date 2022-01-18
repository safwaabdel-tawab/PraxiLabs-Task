using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Controler : MonoBehaviour
{
    private Manager _manager;
    public Object_Model model { get; private set; }
    public Object_View view { get; private set; }

    public Object_Controler(Object_Model model, Object_View view)
    {
        this.model = model;
        this.view = view;

        this.view.gameObject.SetActive(false);
        this.view.Init(SubscribeToEvents, UnsubscribeFromEvents, ObjectRotated);
        _manager = Manager.Instance;
    }

    public void SubscribeToEvents()
    {
        _manager.OnColorChanged_Event += ChangeColor;
        _manager.OnSaveApp_Event += Save_CurrentObjectData;
    }

    public void UnsubscribeFromEvents()
    {
        _manager.OnColorChanged_Event -= ChangeColor;
        _manager.OnSaveApp_Event -= Save_CurrentObjectData;
    }

    public void ChangeColor(Color color)
    {
        view.ChangeColor(color);
        model.UpdateColor(new float[] { color.r, color.g, color.b});
    }

    public void ObjectRotated(float[] rotation)
    {
        model.UpdateRotation(rotation);
    }
    
    public void Save_CurrentObjectData()
    {
        model.SaveData();
    }
    
    public Object_Model Load_SavedObjectData()
    {
        return model.LoadData<Object_Model>();
    }
}