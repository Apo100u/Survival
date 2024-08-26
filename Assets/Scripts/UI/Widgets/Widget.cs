using System;
using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class Widget : MonoBehaviour
    {
        protected RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SetPositionFromWorldPosition(Vector3 worldPosition, Camera camera)
        {
            Vector3 screenPosition = camera.WorldToScreenPoint(worldPosition);
            rectTransform.anchoredPosition = screenPosition;
        }
    }
}
