using System;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Threading;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(FirstThread);
            Thread thread2 = new Thread(SecondThread);

            thread1.Start();
            thread2.Start();



            static void FirstThread()
            {
                Console.WriteLine("[Поток 1] Запуск потока.");
                var psi = new ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    UseShellExecute = true
                };
                Process process = Process.Start(psi);

                Console.WriteLine("[Поток 1] Ожидание 3 секунды...");
                Thread.Sleep(3000);
                Console.WriteLine("[Поток 1] Закрытие Notepad.");
                process.Kill();
            }
            static void SecondThread()
            {
                Console.WriteLine("[Поток 2] Запуск потока.");
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    UseShellExecute = false,
                    Arguments = "/c dir",
                    RedirectStandardOutput = true
                };
                Console.WriteLine("[Поток 2] Запуск командной строки.");
                Process process = Process.Start(psi);

                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine("[Поток 2] Результат команды: {0}", result);
            }

        }
    }
}