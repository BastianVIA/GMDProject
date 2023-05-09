using UnityEngine;

namespace UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float fadeSpeed = 2f;

        private TextMesh textMesh;
        private Color textColor;

        private void Awake()
        {
            textMesh = GetComponentInChildren<TextMesh>();
            textColor = textMesh.color;
        }

        private void Update()
        {
            MoveUp();
            LookAtCamera();
            FadeOut();
        }

        private void MoveUp()
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        private void LookAtCamera()
        {
            transform.LookAt(UnityEngine.Camera.main.transform);
        }

        private void FadeOut()
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}