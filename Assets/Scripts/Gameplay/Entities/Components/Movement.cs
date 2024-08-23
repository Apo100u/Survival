using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Movement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 10.0f;
        
        public void Move(Vector3 direction)
        {
            transform.position += direction * speed;
        }
    }
}
