using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Enemy.SkeletonMelee
{
    public class ItemDropper : MonoBehaviour, IItemDropper
    {
        [System.Serializable]
        public class ItemDrop
        {
            public GameObject itemPrefab;
            [Range(0f, 100f)] public float dropChancePercentage;
        }

        public List<ItemDrop> itemList;

        public void DropItem()
        {
            if (Random.value < 0.5f)
            {
                return;
            }
            float totalDropChancePercentage = 0f;
            foreach (ItemDrop item in itemList)
            {
                totalDropChancePercentage += item.dropChancePercentage;
            }

            float randomNumber = Random.Range(0f, totalDropChancePercentage);

            float cumulativeDropChancePercentage = 0f;
            foreach (ItemDrop item in itemList)
            {
                cumulativeDropChancePercentage += item.dropChancePercentage;
                if (randomNumber < cumulativeDropChancePercentage)
                {
                    Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
