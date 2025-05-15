using System;

namespace HomeTheaterApp
{
    public class MovieModeStrategy : ITheaterModeStrategy
    {
        private readonly LightsAction _dimLights;
        private readonly LightsToggleAction _lightsOn;
        private readonly ScreenAction _screenDown;
        private readonly ScreenAction _screenUp;
        private readonly ProjectorAction _projectorOn;
        private readonly ProjectorAction _projectorOff;
        private readonly ProjectorSetInput _projectorSetInput;
        private readonly AmplifierAction _amplifierOn;
        private readonly AmplifierAction _amplifierOff;
        private readonly AmplifierSetVolume _amplifierSetVolume;
        private readonly CdPlayerAction _cdPlayerOn;
        private readonly CdPlayerAction _cdPlayerOff;
        private readonly CdPlayerAction _cdPlayerPlay;
        private readonly CdPlayerAction _cdPlayerStop;
        private bool _isActive;

        // Конструктор, принимающий делегаты для управления подсистемами
        public MovieModeStrategy(
            LightsAction dimLights, LightsToggleAction lightsOn,
            ScreenAction screenDown, ScreenAction screenUp,
            ProjectorAction projectorOn, ProjectorAction projectorOff, ProjectorSetInput projectorSetInput,
            AmplifierAction amplifierOn, AmplifierAction amplifierOff, AmplifierSetVolume amplifierSetVolume,
            CdPlayerAction cdPlayerOn, CdPlayerAction cdPlayerOff, CdPlayerAction cdPlayerPlay, CdPlayerAction cdPlayerStop)
        {
            _dimLights = dimLights;
            _lightsOn = lightsOn;
            _screenDown = screenDown;
            _screenUp = screenUp;
            _projectorOn = projectorOn;
            _projectorOff = projectorOff;
            _projectorSetInput = projectorSetInput;
            _amplifierOn = amplifierOn;
            _amplifierOff = amplifierOff;
            _amplifierSetVolume = amplifierSetVolume;
            _cdPlayerOn = cdPlayerOn;
            _cdPlayerOff = cdPlayerOff;
            _cdPlayerPlay = cdPlayerPlay;
            _cdPlayerStop = cdPlayerStop;
            _isActive = false;
        }

        public string StartMode()
        {
            if (_isActive)
                throw new InvalidOperationException("Режим просмотра фильма уже активен!");

            string log = "Подготовка к просмотру фильма...\n";
            _dimLights(10); // Передаём уровень яркости
            log += "Освещение: яркость установлена на 10%.\n";
            _screenDown();
            log += "Экран опущен.\n";
            _projectorOn();
            log += "Проектор включён.\n";
            _projectorSetInput("CD");
            log += "Проектор: установлен источник CD.\n";
            _amplifierOn();
            log += "Усилитель включён.\n";
            _amplifierSetVolume(50);
            log += "Усилитель: громкость установлена на 50.\n";
            _cdPlayerOn();
            log += "CD-плеер включён.\n";
            _cdPlayerPlay();
            log += "CD-плеер: воспроизведение начато.\n";
            _isActive = true;
            log += "Просмотр фильма начат!\n";
            return log;
        }

        public string EndMode()
        {
            if (!_isActive)
                throw new InvalidOperationException("Режим просмотра фильма не активен!");

            string log = "Завершение просмотра фильма...\n";
            _cdPlayerStop();
            log += "CD-плеер: воспроизведение остановлено.\n";
            _cdPlayerOff();
            log += "CD-плеер выключен.\n";
            _amplifierOff();
            log += "Усилитель выключен.\n";
            _projectorOff();
            log += "Проектор выключен.\n";
            _screenUp();
            log += "Экран поднят.\n";
            _lightsOn(); // Используем LightsToggleAction без параметров
            log += "Освещение включено.\n";
            _isActive = false;
            log += "Просмотр фильма завершён!\n";
            return log;
        }

        public string GetStatus()
        {
            return _isActive ? "Режим: Просмотр фильма активен." : "Режим: Просмотр фильма не активен.";
        }
    }
}