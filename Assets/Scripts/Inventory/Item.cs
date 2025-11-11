using NUnit.Framework.Interfaces;
using UnityEngine;

namespace Inventory
{

    [System.Serializable]
    public class Item : MonoBehaviour
    {
        public int type;
        public Color color;

        private Renderer objRenderer;
        void Awake()
        {
            objRenderer = GetComponent<Renderer>();
        }

        public void Initialize(int newType, Color c)
        {
            type = newType;
            color = c;
            objRenderer.material.color = color;
        }

        public ItemData GetData()
        {
            ItemData data = new ItemData();
            data.type = type;
            data.position = transform.position;
            data.color = color;
            return data;
        }
    }

    [System.Serializable]
    public class ItemData
    {
        public int type;
        public Vector3 position;
        public Color color;
    }
}
