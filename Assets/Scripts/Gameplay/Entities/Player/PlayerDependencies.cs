using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.UI;
using UnityEngine;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities.Player
{
    public class PlayerDependencies
    {
        public readonly Transform Visuals;
        public readonly Movement Movement;
        public readonly InteractionHandler InteractionHandler;
        public readonly Inventory Inventory;
        public readonly Input Input;
        public readonly PlayerHUD Hud;
        public readonly Camera MainCamera;
        public readonly ObjectPools ObjectPools;
        public readonly float CameraTransitionSpeed;

        public PlayerDependencies(Transform visuals, Movement movement, InteractionHandler interactionHandler, Inventory inventory, Input input, PlayerHUD hud,
            Camera mainCamera, ObjectPools objectPools, float cameraTransitionSpeed)
        {
            Visuals = visuals;
            Movement = movement;
            InteractionHandler = interactionHandler;
            Inventory = inventory;
            Input = input;
            Hud = hud;
            MainCamera = mainCamera;
            ObjectPools = objectPools;
            CameraTransitionSpeed = cameraTransitionSpeed;
        }
    }
}
