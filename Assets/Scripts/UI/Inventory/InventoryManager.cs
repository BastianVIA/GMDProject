using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;
        public List<Item> Items = new List<Item>();
        [SerializeField] private ActionBarController actionBarController;
        
        public ItemController[] inventoryItems;
        public Transform itemContent;
        public GameObject inventoryItem;
        private void Awake()
        {
            Instance = this;
        }

        public void Add(Item item)
        {
            Items.Add(item);
        }

        public void Remove(Item item)
        {
            Items.Remove(item);
        }

        public void ListItems()
        {
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in Items)
            {
                GameObject gameObject = Instantiate(inventoryItem, itemContent);
                var itemName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                    var itemIcon = gameObject.transform.Find("ItemIcon").GetComponent<Image>();
                    var itemAmount = gameObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

                    itemName.text = item.itemName;
                    itemIcon.sprite = item.icon;
                    itemAmount.text = "";
            }
            //SetInventoryItems();
        }

        public void OnHealthPotionClick()
        {
            if (Items.Count <= 0) return;
            
            var item = Items.First(x => x.type == ItemType.Potion);
            Remove(item);
            int numberOfHealthPotions = Items.FindAll(i => i.type == ItemType.Potion).Count;
            actionBarController.SetAmountText(numberOfHealthPotions);
            ListItems();
        }

        private void SetInventoryItems()
        {
            inventoryItems = itemContent.GetComponentsInChildren<ItemController>();

            for (int i = 0; i < Items.Count; i++)
            {
                inventoryItems[i].AddItem(Items[i]);
            }
        }
    }
}
