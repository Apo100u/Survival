using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public ItemData ItemData { get; private set; }
        
        public void Interact()
        {
            gameObject.SetActive(false);
        }

        public string GetInteractionMessage()
        {
            return "Pick up";
        }
    }
}
