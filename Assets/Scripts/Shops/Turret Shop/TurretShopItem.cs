using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Shops
{
    [CreateAssetMenu(fileName = "New Turret Shop Item", menuName = "Tower Defense/Turret Shop Item")]
    public class TurretShopItem : ScriptableObject
    {
        public int cost;
        public GameObject turretPrefab;
        public Sprite icon;
        public Color iconColor;
    }
}