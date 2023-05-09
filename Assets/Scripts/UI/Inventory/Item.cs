using UnityEngine;

namespace UI.Inventory
{
    public enum ItemType {Potion, Weapon}
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public Sprite icon;
        public ItemType type;
        public string details;
    }
}
