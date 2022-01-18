using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float PCRotationSpeed = 10f;
    private Camera cam;

    public event System.Action<float[]> OnMouseDragRotate_Event;

    private void Start()
    {
        cam = Manager.Instance.cam;
    }

    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
        float rotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

        Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
        Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

        transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;

        RaiseValueChanged(new float[] { transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z });
    }
    public void RaiseValueChanged(float[] rotation)
    {
        OnMouseDragRotate_Event?.Invoke(rotation);
    }
}