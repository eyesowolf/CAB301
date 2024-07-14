using System;
using System.IO;
namespace Assignment2
{
	public class TreeGenerator
	{
        private static ToolCollection? tree;

        static public void Main(String[] args)
        {
            tree = new ToolCollection();
            for (int depth = 4; depth <= 23; depth++)
            {
                Console.WriteLine("Depth: " + depth);
                for (int i = 1; i <= 20; i++)
                {
                    Console.WriteLine("Test#: " + i);
                    GenerateTree(depth);
                    tree.ToArray();
                    tree.Clear();
                }
                Console.WriteLine("========================================");
            }
        }

        public static string ToBase26(int num)
        {
            string[] alpha = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] name = new string[0];

            string[] addChar(string[] name, string newValue)
            {
                int len = name.Length + 1;
                string[] oldName = name;
                name = new string[len];
                for (int i = 0; i < oldName.Length; i++)
                {
                    name[i] = oldName[i];
                }
                name[len - 1] = newValue;
                return name;
            }

            int i = 0;
            while (num >= 26)
            {
                int value = num % 26;
                if (value == 0)
                {
                    num /= 25;
                    name = addChar(name, "a");
                } else
                {
                    num /= 26;
                    name = addChar(name, alpha[value]);
                }
            }
            if (num > 0)
            {
                name = addChar(name, alpha[num]);
            }
            return string.Join("",name);
        }

        public static void GenerateTree(int Depth)
		{
            Random rnd = new Random();
            int nodesThisRow = 1;
            tree = new ToolCollection();
            int i;
            for (i = Depth; i >= 1; i--)
            {
                int lmna = (int)Math.Pow(2, i);
                if (lmna >= 2147483591)
                {
                    return;
                }
                int name = lmna >> 1;
                for (int j = 0; j < nodesThisRow; j++)
                {
                    var node = new Tool(ToBase26(name));
                    int random = rnd.Next(0, 100);
                    if (random <= 30)
                    {
                        continue;
                    }
                    tree.Insert(node);
                    name += lmna;
                }
                nodesThisRow *= 2;
            }
            return;
		}
	}
}