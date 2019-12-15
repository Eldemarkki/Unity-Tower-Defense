using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Unit", menuName = "Wave Unit"), System.Serializable]
public class WaveUnit : ScriptableObject
{
    public UnitType unitType;

    // UnitType.Enemy
    public UnitSettings unitSettings;
    public int unitCount;
    public float spawnRate;

    // UnitType.Wait
    public float waitTime;
}