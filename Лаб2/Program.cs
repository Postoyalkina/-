using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб2 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Программа для нахождения кратчайшего пути в лабиринте. Здесь \"1\" стенки лабиринта");
            Random random = new Random();
            int n = 10, m = 0;
            int[,] mas = new int[n, n], masC = new int[n, n];
            while (m != n*n*0.4) {
                m = 0;
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (random.Next(0, n * n) <= n * n * 0.4) mas[i, j] = 1;
                        else mas[i, j] = 0; 
                        m += mas[i, j];
                    }
                }
            }
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    Console.Write(mas[i, j]+" ");
                    if (j == n - 1) Console.WriteLine();
                    if (mas[i, j] == 0)
                        masC[i, j] = 0;
                    else
                        masC[i, j] = -1;
                }
            }
            int x1, y1, x2, y2;
            bool ex = false;
            Console.WriteLine("Координаты точек вводятся по таким правилам: \n1)Первая координата значение столбца, вторая - строки \n2)нумерация с нуля");
            do {
                ex = false;
                Console.WriteLine("Введите координаты начала: ");
                y1 = int.Parse(Console.ReadLine());
                x1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите координаты конца: ");
                y2 = int.Parse(Console.ReadLine());
                x2 = int.Parse(Console.ReadLine());
                if (mas[x1, y1] != 0 || mas[x2, y2] != 0) { ex = true; Console.WriteLine("Неверно указаны координаты. "); }
            }
            while (ex);
            masF(x1, y1, x2, y2, n, masC, mas);
            Console.Read();
        }
        public static void masF(int x1, int y1, int x2, int y2, int n, int[,] masC, int[,] mas) {
            int k = 1, i1, j1;
            masC[x1, y1] = k;
            bool b = true;
            try {
                while (k <= (n * n)) {
                    for (int i = 0; i < n; i++) {
                        for (int j = 0; j < n; j++) {
                            if (masC[i, j] == k) {
                                if ((i + 1) < n && masC[i + 1, j] == 0) masC[i + 1, j] = k + 1;
                                if ((j - 1) >= 0 && masC[i, j - 1] == 0) masC[i, j - 1] = k + 1;
                                if ((i - 1) >= 0 && masC[i - 1, j] == 0) masC[i - 1, j] = k + 1;
                                if ((j + 1) < n && masC[i, j + 1] == 0) masC[i, j + 1] = k + 1;
                                if (i == x2 && j == y2) {
                                    i1 = x2;
                                    j1 = y2;
                                    b = false;
                                }
                            }
                        }
                    }
                    if (k >= n * n) throw new Exception();
                    k += 1;
                    if (!b) break;
                }
                
                k = masC[x2, y2];
                do {
                    bool b1 = false;
                    for (int i = 0; (i < n) && !b1; i++) {
                        for (int j = 0; (j < n) && !b1; j++) {
                            if (i == x2 && j == y2) {
                                mas[i, j] = 2;
                                if ((i + 1) < n && masC[i + 1, j] == k - 1) {
                                    x2 = i + 1; y2 = j;
                                    k -= 1;
                                    b1 = true;
                                }
                                if ((j - 1) >= 0 && masC[i, j - 1] == k - 1) {
                                    x2 = i; y2 = j - 1;
                                    k -= 1;
                                    b1 = true;
                                }
                                if ((i - 1) >= 0 && masC[i - 1, j] == k - 1) {
                                    x2 = i - 1; y2 = j;
                                    k -= 1;
                                    b1 = true;
                                }
                                if ((j + 1) < n && masC[i, j + 1] == k - 1) {
                                    x2 = i; y2 = j + 1;
                                    k -= 1;
                                    b1 = true;
                                }
                                if (x2 == x1 && y2 == y1) {
                                    mas[x2, y2] = 2;
                                    b = true;
                                }
                            }
                        }
                    }
                    if (b) break;
                }
                while (k != 1);
                Console.WriteLine("Ваш путь: ");
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (mas[i, j] == 2) {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("0 ");
                            Console.ResetColor();
                            if (j == n - 1) Console.WriteLine();
                        }
                        else {
                            Console.Write(mas[i, j] + " ");
                            if (j == n - 1) Console.WriteLine();
                        }
                    }
                }            
            }
            catch(Exception) {
                Console.WriteLine("Пути нет!");
            }
        }
    }
}
