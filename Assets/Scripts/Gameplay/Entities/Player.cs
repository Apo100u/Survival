using UnityEngine;
using SurvivalGame.Gameplay.Entities.Components;
using Input = SurvivalGame.Gameplay.Entities.Components.Input;

namespace SurvivalGame.Gameplay.Entities
{
    [RequireComponent(typeof(Input))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Equipment))]
    public class Player : Entity
    {
        [Header("Player Dependencies")]
        [SerializeField] private Camera mainCamera;
        
        private Input input;
        private Movement movement;
        private Equipment equipment;
        
        public override void Init()
        {
            AssignComponents();
        }

        private void AssignComponents()
        {
            input = GetComponent<Input>();
            movement = GetComponent<Movement>();
            equipment = GetComponent<Equipment>();
        }

        private void Update()
        {
            movement.Move(input.GetNormalizedMovementInput() * Time.deltaTime);
            UpdateVisualsLookAt(input.GetAimWorldPosition(mainCamera));
        }
    }
}
