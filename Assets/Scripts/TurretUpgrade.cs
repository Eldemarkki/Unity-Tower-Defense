using UnityEngine;

public abstract class TurretUpgrade : ScriptableObject
{
    public int cost;
    public Sprite icon;
    public abstract void ApplyToTurret(Turret turret);
}