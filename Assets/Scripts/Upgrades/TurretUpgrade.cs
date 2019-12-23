using Eldemarkki.TowerDefenseGame.Turrets;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Upgrades
{
    public abstract class TurretUpgrade : ScriptableObject
    {
        public int cost;
        public Sprite icon;
        public abstract void ApplyToTurret(Turret turret);
    }
}