using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class CraftingState : PlayerState
    {
        private Transform visuals;

        public CraftingState(PlayerDependencies playerDependencies, Transform cameraTarget) : base(playerDependencies, cameraTarget)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Vector3 lootAtTarget = visuals.transform.position - Vector3.forward;
            
            visuals.LookAt(lootAtTarget);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}
