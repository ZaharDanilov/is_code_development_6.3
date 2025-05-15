using System;

namespace HomeTheaterApp
{
    public class Projector
    {
        public void On() => Console.WriteLine("Проектор включён.");
        public void Off() => Console.WriteLine("Проектор выключен.");
        public void SetInput(string input) => Console.WriteLine($"Проектор: установлен источник {input}.");
    }
}