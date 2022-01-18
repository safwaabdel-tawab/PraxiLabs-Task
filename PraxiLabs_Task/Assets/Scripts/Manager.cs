using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Manager>();
            return _instance;
        }
    }

    /// <summary>
    /// Holds all objects names and prefabs.
    /// </summary>
    [SerializeField] ObjectDataList_SO objectsData_List;
    /// <summary>
    /// Parent transform that holds all the instantiated objects.
    /// </summary>
    [SerializeField] Transform viewPoint;
    public Camera cam;

    [Header("Read Only")]
    [SerializeField, ReadOnly] Color currentColorInUse;
    [SerializeField, ReadOnly] GameObject currentObjectInUse;

    /// <summary>
    /// key Holds objects that got instantiated, and the value holds the corresponding scriptableObject that have the name of the object and the prefab that will be instantiated.
    /// </summary>
    private Dictionary<GameObject, ObjectData_SO> InstanObjects_Dict;

    //--Events--//
    public delegate void ColorChangedEvent(Color _newColor);
    /// <summary>
    /// Event that gets called after Brush_Btn being clicked.
    /// Responsible for changing color of object material and update object_model CurrentColor variable. 
    /// </summary>
    public event ColorChangedEvent OnColorChanged_Event;

    public delegate void SaveApp();
    /// <summary>
    /// Responsible for saving current object data.
    /// </summary>
    public event SaveApp OnSaveApp_Event;

    Object_MVC_Element object_Element;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InstanObjects_Dict = new Dictionary<GameObject, ObjectData_SO>();

        foreach (ObjectData_SO _object in objectsData_List.list)
        {
            object_Element = new Object_MVC_Element();
            GameObject objectInstan = object_Element.Initialize(_object.objectPrefab, viewPoint, _object.objectName.Value); //Prepare all mvc components for all the object elements that we have.

            InstanObjects_Dict.Add(objectInstan, _object);
        }

        LoadSavedData();
    }

    /// <summary>
    /// Views the object that has been selected, and disable the old viewed object.
    /// </summary>
    /// <param name="_name"></param>
    public void ObjectSelected(string _name)
    {
        KeyValuePair<GameObject, ObjectData_SO> itemTobeViewed = InstanObjects_Dict.Where(p => p.Value.objectName.Value == _name).First();

        if (itemTobeViewed.Key == null || itemTobeViewed.Value == null)
            Debug.LogError(string.Format("{0} is not included right, make sure that it is added to the \"ObjectsData_List\" scriptableObject", _name));

        if (currentObjectInUse == itemTobeViewed.Key) return;
        if (currentObjectInUse != null) currentObjectInUse.SetActive(false);

        itemTobeViewed.Key.SetActive(true);

        currentObjectInUse = itemTobeViewed.Key;
        currentColorInUse = new Color(0, 0, 0, 0);
    }

    /// <summary>
    /// Fires the OnColorChanged_Event when color changed.
    /// </summary>
    /// <param name="_colorToBe"></param>
    public void ColorSelected(Color _colorToBe)
    {
        if (DoColorsMatch(currentColorInUse, _colorToBe)) return;

        currentColorInUse = _colorToBe;

        OnColorChanged_Event?.Invoke(currentColorInUse);
    }

    public bool DoColorsMatch(Color _color1, Color _color2)
    {
        return _color1.r == _color2.r && _color1.g == _color2.g && _color1.b == _color2.b && _color1.a == _color2.a;
    }

    public void LoadSavedData()
    {
        Object_Model saved_ObjectModel = object_Element.controller.Load_SavedObjectData(); //Getting data saved.
        if (saved_ObjectModel == null) return;


        ObjectSelected(saved_ObjectModel.object_name); //View object that was last saved.
        ColorSelected(new Color(saved_ObjectModel.CurrentColor[0], saved_ObjectModel.CurrentColor[1], saved_ObjectModel.CurrentColor[2])); //Apply color saved.

        //Update rotation data to both gameObject and for (Object_Model when RaiseValueChanged is called).
        currentObjectInUse.transform.eulerAngles = new Vector3(saved_ObjectModel.Rotation[0], saved_ObjectModel.Rotation[1], saved_ObjectModel.Rotation[2]);
        currentObjectInUse.GetComponent<Object_View>().RaiseValueChanged(saved_ObjectModel.Rotation);
    }

    private void OnApplicationQuit()
    {
        OnSaveApp_Event();
    }
}