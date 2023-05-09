using UnityEngine;

namespace UI.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Weapon")]
    public class Weapon : Item
    {
        public int weaponDamage;
    }
}
