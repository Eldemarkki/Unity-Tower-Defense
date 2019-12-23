using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Part", menuName = "Tower Defense/Wave Part")]
public class WavePart : ScriptableObject
{
    public UnitType unitType;

    // UnitType.Enemy
    public UnitSettings unitSettings;
    public int unitCount;
    public float spawnRate;

    // UnitType.Wait
    public float waitTime;
}