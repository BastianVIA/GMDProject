using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ActionBarController : MonoBehaviour
    {
        private TextMeshProUGUI amountText;
        public Image imageSkillQ;
        private float cooldownQ = 5f;
        private bool isCooldown = false;
        private bool qIsPressed = false;
        private void Awake()
        {
            amountText = GameObject.FindGameObjectWithTag("AmountText").GetComponent<TextMeshProUGUI>();
            imageSkillQ.fillAmount = 0;
        }

        private void Update()
        {
            SkillQ();
        }

        private void SkillQ()
        {
            if (isCooldown == false && qIsPressed)
            {
                qIsPressed = false;
                isCooldown = true;
                imageSkillQ.fillAmount = 1f;
            }

            if (isCooldown)
            {
                imageSkillQ.fillAmount -= 1 / cooldownQ * Time.deltaTime;

                if (imageSkillQ.fillAmount <= 0)
                {
                    imageSkillQ.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }

        public void OnSkillQ(InputAction.CallbackContext context)
        {
            if (InventoryManager.Instance.Items.FindAll(i => i.type == ItemType.Potion).Count == 0)
            {
                return;
            }
            if (context.started)
            {
                if (isCooldown)
                {
                    return;
                }
                qIsPressed = true;
                InventoryManager.Instance.OnHealthPotionClick();
                InventoryManager.Instance.ListItems();
                PlayerHealth.Instance.Heal();
            }
        }

        public void SetAmountText(int amount)
        {
            amountText.text = amount.ToString();
        }
    }
}
