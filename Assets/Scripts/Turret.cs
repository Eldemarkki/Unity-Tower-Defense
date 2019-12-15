using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] private float range;
    [SerializeField] private float weaponCooldown;
    [SerializeField] private TurretTargeting turretTargeting;

    [Header("Projectile")]
    [SerializeField] private int damage;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 300f;

    [Header("Other")]
    [SerializeField] private Transform rangeDisplay;

    private WaveSpawner waveSpawner;
    private float cooldownLeft;

    public float Range
    {
        get => range;
        set => SetRange(value);
    }

    private void Awake()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
        rangeDisplay.localScale = Vector3.one * 2 * range;
    }

    private void OnValidate()
    {
        rangeDisplay.localScale = Vector3.one * 2 * range;
    }

    void Update()
    {
        if (cooldownLeft <= 0)
        {
            Unit[] unitsInRange = GetUnitsInRange(transform.position, range, waveSpawner.spawnedUnits);
            if (unitsInRange.Length > 0)
            {
                Unit target = GetTarget(turretTargeting, transform.position, unitsInRange);

                ShootAtUnit(target);
                LookAtPosition(target.transform.position);
            }
        }

        cooldownLeft -= Time.deltaTime;
    }

    private Unit GetTarget(TurretTargeting turretTargeting, Vector2 position, Unit[] unitsInRange)
    {
        if(turretTargeting == TurretTargeting.First)
        {
            return unitsInRange[0];
        }
        else if(turretTargeting == TurretTargeting.Last)
        {
            return unitsInRange[unitsInRange.Length - 1];
        }
        else if(turretTargeting == TurretTargeting.Closest)
        {
            return unitsInRange.OrderBy(u => Utils.SqrDistance(position, u.transform.position)).First();
        }
        else if(turretTargeting == TurretTargeting.Weakest)
        {
            return unitsInRange.OrderBy(u => u.Health).First();
        }
        else if (turretTargeting == TurretTargeting.Strongest)
        {
            return unitsInRange.OrderByDescending(u => u.Health).First();
        }

        throw new NotImplementedException("No implementation for TurretTargeting." + turretTargeting);
    }

    private Unit[] GetUnitsInRange(Vector2 position, float range, List<Unit> units)
    {
        List<Unit> unitsInRange = new List<Unit>();
        for (int i = 0; i < units.Count; i++)
        {
            Unit unit = units[i];
            if (Utils.SqrDistance(position, unit.transform.position) <= range*range)
            {
                unitsInRange.Add(unit);
            }
        }

        return unitsInRange.ToArray();
    }

    private void LookAtPosition(Vector2 unitPosition)
    {
        Vector2 lookPosition = unitPosition - (Vector2)transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg - 90);
    }

    private void SetRange(float range)
    {
        this.range = range;
        rangeDisplay.localScale = Vector3.one * 2 * range;
    }

    void ShootAtUnit(Unit unit)
    {
        Vector2 unitPosition = unit.transform.position;
        Rigidbody2D instantiatedBullet = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        instantiatedBullet.AddForce((unitPosition - (Vector2)transform.position).normalized * projectileSpeed);
        unit.Health -= damage;
        cooldownLeft = weaponCooldown;

        Destroy(instantiatedBullet, 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, range);
    }
}
