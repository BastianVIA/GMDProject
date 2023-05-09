using UnityEngine;

namespace Player
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance;
        [SerializeField] private Transform weaponPlacement;
        [SerializeField] private GameObject[] weapons;
        private void Start()
        {
            Instance = this;
        }

        public void ChangeWeapon(string weaponName)
        {
            Debug.Log("Spawn Weapon");
            switch (weaponName)
            {
                case "Dagger":
                    Instantiate(weapons[0], weaponPlacement);
                    break;
                case "Sword":
                    Instantiate(weapons[1], weaponPlacement);
                    break;
            }
            
        }
    }
}
