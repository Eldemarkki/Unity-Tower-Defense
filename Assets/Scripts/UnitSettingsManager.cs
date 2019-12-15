using System.Linq;
using UnityEngine;

public class UnitSettingsManager : MonoBehaviour
{
    public static UnitSettingsManager instance;

    [SerializeField] private UnitSettings[] allUnitSettings;

    private void Awake()
    {
        instance = this;
        allUnitSettings = allUnitSettings.OrderBy(s => s.startingHealth).ToArray();
    }

    // Gets the first UnitSettings whose startingHealth is greater than or equal to the health parameter. allUnitSettings is sorted to an increasing order
    public UnitSettings GetUnitSettingsByHealth(int health)
    {
        for (int i = 0; i < allUnitSettings.Length; i++)
        {
            UnitSettings item = allUnitSettings[i];
            if (item.startingHealth >= health)
                return item;
        }

        return null;
    }
}