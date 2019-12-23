using Eldemarkki.TowerDefenseGame.Managers;
using Eldemarkki.TowerDefenseGame.Miscellaneous;
using Eldemarkki.TowerDefenseGame.Turrets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eldemarkki.TowerDefenseGame.Shops
{
    public class TurretShop : MonoBehaviour
    {
        [SerializeField] private TurretShopItem[] items;
        [SerializeField] private Button shopButtonPrefab;
        [SerializeField] private RectTransform shopButtonHolder;

        [SerializeField] private MoneyManager moneyManager;

        private void Start()
        {
            CreateShopButtons();
        }

        void CreateShopButtons()
        {
            foreach (TurretShopItem item in items)
            {
                Button shopButton = Instantiate(shopButtonPrefab, Vector3.zero, Quaternion.identity, shopButtonHolder);

                shopButton.GetComponentInChildren<TMP_Text>().text = item.turretPrefab.name;
                Image image = shopButton.GetComponentsInChildren<Image>()[1];
                image.sprite = item.icon;
                image.color = item.iconColor;

                shopButton.onClick.AddListener(() => PurchaseItem(item));
            }
        }

        private void PurchaseItem(TurretShopItem item)
        {
            bool hasEnoughMoney = moneyManager.HasEnoughMoneyForPurchase(item.cost);
            if (hasEnoughMoney)
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject purchasedTurret = Instantiate(item.turretPrefab, worldPosition, Quaternion.identity);
                DragAndDrop dragAndDrop = purchasedTurret.GetComponent<DragAndDrop>();
                dragAndDrop.IsDragging = true;

                // Disable shooting
                Turret turret = purchasedTurret.GetComponent<Turret>();
                turret.enabled = false;

                // When it is dropped, disable dragging to lock it in place and enable shooting
                dragAndDrop.OnDropped += () =>
                {
                    turret.enabled = true;
                    Destroy(dragAndDrop);

                    // Only if the user finalized the purchase then take the payment
                    moneyManager.Money -= item.cost;
                };

                dragAndDrop.OnCancelledPurchase += () =>
                {
                    Destroy(purchasedTurret);
                };
            }
        }
    }
}