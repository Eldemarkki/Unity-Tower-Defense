using Eldemarkki.TowerDefenseGame.Turrets;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Upgrades
{
    [CreateAssetMenu(fileName = "New Increase Damage Upgrade", menuName = "Tower Defense/Upgrades/Increase Damage Upgrade")]
    public class IncreaseDamageUpgrade : TurretUpgrade
    {
        [SerializeField] private int damageIncrease;

        public override void ApplyToTurret(Turret turret)
        {
            turret.Damage += damageIncrease;
        }
    }
}