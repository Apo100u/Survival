using UnityEngine;

namespace SurvivalGame.Gameplay.Entities.Components
{
    public class Input : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Key Binds")]
        [SerializeField] private KeyCode forwardMovement  = KeyCode.W;
        [SerializeField] private KeyCode backwardMovement = KeyCode.S;
        [SerializeField] private KeyCode rightMovement    = KeyCode.D;
        [SerializeField] private KeyCode leftMovement     = KeyCode.A;
        [SerializeField] private KeyCode interact         = KeyCode.E;

        public KeyCode InteractKey => interact;

        public Vector3 GetNormalizedMovementInput()
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

            return movementInput.normalized;
        }

        public bool GetInteractionDown()
        {
            return UnityEngine.Input.GetKeyDown(interact);
        }
        
        public Vector3 GetAimWorldPosition(Camera camera)
        {
            Vector3 aimWorldPosition = new();
            Ray ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, groundLayer))
            {
                aimWorldPosition = hitInfo.point;
            }
            
            return aimWorldPosition;
        }
    }
}
