using UnityEngine;

namespace SurvivalGame.UI.Widgets
{
    public class SlidingWidget : Widget
    {
        [Header("Sliding Widget settings")]
        [Tooltip("How fast will the widget slide in or out when showing / hiding.")]
        [SerializeField] private float slideSpeed = 5.0f;
        
        private Vector2 hiddenPosition;
        private Vector2 shownPosition;
        private Vector2 targetPosition;
        
        public override void Init(Camera camera, RectTransform area)
        {
            base.Init(camera, area);
            
            hiddenPosition = rectTransform.anchoredPosition;
            shownPosition = Vector2.zero;
        }

        public override void Show(bool show)
        {
            if (show)
            {
                gameObject.SetActive(true);
                isAutoHiding = false;
            }
            
            targetPosition = show
                ? shownPosition
                : hiddenPosition;
        }

        protected override void Update()
        {
            base.Update();
            
            if (rectTransform.anchoredPosition != targetPosition)
            {
                MoveTowardsTargetPosition();
            }
        }

        private void MoveTowardsTargetPosition()
        {
            Vector2 movedPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, slideSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = movedPosition;

            if (movedPosition == hiddenPosition)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
