using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public ItemData ItemData { get; private set; }

        private bool isInteractable;

        private void Awake()
        {
            isInteractable = true;
        }

        public void Interact()
        {
            
        }

        public bool IsInteractable()
        {
            return isInteractable;
        }

        public void SetIsInteractable(bool interactable)
        {
            isInteractable = interactable;
        }

        public string GetInteractionMessage()
        {
            return "Pick up";
        }
    }
}
