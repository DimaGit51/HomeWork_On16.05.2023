using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Program
{
    public class ArNode
    {
        private double info; 
        private char pr; 
        private ArNode left; 
        private ArNode right; 
                            
        public double Info
        {
            get { return info; }
            set { info = value; }
        }
        public char Pr
        {
            get { return pr; }
            set { pr = value; }
        }
        public ArNode Left
        {
            get { return left; }
            set { left = value; }
        }
        public ArNode Right
        {
            get { return right; }
            set { right = value; }
        }
        public ArNode() { }
        public ArNode(double info, char pr)
        { Info = info; Pr = pr; }

        public ArNode(double info, char pr, ArNode left, ArNode right)
        { Info = info; Pr = pr; Left = left; Right = right; }
    }
    public class ArTree // Класс «Дерево арифметических выражений»
    {
        private ArNode root;
        static public string math = "";
        string xx = "";
        string yy = "";
        public ArNode Root
        {
            get
            { return root; }
            set
            { root = value; }
        }
        public ArTree() // Конструктор
        {
            root = null;
        }
        // Методы
        public ArNode CreateRecursive(int n)
        {
            Console.WriteLine(n);
            double x; char p; ArNode root;
            if (n == 0) root = null;
            else
            {

                Console.Write("Введите Операнд = ");
                while ((!double.TryParse(Console.ReadLine(), out x)))
                {
                    Console.Write("Вы ввели не ^double^! Введите double: ");
                }
                Console.Write("Введите Оператор = ");
                while ((!char.TryParse(Console.ReadLine(), out p)))
                {
                    Console.Write("Вы ввели не ^char^! Введите char: ");
                }
                root = new ArNode(x, p);
                root.Left = CreateRecursive(n / 2);
                root.Right = CreateRecursive(n - n / 2 - 1);

            }
            return root;
        }
        public int RecursionCount(ArNode root)
        {
            int count;
            if (root == null) count = 0;
            else count = 1 + RecursionCount(root.Left) + RecursionCount(root.Right);
            return count;
        }
        public void PrintBalancedTreeByLevel(ArNode root)
        {
            int height = RecursionCount(root);
            for (int i = 1; i <= height; i++)
            {
                PrintLevel(root, i);
                Console.WriteLine();
            }
        }

        public void PrintLevel(ArNode root, int level)
        {
            if (root != null)
            {
                if (level == 1)
                    if (root.Pr == 'n') Console.Write(root.Info + " ");
                    else Console.Write(root.Pr + " ");


                else if (level > 1)
                {
                    PrintLevel(root.Left, level - 1);
                    PrintLevel(root.Right, level - 1);
                }
            }
        }
        public void LKP(ArNode root) // root – ссылка на корень дерева и любого из деревьев
        {
            if (root != null) // дерево не пусто?
            {
                LKP(root.Left); // обойти левое поддерево в нисходящем порядке
                if (root.Pr == 'n') Console.Write(root.Info + " ");
                else Console.Write(root.Pr + " ");
                LKP(root.Right); // обойти правое поддерево в нисходящем порядке
            }
        }

        public void Print(ArNode root) // root – ссылка на корень дерева и любого из деревьев
        {
            if (root != null) // дерево не пусто?
            {
                Print(root.Left); // обойти левое поддерево в нисходящем порядке
                if (root.Pr == 'n')
                {
                    if (xx.Length > 0) { yy = root.Info.ToString(); math += yy+")"; xx = ""; yy = ""; }
                    else { xx = root.Info.ToString(); math += "(" + xx; }
                }
                else
                {
                    math += root.Pr;
                }
                Print(root.Right); // обойти правое поддерево в нисходящем порядке
            }
        }
        
    }
    

    public class Program
    {
        static void Main()
        {
            ArTree Ar = new ArTree();
            Ar.Root = Ar.CreateRecursive(3);
            Console.WriteLine("==========PRINT===========");
            Ar.Print(Ar.Root);
            string value = new DataTable().Compute(ArTree.math, null).ToString();
            Console.WriteLine(value);

        }
    }
}