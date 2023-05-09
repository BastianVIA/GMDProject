using UnityEngine;

namespace UI.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Potion")]
    public class Potion : Item
    {
        public bool isStackable;
    }
}