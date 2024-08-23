using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Input : MonoBehaviour
    {
        [Header("Key Binds")]
        [SerializeField] private KeyCode forwardMovement  = KeyCode.W;
        [SerializeField] private KeyCode backwardMovement = KeyCode.S;
        [SerializeField] private KeyCode rightMovement    = KeyCode.D;
        [SerializeField] private KeyCode leftMovement     = KeyCode.A;

        public Vector3 GetMovementInput()
        {
            Vector3 movementInput = new();

            if (UnityEngine.Input.GetKey(forwardMovement))
            {
                movementInput.z += 1.0f;
            }

            if (UnityEngine.Input.GetKey(backwardMovement))
            {
                movementInput.z -= 1.0f;
            }
            
            if (UnityEngine.Input.GetKey(rightMovement))
            {
                movementInput.x += 1.0f;
            }
            
            if (UnityEngine.Input.GetKey(leftMovement))
            {
                movementInput.x -= 1.0f;
            }

            return movementInput;
        }
    }
}
