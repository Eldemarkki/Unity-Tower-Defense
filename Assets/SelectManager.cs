using System.Linq;
using Eldemarkki.TowerDefenseGame.Turrets;
using UnityEngine;

namespace Eldemarkki.TowerDefenseGame.Managers
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private LayerMask turretLayer;

        private Turret selectedTurret;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPosition, Vector2.zero);
                if (hits.Length > 0)
                {
                    // Take the hit that is closest to mouse world position
                    RaycastHit2D closestHit = hits.OrderBy(h => Utilities.Utils.SqrDistance(h.point, mouseWorldPosition)).First();

                    if (closestHit.transform != null)
                    {
                        Turret turret = closestHit.transform.GetComponent<Turret>();
                        if(turret != null)
                            Select(turret);
                    }
                }
                else
                {
                    Deselect();
                }
            }
        }

        public void Select(Turret turret)
        {
            if (selectedTurret != null)
                selectedTurret.Selected = false;
            turret.Selected = true;
            selectedTurret = turret;
        }

        private void Deselect()
        {
            if (selectedTurret != null)
                selectedTurret.Selected = false;
            selectedTurret = null;
        }
    }
}