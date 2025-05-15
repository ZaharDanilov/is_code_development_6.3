using System;

namespace HomeTheaterApp
{
    public class CdPlayer
    {
        // Методы для управления воспроизведением CD
        public void On() => Console.WriteLine("CD-плеер включён.");
        public void Off() => Console.WriteLine("CD-плеер выключен.");
        public void Play() => Console.WriteLine("CD-плеер: воспроизведение начато.");
        public void Stop() => Console.WriteLine("CD-плеер: воспроизведение остановлено.");
    }
}