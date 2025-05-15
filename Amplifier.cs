using System;

namespace HomeTheaterApp
{
    public class Amplifier
    {
        public void On() => Console.WriteLine("Усилитель включён.");
        public void Off() => Console.WriteLine("Усилитель выключен.");
        public void SetVolume(int level) => Console.WriteLine($"Усилитель: громкость установлена на {level}.");
    }
}