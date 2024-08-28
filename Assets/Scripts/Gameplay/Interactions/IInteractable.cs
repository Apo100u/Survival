namespace SurvivalGame.Gameplay.Interactions
{
    public interface IInteractable
    {
        public void Interact();
        public bool IsInteractable();
        public string GetInteractionMessage();
    }
}
