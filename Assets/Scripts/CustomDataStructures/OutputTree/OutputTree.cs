namespace SurvivalGame.CustomDataStructures.OutputTree
{
    /// <summary>
    /// Represents tree structure but can also contain "output nodes" that cannot have children. Each node can have no more than one output node.
    /// </summary>
    public class OutputTree<T>
    {
        private OutputTreeNode<T> root;

        public OutputTree(T rootValue)
        {
            root = new OutputTreeNode<T>(rootValue);
        }
    }
}
