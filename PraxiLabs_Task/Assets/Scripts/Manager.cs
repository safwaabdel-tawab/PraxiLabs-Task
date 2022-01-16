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
    [SerializeField, ReadOnly] Color currentColorInUse;

    public Dictionary<GameObject, ObjectData_SO> InstanObjects_Dict;
    public int IndexOfObjectViewed { get; set; }

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

            Object_Controler object_Controler = objectInstan.GetComponent<Object_Controler>();
            object_Controler = new Object_Controler();

            objectInstan.gameObject.SetActive(false);

            InstanObjects_Dict.Add(objectInstan, _object);
        }
    }

    public void ViewObject(string _name)
    {
        KeyValuePair<GameObject, ObjectData_SO> itemTobeViewed = InstanObjects_Dict.Where(p => p.Value.objectName.Value == _name).First();
        itemTobeViewed.Key.SetActive(true);
    }

    public void ColorSelected(Color _colorToBe)
    {
        if (ColorsDoMatch(currentColorInUse, _colorToBe)) return;

        currentColorInUse = _colorToBe;

        OnColorChanged_Event(currentColorInUse);
    }

    public bool ColorsDoMatch(Color _color1, Color _color2)
    {
        return _color1.r == _color2.r && _color1.g == _color2.g && _color1.b == _color2.b && _color1.a == _color2.a;
    }
}
