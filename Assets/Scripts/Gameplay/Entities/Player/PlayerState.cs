using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Helpers.StateMachine;
using SurvivalGame.UI;
using UnityEngine;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public abstract class PlayerState : State<PlayerStateMachineCommand>
    {
        protected Transform visuals;
        protected Movement movement;
        protected InteractionHandler interactionHandler;
        protected Inventory inventory;
        protected Input input;
        protected PlayerHUD hud;
        protected Camera mainCamera;
        protected ObjectPools objectPools;
        
        private Transform cameraTarget;

        public PlayerState(PlayerDependencies playerDependencies, Transform cameraTarget)
        {
            visuals            = playerDependencies.Visuals;
            movement           = playerDependencies.Movement;
            interactionHandler = playerDependencies.InteractionHandler;
            hud                = playerDependencies.Hud;
            inventory          = playerDependencies.Inventory;
            input              = playerDependencies.Input;
            mainCamera         = playerDependencies.MainCamera;
            objectPools        = playerDependencies.ObjectPools;
            
            this.cameraTarget = cameraTarget;
        }
    }
}
