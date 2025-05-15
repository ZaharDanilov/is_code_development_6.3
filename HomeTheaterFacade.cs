using System;

namespace HomeTheaterApp
{
    // Фасад для управления кинотеатром через стратегию
    public class HomeTheaterFacade
    {
        private ITheaterModeStrategy _strategy;

        public HomeTheaterFacade(ITheaterModeStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy), "Стратегия не может быть null.");
        }

        public void SetStrategy(ITheaterModeStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy), "Стратегия не может быть null.");
        }

        public string StartMode() => _strategy.StartMode();
        public string EndMode() => _strategy.EndMode();
        public string GetStatus() => _strategy.GetStatus();
    }
}