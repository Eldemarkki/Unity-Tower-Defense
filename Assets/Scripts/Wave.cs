using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave"), System.Serializable]
public class Wave : ScriptableObject
{
    public WaveUnit[] waveUnits;
}