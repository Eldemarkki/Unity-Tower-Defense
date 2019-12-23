using Eldemarkki.TowerDefenseGame.Turrets;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Upgrades
{
    [CreateAssetMenu(fileName = "New Increase Range Upgrade", menuName = "Tower Defense/Upgrades/Increase Range Upgrade")]
    public class IncreaseRangeUpgrade : TurretUpgrade
    {
        [SerializeField] private int rangeIncrease;

        public override void ApplyToTurret(Turret turret)
        {
            turret.Range += rangeIncrease;
        }
    }
}