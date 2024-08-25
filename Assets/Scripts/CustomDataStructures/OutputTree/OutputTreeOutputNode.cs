using UnityEngine;

namespace SurvivalGame.CustomDataStructures.OutputTree
{
    public class OutputTreeOutputNode<T> : OutputTreeNode<T>
    {
        public OutputTreeOutputNode(T value) : base(value)
        {
        }

        public override void AddChild(T child)
        {
            Debug.LogError($"Tried to add child to {nameof(OutputTreeOutputNode<T>)}. This is not valid and should not happen.");
        }

        public override void AddOutputChild(T outputChild)
        {
            Debug.LogError($"Tried to add output child to {nameof(OutputTreeOutputNode<T>)}. This is not valid and should not happen.");
        }
    }
}
