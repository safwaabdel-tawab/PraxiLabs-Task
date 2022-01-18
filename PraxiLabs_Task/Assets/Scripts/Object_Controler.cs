using UnityEngine;

/// <summary>
/// Connects between Object_Model and Object_View
/// </summary>
public class Object_Controler
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

    /// <summary>
    /// Will be called whenever Object_View is enabled.
    /// </summary>
    public void SubscribeToEvents()
    {
        _manager.OnColorChanged_Event += ChangeColor;
        _manager.OnSaveApp_Event += Save_CurrentObjectData;
    }

    /// <summary>
    /// Will be called whenever Object_View is disabled.
    /// </summary>
    public void UnsubscribeFromEvents()
    {
        _manager.OnColorChanged_Event -= ChangeColor;
        _manager.OnSaveApp_Event -= Save_CurrentObjectData;
    }

    /// <summary>
    /// Update color data in model and the material color in view. 
    /// </summary>
    /// <param name="color"></param>
    public void ChangeColor(Color color)
    {
        view.ChangeColor(color);
        model.UpdateColor(new float[] { color.r, color.g, color.b});
    }

    /// <summary>
    /// Listens to the event that fires when the object rotate from object_view, then update rotation in Object_Model.
    /// </summary>
    /// <param name="rotation"></param>
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