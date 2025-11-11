
using System.Collections.Generic;

using UnityEngine;


public class GardenManager : MonoBehaviour
{

    public Plant[] plantPrefabs;
    private List<Plant> currentPlants = new List<Plant>();

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        List<PlantData> savedPlants = sceneLoader.LoadGarden();
        if(savedPlants != null)
        {
            foreach(PlantData plant in savedPlants)
            {
                SpawnPlant(plant);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateRandomPlant();
        }
    }

    void CreateRandomPlant()
    {
        int randType = Random.Range(0, plantPrefabs.Length);
        Vector3 position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        Plant newPlant = Instantiate(plantPrefabs[randType], position, Quaternion.identity);
        newPlant.GetComponent<Plant>().Initialize(randType, color);
        currentPlants.Add(newPlant);
    }

    void SpawnPlant(PlantData plant)
    {
        Plant newPlant = Instantiate(plantPrefabs[plant.type], plant.position, Quaternion.identity);
        newPlant.GetComponent<Plant>().Initialize(plant.type, plant.color);
        currentPlants.Add(newPlant);
    }

    private void OnApplicationQuit()
    {

        SaveGarden();
    }

    private void SaveGarden()
    {
        List<PlantData> dataList = new List<PlantData>();
        foreach(Plant plant in currentPlants)
        {
            dataList.Add(plant.GetData());
        }

        sceneLoader.SaveGarden(dataList);
    }

    public void RemovePlant(Plant plant)
    {
        if (currentPlants.Contains(plant))
        {
            currentPlants.Remove(plant);
            Destroy(plant.gameObject);
        }
    }
}
