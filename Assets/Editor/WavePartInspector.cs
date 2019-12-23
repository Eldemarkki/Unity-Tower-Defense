using Eldemarkki.TowerDefenseGame.Units;
using UnityEditor;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Waves.Editor
{
    [CustomEditor(typeof(WavePart))]
    public class WavePartInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var wavePart = target as WavePart;

            wavePart.unitType = (UnitType)EditorGUILayout.EnumPopup("Unit Type", wavePart.unitType);
            if (wavePart.unitType == UnitType.Enemy)
            {
                wavePart.unitSettings = (UnitSettings)EditorGUILayout.ObjectField("Unit Settings", wavePart.unitSettings, typeof(UnitSettings), false);
                wavePart.unitCount = EditorGUILayout.IntField("Unit Count", wavePart.unitCount);
                wavePart.spawnRate = EditorGUILayout.FloatField(new GUIContent("Spawn Rate", "The amount of units that will get spawned in 1 second."), wavePart.spawnRate);
            }
            else if (wavePart.unitType == UnitType.Wait)
            {
                wavePart.waitTime = EditorGUILayout.FloatField(new GUIContent("Wait Time", "How to long wait"), wavePart.waitTime);
            }
        }
    }
}