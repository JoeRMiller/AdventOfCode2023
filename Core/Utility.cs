using System.Collections.Generic;
using System.Linq;

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

        public static T[][] GetInputArray<T>(List<string> input)
        {
            var length = input.Count;

            T[][] array = new T[length][];

            for (int x = 0; x < length; x++)
            {
                var width = input[x].Length;
                T[] arrayLine = new T[width];
                for (int i = 0; i < width; i++)
                {
                    arrayLine[i] = ConvertToType<T>(input[x][i]);
                }
                array[x] = arrayLine;
            }

            return array;
        }

        private static T ConvertToType<T>(char character)
        {
            object result = null;


            if (typeof(T) == typeof(char))
            {
                result = character;
            }
            else if (typeof(T) == typeof(int))
            {
                result = (int)character;
            }
            
            else
            {
                throw new InvalidOperationException($"Conversion to type {typeof(T)} is not supported.");
            }

            return (T)result;
        }
    }

    public static class ListExtensions
    {
        public static List<T> SubList<T>(this List<T> list, int startIndex)
        {
            return list.SubList(startIndex, list.Count - startIndex);
        }
        public static List<T> SubList<T>(this List<T> list, int startIndex, int  endIndex)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (startIndex < 0 || endIndex < 0 || startIndex > endIndex || endIndex >= list.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return list.GetRange(startIndex, endIndex - startIndex + 1);
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
