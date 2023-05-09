using UnityEngine;

namespace UI
{
    public class RotateCanvas : MonoBehaviour
    {
        private Vector3 initialScale;
        void Start()
        {
            initialScale = transform.localScale;
        }

        void Update()
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
    }
}
