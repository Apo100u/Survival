using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame.CustomDataStructures.OutputTree
{
    public class OutputTreeNode<T>
    {
        public T Value { get; private set; }

        public OutputTreeOutputNode<T> Output;
            
        public readonly List<OutputTreeNode<T>> Children;

        public OutputTreeNode(T value)
        {
            Value = value;
            Children = new List<OutputTreeNode<T>>();
        }

        public virtual void AddChild(T child)
        {
            Children.Add(new OutputTreeNode<T>(child));
        }

        public virtual void AddOutputChild(T outputChild)
        {
            if (Output != null)
            {
                Debug.LogError($"Tried to add output child to {nameof(OutputTreeNode<T>)}, but it already has an output. This shouldn't happen.");
            }

            Output = new OutputTreeOutputNode<T>(outputChild);
        }
    }
}
