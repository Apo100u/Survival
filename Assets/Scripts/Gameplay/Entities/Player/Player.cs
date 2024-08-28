using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Helpers.StateMachine;
using SurvivalGame.Gameplay.Items;
using SurvivalGame.UI;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities.Player
{
    [RequireComponent(typeof(Input), typeof(Movement), typeof(Inventory))]
    [RequireComponent(typeof(InteractionHandler))]
    public class Player : Entity
    {
        [Header("Player Dependencies")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerHUD hud;

        [Header("Player Camera settings")]
        [SerializeField] private float cameraTransitionSpeed = 100.0f;
        [SerializeField] private Transform exploringCameraTarget;
        [SerializeField] private Transform craftingCameraTarget;
        
        public ItemsSystem ItemsSystem { get; set; }
        
        private StateMachine<PlayerStateMachineCommand, State<PlayerStateMachineCommand>> stateMachine;
        private PlayerDependencies playerDependencies;
        
        public override void Init(ObjectPools objectPools)
        {
            base.Init(objectPools);
            
            CreatePlayerDependencies();
            hud.Init(mainCamera);
            SetUpStateMachine();
            SetUpStateTransitions();
        }

        private void Start()
        {
            string movementTutorialMessage = $"Press [{playerDependencies.Input.ForwardMovementKey}, {playerDependencies.Input.BackwardMovementKey}, " +
                                             $"{playerDependencies.Input.LeftMovementKey}, {playerDependencies.Input.RightMovementKey}] to move.";
            
            hud.InfoWidget.Show(true);
            hud.InfoWidget.SetInfoText(movementTutorialMessage);
            hud.InfoWidget.HideAfterSeconds(5.0f);
        }

        private void Update()
        {
            stateMachine.Process();
        }

        private void CreatePlayerDependencies()
        {
            playerDependencies = new PlayerDependencies(
                visuals:               visuals,
                movement:              GetComponent<Movement>(),
                interactionHandler:    GetComponent<InteractionHandler>(),
                inventory:             GetComponent<Inventory>(),
                input:                 GetComponent<Input>(),
                hud:                   hud,
                mainCamera:            mainCamera,
                objectPools:           objectPools,
                cameraTransitionSpeed: cameraTransitionSpeed);
        }

        private void SetUpStateMachine()
        {
            stateMachine = new StateMachine<PlayerStateMachineCommand, State<PlayerStateMachineCommand>>();

            ExploringState exploringState = new(playerDependencies, exploringCameraTarget);
            exploringState.AddTransition<CraftingState>(PlayerStateMachineCommand.RequestCrafting);

            CraftingState craftingState = new(playerDependencies, craftingCameraTarget, ItemsSystem);
            craftingState.AddTransition<ExploringState>(PlayerStateMachineCommand.RequestExploring);

            stateMachine.AddState(exploringState);
            stateMachine.AddState(craftingState);
            
            stateMachine.Start(exploringState);
        }
        
        private void SetUpStateTransitions()
        {
            hud.PlayerInventoryWidget.CraftingButtonInteracted += OnCraftingButtonInteracted;
            hud.PlayerCraftingWidget.BackButtonInteracted += OnBackButtonInteracted;
        }

        private void OnCraftingButtonInteracted()
        {
            stateMachine.ExecuteCommand(PlayerStateMachineCommand.RequestCrafting);
        }

        private void OnBackButtonInteracted()
        {
            stateMachine.ExecuteCommand(PlayerStateMachineCommand.RequestExploring);
        }
    }
}
