using Eldemarkki.TowerDefenseGame.Managers;
using Eldemarkki.TowerDefenseGame.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eldemarkki.TowerDefenseGame.Turrets
{
    [RequireComponent(typeof(Turret))]
    public class TurretUpgrader : MonoBehaviour
    {
        [Header("Left Upgrade Path")]
        [SerializeField] private TurretUpgrade[] leftUpgrades;
        [SerializeField] private Button leftUpgradeButton;
        [SerializeField] private TMP_Text leftUpgradeCostText;

        [Header("Right Upgrade Path")]
        [SerializeField] private TurretUpgrade[] rightUpgrades;
        [SerializeField] private Button rightUpgradeButton;
        [SerializeField] private TMP_Text rightUpgradeCostText;

        [Header("Both")]
        [SerializeField] private Sprite noUpgradesRemainingIcon;

        private int leftPathNextUpgradeIndex;
        private int rightPathNextUpgradeIndex;
        private MoneyManager moneyManager;
        private Turret turret;

        private void Awake()
        {
            turret = GetComponent<Turret>();
        }

        private void Start()
        {
            moneyManager = MoneyManager.instance;
            UpdateUI(leftUpgrades[0], UpgradePath.Left);
            UpdateUI(rightUpgrades[0], UpgradePath.Right);
        }

        public void TryUpgradeLeftPath()
        {
            TryUpgradePath(UpgradePath.Left);
        }

        public void TryUpgradeRightPath()
        {
            TryUpgradePath(UpgradePath.Right);
        }

        private void TryUpgradePath(UpgradePath upgradePath)
        {
            TurretUpgrade upgrade = null;
            if (upgradePath == UpgradePath.Left && leftPathNextUpgradeIndex < leftUpgrades.Length)
            {
                upgrade = leftUpgrades[leftPathNextUpgradeIndex];
            }
            else if (upgradePath == UpgradePath.Right && rightPathNextUpgradeIndex < rightUpgrades.Length)
            {
                upgrade = rightUpgrades[rightPathNextUpgradeIndex];
            }

            if (upgrade != null && moneyManager.HasEnoughMoneyForPurchase(upgrade.cost))
            {
                moneyManager.Money -= upgrade.cost;
                upgrade.ApplyToTurret(turret);

                if (upgradePath == UpgradePath.Left) leftPathNextUpgradeIndex++;
                else if (upgradePath == UpgradePath.Right) rightPathNextUpgradeIndex++;

                UpdateUI(upgrade, upgradePath);
            }
        }

        private void UpdateUI(TurretUpgrade upgrade, UpgradePath upgradePath)
        {
            if (upgradePath == UpgradePath.Left)
            {
                // Set the icon
                if (leftPathNextUpgradeIndex >= leftUpgrades.Length)
                {
                    leftUpgradeButton.image.sprite = noUpgradesRemainingIcon;
                    leftUpgradeButton.interactable = false;
                    leftUpgradeCostText.gameObject.SetActive(false);
                }
                else
                {
                    leftUpgradeButton.image.sprite = upgrade.icon;
                    leftUpgradeCostText.text = "$" + upgrade.cost;
                }
            }
            else if (upgradePath == UpgradePath.Right)
            {
                // Set the icon
                if (rightPathNextUpgradeIndex >= rightUpgrades.Length)
                {
                    rightUpgradeButton.image.sprite = noUpgradesRemainingIcon;
                    rightUpgradeButton.interactable = false;
                    rightUpgradeCostText.gameObject.SetActive(false);
                }
                else
                {
                    rightUpgradeButton.image.sprite = upgrade.icon;
                    rightUpgradeCostText.text = "$" + upgrade.cost;
                }
            }
        }
    }
}