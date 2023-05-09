using Player;
using UnityEngine;

namespace UI.Inventory
{
    public class ItemController : MonoBehaviour
    {
        public Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
        }

        public void RemoveItem()
        {
            InventoryManager.Instance.Remove(item);
            
            Destroy(gameObject);
        }

        public void UseItem()
        {
            switch (item.type)
            {
                case ItemType.Potion:
                    InventoryManager.Instance.OnHealthPotionClick();
                    PlayerHealth.Instance.Heal();
                    break;
                
                case ItemType.Weapon:
                    WeaponManager.Instance.ChangeWeapon(item.itemName);
                    break;
            }
            RemoveItem();
        }
    }
}
