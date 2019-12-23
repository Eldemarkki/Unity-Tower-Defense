using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Waves
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Tower Defense/Wave")]
    public class Wave : ScriptableObject
    {
        public WavePart[] waveUnits;
    }
}