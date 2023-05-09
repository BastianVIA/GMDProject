using UnityEngine;

namespace UI.Inventory
{
    public class ItemBounce : MonoBehaviour
    {
        private readonly float bounceHeight = 0.2f;
        private readonly float bounceSpeed = 4f;
    
        private Vector3 startPos;
    
        void Start()
        {
            startPos = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    
        void Update()
        {
            float yOffset = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            transform.position = startPos + new Vector3(0f, yOffset, 0f);
        }
    }
}
