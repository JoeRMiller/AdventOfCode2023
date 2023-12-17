using System.Text;

namespace AdventofCode2023.Core
{
    public class Tree<T>
    {
        public TreeNode<T> Root { get; private set; }

        public Tree(T rootValue)
        {
            this.Root = new TreeNode<T>(rootValue);
        }

        public void PreOrderTraversal(TreeNode<T> node, Action<T> visit)
        {
            if (node == null) return;

            visit(node.Value);

            foreach (var child in node.Nodes)
            {
                PreOrderTraversal(child, visit);
            }
        }

        public void PreOrderTraversal(TreeNode<T> node, Func<T, T> visit)
        {
            if (node == null) return;

            visit(node.Value);

            foreach (var child in node.Nodes)
            {
                PreOrderTraversal(child, visit);
            }
        }

        public void PostOrderTraversal(TreeNode<T> node, Action<T> visit)
        {
            if (node == null) return;

            foreach (var child in node.Nodes)
            {
                PostOrderTraversal(child, visit);
            }

            visit(node.Value);
        }

        public void PostOrderTraversal(TreeNode<T> node, Func<T, T> visit)
        {
            if (node == null) return;

            foreach (var child in node.Nodes)
            {
                PostOrderTraversal(node, visit);
            }

            visit(node.Value);
        }
    }

    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Nodes { get; private set; }

        public TreeNode(T value)
        {
            this.Value = value;
            this.Nodes = [];
        }

        public void AddNode(TreeNode<T> node)
        {
            this.Nodes.Add(node);
        }

        public void PreOrderTraversal(TreeNode<T> node, Action<T> visit)
        {
            if (node == null) return;

            visit(node.Value);

            foreach (var child in node.Nodes)
            {
                PreOrderTraversal(child, visit);
            }
        }

        public void PreOrderTraversal(TreeNode<T> node, Func<T, T> visit)
        {
            if (node == null) return;

            visit(node.Value);

            foreach (var child in node.Nodes)
            {
                PreOrderTraversal(child, visit);
            }
        }

        public void PostOrderTraversal(TreeNode<T> node, Action<T> visit)
        {
            if (node == null) return;

            foreach (var child in node.Nodes)
            {
                PostOrderTraversal(child, visit);
            }

            visit(node.Value);
        }

        public void PostOrderTraversal(TreeNode<T> node, Func<T, T> visit)
        {
            if (node == null) return;

            foreach (var child in node.Nodes)
            {
                PostOrderTraversal(node, visit);
            }

            visit(node.Value);
        }
    }
    public class Utility
    {
        public static List<string> ReadProjectFile(string path)
        {
            return File.ReadAllLines($"..\\..\\..\\{path}").ToList();
        }
    }

    public static class StringExtensions
    {
        public static string ReplaceCharAt(this string input, char replacement, int index)
        {
            StringBuilder sb = new StringBuilder(input);
            sb[index] = replacement;
            return sb.ToString();
        }
    }
}
