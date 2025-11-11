using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Inventory
{
    public class SceneLoader : MonoBehaviour
    {
        private string inventoryPath;

        [System.Serializable]
        public class InventoryData
        {
            public List<ItemData> items = new List<ItemData>();
        }

        void Awake()
        {
            inventoryPath = Application.persistentDataPath + "/inventory.json";
        }

        public void SaveInventory(List<ItemData> dataList)
        {
            InventoryData data = new InventoryData();
            data.items = dataList;

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(inventoryPath, json);

            Debug.Log("Inventario guardado en: " + inventoryPath);
        }

        public List<ItemData> LoadInventory()
        {
            if (!File.Exists(inventoryPath))
            {
                Debug.Log("No hay inventario guardado");
                return null;
            }

            string json = File.ReadAllText(inventoryPath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);

            Debug.Log("Inventario cargado con " + data.items.Count + " objetos");
            return data.items;
        }

        internal void DeleteInventoryFile()
        {
            if (File.Exists(inventoryPath))
            {
                File.Delete(inventoryPath);
                Debug.Log("Archivo de inventario eliminado.");
            }
        }
    }
}

