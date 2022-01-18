using System.Linq;

[System.Serializable]
public class Object_Model : ISave
{
    public float[] Rotation;
    public float[] CurrentColor;
    public string object_name;

    //Constructor Chaining
    public Object_Model() : this("") { }

    public Object_Model(string object_name) : this(new float[] { 0, 0, 0 }, new float[] { 1, 1, 1 }, object_name) { }

    public Object_Model(float[] rotation, float[] currentColor, string object_name)
    {
        Rotation = rotation;
        CurrentColor = currentColor;
        this.object_name = object_name;
    }

    public void UpdateColor(float[] color)
    {
        CurrentColor = color;
    }

    public void UpdateRotation(float[] rotation)
    {
        Rotation = rotation;
    }

    public void SaveData()
    {
        SaveSystem.Save(this, "object.txt");
    }

    public Object_Model LoadData<Object_Model>()
    {
        Object_Model model = (Object_Model)SaveSystem.Load<ISave>("object.txt");
        return model;
    }
}

public interface ISave
{
    void SaveData();
    T LoadData<T>();
}