using UnityEngine;


[System.Serializable]
public class Plant : MonoBehaviour
{
    public int type;
    public Color color;

    private Renderer plantRenderer;

    private void Awake()
    {
        plantRenderer = GetComponent<Renderer>();
    }

    public void Initialize(int createType, Color c)
    {
        type = createType;
        color = c;
        plantRenderer.material.color = color;
    }

    public PlantData GetData()
    {
        PlantData data = new PlantData();
        data.type = type;
        data.color = color;
        data.position = transform.position;
        return data;
    }


}
[System.Serializable]

public class PlantData
{
    public int type;
    public Vector3 position;
    public Color color;
}
