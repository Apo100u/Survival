using System.Collections.Generic;
using UnityEngine;

namespace SurvivalGame.CustomDataStructures.OutputTree
{
    public class OutputTreeNode<T> where T : struct
    {
        public T Value { get; private set; }
        public T? Output { get; private set; }
        
        public readonly List<OutputTreeNode<T>> Children;

        public OutputTreeNode(T value)
        {
            Value = value;
            Children = new List<OutputTreeNode<T>>();
        }

        public bool HasChild(T childValue, out OutputTreeNode<T> childNode)
        {
            childNode = null;
            
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Value.Equals(childValue))
                {
                    childNode = Children[i];
                    break;
                }
            }

            return childNode != null;
        }

        public virtual void AddChild(T childValue, out OutputTreeNode<T> addedChild)
        {
            addedChild = new OutputTreeNode<T>(childValue);
            Children.Add(addedChild);

        }

        public virtual void AddOutputChild(T output)
        {
            if (Output != null)
            {
                Debug.LogError($"Tried to add output child to {nameof(OutputTreeNode<T>)}, but it already has an output. This shouldn't happen.");
            }

            Output = output;
        }
    }
}
