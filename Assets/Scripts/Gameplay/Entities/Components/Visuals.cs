using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Visuals : MonoBehaviour
    {
        [Header("Visuals Dependencies")]
        [SerializeField] private Transform visualsParent;

        public void LookAt(Vector3 worldPosition)
        {
            visualsParent.LookAt(worldPosition);
        }
    }
}
