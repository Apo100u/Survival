using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using SurvivalGame.Gameplay.Helpers;
using SurvivalGame.Gameplay.Helpers.StateMachine;
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

        private StateMachine<PlayerStateMachineCommand, State<PlayerStateMachineCommand>> stateMachine;
        private PlayerDependencies playerDependencies;
        
        public override void Init(ObjectPools objectPools)
        {
            base.Init(objectPools);
            
            CreatePlayerDependencies();
            hud.Init(mainCamera);
            SetUpStateMachine();
        }

        private void Update()
        {
            stateMachine.Process();
        }

        private void CreatePlayerDependencies()
        {
            playerDependencies = new PlayerDependencies(
                visuals:            visuals,
                movement:           GetComponent<Movement>(),
                interactionHandler: GetComponent<InteractionHandler>(),
                inventory:          GetComponent<Inventory>(),
                input:              GetComponent<Input>(),
                hud:                hud,
                mainCamera:         mainCamera,
                objectPools:        objectPools);
        }

        private void SetUpStateMachine()
        {
            stateMachine = new StateMachine<PlayerStateMachineCommand, State<PlayerStateMachineCommand>>();

            ExploringState exploringState = new(playerDependencies);
            exploringState.AddTransition<CraftingState>(PlayerStateMachineCommand.RequestCrafting);

            stateMachine.AddState(exploringState);
            
            stateMachine.Start(exploringState);
        }
    }
}
