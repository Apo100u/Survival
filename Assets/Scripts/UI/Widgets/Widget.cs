using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class Widget : MonoBehaviour
    {
        [Tooltip("Position offset to add when this widget is following a transform.")]
        [SerializeField] private Vector3 transformFollowOffset;

        public bool IsShown => gameObject.activeSelf;
        
        protected RectTransform rectTransform;
        protected bool isAutoHiding;

        private Camera mainCamera;
        private RectTransform area;
        private Transform transformToFollow;
        private float secondsLeftToAutoHide;
        
        public virtual void Init(Camera camera, RectTransform area)
        {
            mainCamera = camera;
            this.area = area;
            rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Update()
        {
            if (transformToFollow)
            {
                FollowAssignedTransform();
            }

            if (isAutoHiding)
            {
                secondsLeftToAutoHide -= Time.deltaTime;

                if (secondsLeftToAutoHide <= 0.0f)
                {
                    isAutoHiding = false;
                    Show(false);
                }
            }
        }

        public void ToggleShow()
        {
            Show(!gameObject.activeSelf);
        }

        public virtual void Show(bool show)
        {
            gameObject.SetActive(show);
            isAutoHiding = false;
        }

        public void HideAfterSeconds(float durationInSeconds)
        {
            isAutoHiding = true;
            secondsLeftToAutoHide = durationInSeconds;
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
