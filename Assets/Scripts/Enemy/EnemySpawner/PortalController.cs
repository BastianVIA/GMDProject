using UnityEngine;

namespace Enemy.EnemySpawner
{
    public class PortalController : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 4);
        }
    }
}
