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
        public void LPK(ArNode root) // root – ссылка на корень дерева и любого из деревьев
        {
            if (root != null) // дерево не пусто?
            {
                LPK(root.Left); // обойти левое поддерево в нисходящем порядке
                LPK(root.Right); // обойти правое поддерево в нисходящем порядке
                if (root.Pr == 'n') Console.Write(root.Info + " ");
                else Console.Write(root.Pr + " ");
            }
        }
        static public int Height(ArNode root, int count) // root – ссылка на корень дерева и любого из деревьев
        {
            if (root != null) // дерево не пусто?
            {
                count = Height(root.Left, count); // обойти левое поддерево в нисходящем порядке
                count++;
            }
            return count;
        }
        public double ArTreeSumm(ArNode root)
        {
            double sum = 0;
            if (root != null)
            {
                if (root.Left == null && root.Right == null)
                    sum = root.Info;
                double leftEval = ArTreeSumm(root.Left);
                double rightEval = ArTreeSumm(root.Right);
                if (root.Pr.Equals('+'))
                    sum = leftEval + rightEval;
                else if (root.Pr.Equals('-'))
                    sum = leftEval - rightEval;
                else if (root.Pr.Equals('*'))
                    sum = leftEval * rightEval;
                else if (root.Pr.Equals('/'))
                    sum = leftEval / rightEval;
            }
            return sum;
        }
    }
    public class Program
    {
        static void Main()
        {
            int n;
            Console.Write("Введите Количество узлов = ");
            while ((!int.TryParse(Console.ReadLine(), out n)))
            {
                Console.Write("Вы ввели не ^int^! Введите int: ");
            }
            ArTree Ar = new ArTree();
            Ar.Root = Ar.CreateRecursive(n);
            Console.WriteLine("==========PRINT Tree===========");
            Ar.LPK(Ar.Root);
            Console.WriteLine();
            Console.WriteLine("==========PRINT SummTree===========");
            Console.WriteLine(Ar.ArTreeSumm(Ar.Root));
        }
    }
}