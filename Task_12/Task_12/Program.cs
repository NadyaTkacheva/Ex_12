using System;
using System.Diagnostics;

namespace Task_12
{
    /// <summary>
    /// класс для работы с деревом
    /// </summary>
    public class Tree
    {
        public int data;//значение
        public Tree left;//левое значение
        public Tree right;//правое значение
        public static int countChange = 0;// счетчик пересылок
        //конструктор без параметров
        public Tree()
        {
            data = 0;
            left = null;
            right = null;
        }
        //конструктор с параметрами
        public Tree(int v)
        {
            data = v;
            left = null;
            right = null;
        }
       
        /// <summary>
        /// вывод дерева
        /// </summary>
        /// <param name="tree"></param>
       public static void Treeprint(Tree tree)
        {
            if (tree != null) //если не пустой узел
            {     
                Treeprint(tree.left);  // вывод левого поддерева
                Console.Write(tree.data + " "); // корень дерева
                Treeprint(tree.right); // вывод правого поддерева
            }
        }

        /// <summary>
        /// Добавление значений в дерево
        /// </summary>
        /// <param name="x"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static Tree AddNode(int x, Tree tree)
        {
            if (tree == null)     // Если дерева нет, то формируем корень
            {
                tree = new Tree();
                tree.data = x;
                tree.left = null;
                tree.right = null;
            }
            else     //если дерево есит
            {
                countChange++;
                if (x < tree.data)   //x меньше корня -  уходим влево
                    tree.left = AddNode(x, tree.left); // добавляем элемент
                else  //x больше корня -  уходим влево
                    tree.right = AddNode(x, tree.right); // добавляем элемент
            }
            return (tree);
        }
    }
    
        class Program
    {
        delegate void Sort(int[] arr);//делегат для сортировки
        
        /// <summary>
        /// сортировка простым выбором
        /// </summary>
        /// <param name="arr"></param>
        static void SortSimple(int[] arr)
        {
            Stopwatch time = Stopwatch.StartNew(); //счетчик времени
            int countChange = 0; // счетчик пересылок
            int countСompare = 0; //счетчик сравнений
            int length = arr.Length; 

            for (int i = 0; i < length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < length; j++) // поиск минимального значения
                {
                    countСompare++;
                    if (arr[j] < arr[min])
                        min = j;
                }

                if (i != min)
                {
                    countChange++;
                    int temp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = temp;
                }
            }
            Console.WriteLine("Отсортированный массив: " + String.Join(", ", arr));
            Console.WriteLine("Затрачено {0} тиков, {1} сравнений, {2} перессылок", time.ElapsedTicks, countСompare, countChange);
            time.Reset();
        }
       
       /// <summary>
       /// сортировка деревом
       /// </summary>
       /// <param name="arr"></param>
        static void SortTree(int[] arr)
        {
            Stopwatch time = Stopwatch.StartNew(); // счетчик
            int countСompare = 0; // счетчиков операций    
            Tree.countChange = 0;// счетчик кол-ва проходов
            int length = arr.Length; 
            Tree root = null;
            for (int i = 0; i < arr.Length; i++)
            {
                countСompare++;
                root = Tree.AddNode(arr[i], root);
            }

           
            Console.Write("Отсортированный массив: " );
            Tree.Treeprint(root);
            Console.WriteLine("\nЗатрачено {0} тиков, {1} сравнений, {2} перессылок", time.ElapsedTicks, countСompare, Tree.countChange);
            time.Reset();//обнуление времени
        }
       
        static void Get(int[] arr, Sort sortMas)
        {
            Console.WriteLine("Неупорядоченный массив: " + String.Join(", ", arr));
            //сортировка неотсортированного массива
            sortMas(arr);
            Console.WriteLine("\nМассив, упорядоченный по возрастанию: " + String.Join(", ", arr));
            //сортировка упорядоченного по возрастанию массива
            sortMas(arr);
            Array.Reverse(arr);
            //сортировка упорядоченного по убыванию
            Console.WriteLine("\nМассив, упорядоченный по убыванию: " + String.Join(", ", arr));
            sortMas(arr);
        }
        static void Main(string[] args)
        {
            Random r = new Random();
            var tmp = new int[10];
            var schange = new Sort(SortSimple);
            var stree = new Sort(SortTree);
            // элементы массива
            var arr = new int[] { 32, 17, 9, 198, 67, 2, 3, 67, 89, 25, 849, 183, 1, 0, 813 };
            //вывод массива
            Console.WriteLine(String.Join(", ", arr));
            //клонирование массива для сортировок
            var simple = (int[])arr.Clone();
            var tree = (int[])arr.Clone();
            //выполнение заданий
            Console.WriteLine("Простой выбор:");
            Get(simple, schange);
            Console.WriteLine("\nДерево: ");
            Get(tree, stree);
            Console.ReadKey();

        }
    }
}

