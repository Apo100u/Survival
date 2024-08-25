using SurvivalGame.Gameplay.Interactions;
using SurvivalGame.ScriptableObjects;
using UnityEngine;

namespace SurvivalGame.Gameplay.Items
{
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        
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
