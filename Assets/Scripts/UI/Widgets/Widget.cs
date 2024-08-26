using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class Widget : MonoBehaviour
    {
        [Tooltip("Position offset to add when this widget is following a transform.")]
        [SerializeField] private Vector3 transformFollowOffset;

        public bool IsShown => gameObject.activeSelf;
        
        protected RectTransform rectTransform;

        private Camera mainCamera;
        private RectTransform area;
        private Transform transformToFollow;
        
        public virtual void Init(Camera camera, RectTransform area)
        {
            mainCamera = camera;
            this.area = area;
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (transformToFollow)
            {
                FollowAssignedTransform();
            }
        }

        public void ToggleShow()
        {
            Show(!gameObject.activeSelf);
        }

        public virtual void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SetTransformToFollow(Transform transformToFollow)
        {
            this.transformToFollow = transformToFollow;
        }

        private void FollowAssignedTransform()
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transformToFollow.position) + transformFollowOffset;
            Vector2 adjustedPosition = new(screenPosition.x / Screen.width * area.sizeDelta.x, screenPosition.y / Screen.height * area.sizeDelta.y);
            
            rectTransform.anchoredPosition = adjustedPosition;
        }
    }
}
