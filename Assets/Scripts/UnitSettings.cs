using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Settings", menuName = "Tower Defense/Unit Settings")]
public class UnitSettings : ScriptableObject
{
    public Sprite sprite;
    public Color spriteColor;
    public float speed;
    public int startingHealth;
}
