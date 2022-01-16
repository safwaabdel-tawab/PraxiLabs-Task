[System.Serializable]
public class Object_Model
{
    public float[] Rotation;
    public float[] CurrentColor;

    public Object_Model()
    {
        Rotation = new float[] { };
        CurrentColor = new float[] { };
    }

    public Object_Model(float[] rotation, float[] currentColor)
    {
        Rotation = rotation;
        CurrentColor = currentColor;
    }
}
