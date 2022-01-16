using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IColor
{
    Color MyColor();
}
public class Brush_Btn : MonoBehaviour, IColor
{
    [SerializeField] Image brush_Img;
    [SerializeField] Button button;

    private void Start()
    {
        button.onClick.AddListener(delegate
        {
            Manager.Instance.ColorSelected(MyColor());
        });
    }
    public Color MyColor()
    {
        return brush_Img.color;
    }
}