using Eldemarkki.TowerDefenseGame.Units;
using TMPro;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Managers
{
    public class HealthManager : MonoBehaviour
    {
        public static HealthManager instance;

        [SerializeField] private int maxHealth = 200;
        [SerializeField] private TMP_Text healthText;

        private int health;
        public int Health { get => health; set => SetHealth(value); }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Health = maxHealth;
            instance = this;
        }

        private void SetHealth(int value)
        {
            health = value;
            healthText.text = $"{health}/{maxHealth}";

            if (health <= 0)
            {
                Debug.Log("Game over!");
            }
        }

        public void OnUnitReachedGoal(Unit unit)
        {
            Health -= unit.Health;
        }
    }
}