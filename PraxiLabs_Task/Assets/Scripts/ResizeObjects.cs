using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ResizeObjects : MonoBehaviour
{
    public float horizontalFoV = 60.0f;
    [SerializeField, ReadOnly] float initialFOV;
    
    Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
        initialFOV = CalculateFOV();
    }

    float CalculateFOV()
    {
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);
        float halfHeight = halfWidth * Screen.height / Screen.width;
        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        return verticalFoV;
    }

    void Update()
    {
        float updatedFOV = CalculateFOV();
        if (updatedFOV > initialFOV)
            _camera.fieldOfView = updatedFOV;
    }
}
