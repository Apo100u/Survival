using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class CraftingState : PlayerState
    {
        public CraftingState(PlayerDependencies playerDependencies, Transform cameraTarget) : base(playerDependencies, cameraTarget)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            ShowCraftingUI(true);
            SetupLookAtVisualsForCrafting();
        }

        public override void OnExit()
        {
            base.OnExit();
            
            ShowCraftingUI(false);
        }

        private void ShowCraftingUI(bool show)
        {
            hud.PlayerInventoryWidget.Show(!show);
            hud.PlayerCraftingWidget.Show(show);

            if (show)
            {
                hud.PlayerCraftingWidget.ShowItems(inventory.GetItems());
            }
        }

        private void SetupLookAtVisualsForCrafting()
        {
            Vector3 lootAtTarget = visuals.transform.position - Vector3.forward;
            visuals.LookAt(lootAtTarget);
        }
    }
}
