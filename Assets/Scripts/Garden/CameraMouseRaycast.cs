using UnityEngine;

public class CameraMouseRaycast : MonoBehaviour
{
    public Camera mainCamera;

    private GardenManager gardenManager;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        gardenManager = FindObjectOfType<GardenManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null)
                {
                    gardenManager.RemovePlant(plant);
                    Debug.Log("Planta eliminada: " + plant.name);
                }
            }
        }
    }
}
