using UnityEngine;
using UnityEngine.EventSystems;

namespace Eldemarkki.TowerDefenseGame.Miscellaneous
{
    public class DragAndDrop : MonoBehaviour, IPointerDownHandler
    {
        private bool isDragging;
        public bool IsDragging { get => isDragging; set => isDragging = value; }

        public delegate void OnDropEvent();
        public OnDropEvent OnDropped;

        public delegate void OnCancelPurchaseEvent();
        public OnCancelPurchaseEvent OnCancelledPurchase;

        private void Update()
        {
            if (isDragging)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                transform.position = position;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                isDragging = false;
                OnDropped?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnCancelledPurchase?.Invoke();
            }
        }
    }
}