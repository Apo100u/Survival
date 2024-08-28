using System.Collections.Generic;

namespace SurvivalGame.CustomDataStructures.OutputTree
{
    /// <summary>
    /// Represents tree structure but nodes can also contain "outputs". Each node can have no more than one output.
    /// </summary>
    public class OutputTree<T> where T : struct
    {
        private List<OutputTreeNode<T>> roots = new();
        
        public T? GetOutput(List<T> sortedNodeValues)
        {
            if (sortedNodeValues.Count == 0 || GetRootWithValue(sortedNodeValues[0]) == null)
            {
                return null;
            }

            T? output = null;
            OutputTreeNode<T> checkedNode = GetRootWithValue(sortedNodeValues[0]);

            for (int i = 1; i < sortedNodeValues.Count; i++)
            {
                T currentValue = sortedNodeValues[i];
                bool isLastNode = i == sortedNodeValues.Count - 1;

                if (checkedNode.HasChild(currentValue, out OutputTreeNode<T> nextChildToCheck))
                {
                    checkedNode = nextChildToCheck;
                }
                else
                {
                    break;
                }

                if (isLastNode)
                {
                    output = checkedNode.Output;
                }
            }

            return output;
        }
        
        public void AddBranch(T[] values, T output)
        {
            OutputTreeNode<T> checkedNode = EnsureRootExists(values[0]);

            for (int i = 1; i < values.Length; i++)
            {
                T currentValue = values[i];
                
                if (!checkedNode.HasChild(currentValue, out OutputTreeNode<T> nextChildToCheck))
                {
                    checkedNode.AddChild(currentValue, out nextChildToCheck);
                }

                checkedNode = nextChildToCheck;
            }
            
            checkedNode.AddOutputChild(output);
        }

        private OutputTreeNode<T> EnsureRootExists(T rootValue)
        {
            OutputTreeNode<T> root = null;
            
            for (int i = 0; i < roots.Count; i++)
            {
                if (roots[i].Value.Equals(rootValue))
                {
                    root = roots[i];
                    break;
                }
            }

            if (root == null)
            {
                root = new OutputTreeNode<T>(rootValue);
                roots.Add(root);
            }

            return root;
        }

        private OutputTreeNode<T> GetRootWithValue(T value)
        {
            OutputTreeNode<T> rootWithValue = null;

            for (int i = 0; i < roots.Count; i++)
            {
                if (roots[i].Value.Equals(value))
                {
                    rootWithValue = roots[i];
                }
            }
            
            return rootWithValue;
        }
    }
}
