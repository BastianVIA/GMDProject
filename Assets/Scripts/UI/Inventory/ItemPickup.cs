using TMPro;
using UnityEngine;

namespace UI.Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private Item item;
        private TextMeshProUGUI amountText;
        
        private void Start()
        {
            amountText = GameObject.FindGameObjectWithTag("AmountText").GetComponent<TextMeshProUGUI>();
        }

        private void Pickup()
        {
            InventoryManager.Instance.Add(item);
            InventoryManager.Instance.ListItems();

            if (item.type == ItemType.Potion)
            {
                int numberOfHealthPotions = InventoryManager.Instance.Items.FindAll(i => i.type == ItemType.Potion).Count;
                amountText.text = numberOfHealthPotions.ToString();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Pickup();
                Destroy(gameObject);
            }
        }
    }
}
