using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    private string savePath;

    [System.Serializable]
    public class GardenData
    {
        public List<PlantData> plants = new List<PlantData>();
    }

    void Awake()
    {
        savePath = Application.persistentDataPath + "/garden.json";
    }

    public void SaveGarden(List<PlantData> dataList)
    {
        GardenData data = new GardenData();
        data.plants = dataList;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Jardín guardado en: " + savePath);
    }

    public List<PlantData> LoadGarden()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No hay datos guardados");
            return null;
        }

        string json = File.ReadAllText(savePath);
        GardenData data = JsonUtility.FromJson<GardenData>(json);

        Debug.Log("Jardín cargado con " + data.plants.Count + " plantas");
        return data.plants;
    }
}
