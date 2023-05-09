using UnityEngine;

namespace UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        public GameObject damageTextPrefab;

        public void SpawnDamageText(int damageAmount, Transform _transform)
        {
            GameObject damageTextObject = Instantiate(damageTextPrefab, _transform.position + new Vector3(0f, 2.1f, 0f), Quaternion.identity);
            TextMesh damageTextMesh = damageTextObject.GetComponentInChildren<TextMesh>();
            damageTextMesh.text = damageAmount.ToString();
        }
    }
}
