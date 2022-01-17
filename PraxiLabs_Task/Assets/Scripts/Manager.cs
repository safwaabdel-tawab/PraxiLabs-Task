using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

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

    [SerializeField] ObjectDataList_SO objectsData_List;
    [SerializeField] Transform viewPoint;
    public Camera cam;

    [Header("Read Only")]
    [SerializeField, ReadOnly] Color currentColorInUse;
    [SerializeField, ReadOnly] GameObject currentObjectInUse;

    private Dictionary<GameObject, ObjectData_SO> InstanObjects_Dict;

    public delegate void ColorChangedEvent(Color _color);
    public event ColorChangedEvent OnColorChanged_Event;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InstanObjects_Dict = new Dictionary<GameObject, ObjectData_SO>();

        foreach (ObjectData_SO _object in objectsData_List.list)
        {
            GameObject objectInstan = Instantiate(_object.objectPrefab, viewPoint);

            //Object_Controler object_Controler = objectInstan.GetComponent<Object_Controler>();
            //object_Controler = new Object_Controler();

            objectInstan.gameObject.SetActive(false);

            InstanObjects_Dict.Add(objectInstan, _object);
        }
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
}