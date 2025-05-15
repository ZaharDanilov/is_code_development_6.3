using System;

namespace HomeTheaterApp
{
    // Интерфейс для стратегий управления режимами кинотеатра
    public interface ITheaterModeStrategy
    {
        // Метод - начала работы режима
        string StartMode();
        // Метод - завершения работы режима
        string EndMode();
        // Метод - получения текущего статуса
        string GetStatus();
    }
}