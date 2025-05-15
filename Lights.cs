using System;

namespace HomeTheaterApp
{
    public class Lights
    {
        // Методы для управления освещением
        public void On() => Console.WriteLine("Освещение включено.");
        public void Off() => Console.WriteLine("Освещение выключено.");
        public void Dim(int level) => Console.WriteLine($"Освещение: яркость установлена на {level}%.");
    }
}