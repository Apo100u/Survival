using SurvivalGame.UI.Widgets;
using UnityEngine;

namespace SurvivalGame.UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [field: Header("Dependencies")]
        [field: SerializeField] public InfoWidget InfoWidget { get; private set; }
        [field: SerializeField] public TooltipWidget TooltipWidget { get; private set; }
        [field: SerializeField] public PlayerInventoryWidget PlayerInventoryWidget { get; private set; }
        [field: SerializeField] public PlayerCraftingWidget PlayerCraftingWidget { get; private set; }
        
        public void Init(Camera camera)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            
            InfoWidget.Init(camera, rectTransform);
            TooltipWidget.Init(camera, rectTransform);
            PlayerInventoryWidget.Init(camera, rectTransform);
            PlayerCraftingWidget.Init(camera, rectTransform);
        }
    }
}
