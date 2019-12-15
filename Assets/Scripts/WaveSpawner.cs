using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Path wavePath;
    [SerializeField] private int waveIndex;
    [SerializeField] private GameObject unitPrefab;

    [HideInInspector] public List<Unit> spawnedUnits;

    private void Start()
    {
        Wave wave = waves[waveIndex];
        StartCoroutine(InitiateWave(wave));
    }

    private IEnumerator InitiateWave(Wave wave)
    {
        Vector3 spawnPosition = wavePath.points[0].position;
        for (int i = 0; i < wave.waveUnits.Length; i++)
        {
            WaveUnit waveUnit = wave.waveUnits[i];
            UnitType unitType = waveUnit.unitType;
            if (unitType == UnitType.Enemy)
            {
                UnitSettings settings = waveUnit.unitSettings;
                for (int j = 0; j < waveUnit.unitCount; j++)
                {
                    Unit unit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity).GetComponent<Unit>();
                    unit.path = wavePath;
                    unit.waveSpawner = this;

                    unit.Initialize(settings);

                    spawnedUnits.Add(unit);

                    yield return new WaitForSeconds(1f / waveUnit.spawnRate);
                }
            }
            else if (unitType == UnitType.Wait)
            {
                yield return new WaitForSeconds(waveUnit.waitTime);
            }
        }
    }
}