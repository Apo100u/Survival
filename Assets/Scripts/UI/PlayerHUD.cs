using SurvivalGame.UI.Widgets;
using UnityEngine;

namespace SurvivalGame.UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [field: Header("Dependencies")]
        [field: SerializeField] public TooltipWidget TooltipWidget { get; private set; }
    }
}
