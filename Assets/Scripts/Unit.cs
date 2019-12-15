using UnityEngine;

public class Unit : MonoBehaviour
{
    private const float MinimumDistanceToWaypoint = 0.1f;

    [SerializeField] private UnitSettings unitSettings;

    [HideInInspector] public Path path;
    [HideInInspector] public WaveSpawner waveSpawner;

    private int nextWaypointIndex;
    private Quaternion targetRotation;
    private int health;

    public int Health { get => health; set => SetHealth(value); }
    public UnitSettings UnitSettings { get => unitSettings; set => SetUnitSettings(value); }

    public void Initialize(UnitSettings settings)
    {
        UnitSettings = settings;
        health = settings.startingHealth;
    }

    private void SetHealth(int value)
    {
        health = value;

        UnitSettings = GetUnitSettingsByHealth(value);

        if (health <= 0)
        {
            waveSpawner.spawnedUnits.Remove(this);
            Destroy(gameObject);
            return;
        }
    }

    private UnitSettings GetUnitSettingsByHealth(int health)
    {
        return UnitSettingsManager.instance.GetUnitSettingsByHealth(health);
    }

    private void SetUnitSettings(UnitSettings value)
    {
        unitSettings = value;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = value.sprite;
        spriteRenderer.color = value.spriteColor;
        spriteRenderer.sortingOrder = value.startingHealth;
    }

    private void Update()
    {
        Vector3 nextWaypoint = path.points[nextWaypointIndex].position;
        Vector3 lookPosition = nextWaypoint - transform.position;

        targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg - 90);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, unitSettings.speed * Time.deltaTime * 3);

        // TODO: Use SqrDistance
        if (Utils.SqrDistance(transform.position, nextWaypoint) <= MinimumDistanceToWaypoint * MinimumDistanceToWaypoint)
        {
            if (nextWaypointIndex + 1 < path.points.Length)
            {
                nextWaypointIndex++;
            }
            else
            {
                // Reached goal
                HealthManager.instance.OnUnitReachedGoal(this);
                waveSpawner.spawnedUnits.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}
