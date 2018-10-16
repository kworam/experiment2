using System.Collections.Generic;

namespace Experiment.Permutation
{
    public class HeapAlgorithm
    {
        public static List<string> GetPerms(string s)
        {
            if (s == null)
            {
                return null;
            }

            
            List<string> perms = new List<string>();
            HeapPerms(s.ToCharArray(), s.Length, perms);
            return perms;
        }

        private static void HeapPerms(char[] arr, int size, List<string> perms)
        {
            if (size <= 1)
            {
                perms.Add(new string(arr));
                return;
            }

            for (int i=0; i<size; i++)
            {
                HeapPerms(arr, size-1, perms);
                if (size % 2 == 1)
                {
                    Swap(arr, 0, size - 1);
                }
                else
                {
                    Swap(arr, i, size - 1);
                }
            }
        }

        private static void Swap<T>(T[] arr, int x, int y)
        {
            T temp = arr[x];
            arr[x] = arr[y];
            arr[y] = temp;
        }
    }
}
