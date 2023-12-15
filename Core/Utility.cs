using System.Collections.Generic;
using System.Linq;

namespace AdventofCode2023.Core
{
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
}
