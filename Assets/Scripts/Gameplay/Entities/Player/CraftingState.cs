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

            hud.PlayerInventoryWidget.Show(false);
            hud.PlayerCraftingWidget.Show(true);
            
            Vector3 lootAtTarget = visuals.transform.position - Vector3.forward;
            visuals.LookAt(lootAtTarget);
        }

        public override void OnExit()
        {
            base.OnExit();
            
            hud.PlayerInventoryWidget.Show(true);
            hud.PlayerCraftingWidget.Show(false);
        }
    }
}
