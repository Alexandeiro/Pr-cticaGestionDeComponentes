
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public Item[] itemPrefabs;
        private List<Item> currentItems = new List<Item>();

        private SceneLoader sceneLoader;

        void Start()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();

            List<ItemData> savedItems = sceneLoader.LoadInventory();
            if(savedItems != null )
            {
                foreach(ItemData item in savedItems )
                {
                    SpawnItem(item);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) CreateItem(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) CreateItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) CreateItem(2);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClearInventory();
            }
        }

       
        void CreateItem(int type)
        {
            if (type >= itemPrefabs.Length) return;

            Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Item newItem = Instantiate(itemPrefabs[type], pos, Quaternion.identity);
            newItem.GetComponent<Item>().Initialize(type, color);
            currentItems.Add(newItem);
        }

        void SpawnItem(ItemData item)
        {
            Item newItem = Instantiate(itemPrefabs[item.type], item.position, Quaternion.identity);
            newItem.GetComponent<Item>().Initialize(item.type, item.color);
            currentItems.Add(newItem);
        }
        public void SaveInventory()
        {
            List<ItemData> dataList = new List<ItemData>();
            foreach (Item i in currentItems)
            {
                dataList.Add(i.GetData());
            }

            sceneLoader.SaveInventory(dataList);
        }

        void OnApplicationQuit()
        {
            SaveInventory();
        }

        private void ClearInventory()
        {
            foreach (Item i in currentItems)
            {
                if (i != null)
                    Destroy(i.gameObject);
            }

            currentItems.Clear();

            sceneLoader.DeleteInventoryFile();

            Debug.Log("Inventario limpiado");
        }


    }
}
