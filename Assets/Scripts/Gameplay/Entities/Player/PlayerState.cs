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
        private const float CameraRotationSpeedMultiplier = 10.0f;
        
        protected Visuals visuals;
        protected Movement movement;
        protected InteractionHandler interactionHandler;
        protected Inventory inventory;
        protected Input input;
        protected PlayerHUD hud;
        protected Camera mainCamera;
        protected ObjectPools objectPools;
        protected float cameraTransitionSpeed;
        
        private Transform cameraTarget;

        public PlayerState(PlayerDependencies playerDependencies, Transform cameraTarget)
        {
            visuals               = playerDependencies.Visuals;
            movement              = playerDependencies.Movement;
            interactionHandler    = playerDependencies.InteractionHandler;
            hud                   = playerDependencies.Hud;
            inventory             = playerDependencies.Inventory;
            input                 = playerDependencies.Input;
            mainCamera            = playerDependencies.MainCamera;
            objectPools           = playerDependencies.ObjectPools;
            cameraTransitionSpeed = playerDependencies.CameraTransitionSpeed;
            
            this.cameraTarget = cameraTarget;
        }

        public override void Process()
        {
            base.Process();

            TranslateCameraTowardsTarget();
        }

        private void TranslateCameraTowardsTarget()
        {
            float distanceDelta = cameraTransitionSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(mainCamera.transform.position, cameraTarget.position, distanceDelta);
            mainCamera.transform.position = newPosition;

            float degreesDelta = cameraTransitionSpeed * Time.deltaTime * CameraRotationSpeedMultiplier;
            Quaternion newRotation = Quaternion.RotateTowards(mainCamera.transform.rotation, cameraTarget.transform.rotation, degreesDelta);
            mainCamera.transform.rotation = newRotation;
        }
    }
}
