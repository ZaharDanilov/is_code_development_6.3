using System;

namespace HomeTheaterApp
{
    public class MusicModeStrategy : ITheaterModeStrategy
    {
        private readonly LightsAction _dimLights;
        private readonly LightsToggleAction _lightsOn;
        private readonly AmplifierAction _amplifierOn;
        private readonly AmplifierAction _amplifierOff;
        private readonly AmplifierSetVolume _amplifierSetVolume;
        private readonly CdPlayerAction _cdPlayerOn;
        private readonly CdPlayerAction _cdPlayerOff;
        private readonly CdPlayerAction _cdPlayerPlay;
        private readonly CdPlayerAction _cdPlayerStop;
        private bool _isActive;

        // Конструктор для музыкального режима
        public MusicModeStrategy(
            LightsAction dimLights, LightsToggleAction lightsOn,
            AmplifierAction amplifierOn, AmplifierAction amplifierOff, AmplifierSetVolume amplifierSetVolume,
            CdPlayerAction cdPlayerOn, CdPlayerAction cdPlayerOff, CdPlayerAction cdPlayerPlay, CdPlayerAction cdPlayerStop)
        {
            _dimLights = dimLights;
            _lightsOn = lightsOn;
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
                throw new InvalidOperationException("Музыкальный режим уже активен!");

            string log = "Подготовка к музыкальному режиму...\n";
            _dimLights(20); // Передаём уровень яркости
            log += "Освещение: яркость установлена на 20%.\n";
            _amplifierOn();
            log += "Усилитель включён.\n";
            _amplifierSetVolume(30);
            log += "Усилитель: громкость установлена на 30.\n";
            _cdPlayerOn();
            log += "CD-плеер включён.\n";
            _cdPlayerPlay();
            log += "CD-плеер: воспроизведение начато.\n";
            _isActive = true;
            log += "Музыкальный режим начат!\n";
            return log;
        }

        public string EndMode()
        {
            if (!_isActive)
                throw new InvalidOperationException("Музыкальный режим не активен!");

            string log = "Завершение музыкального режима...\n";
            _cdPlayerStop();
            log += "CD-плеер: воспроизведение остановлено.\n";
            _cdPlayerOff();
            log += "CD-плеер выключен.\n";
            _amplifierOff();
            log += "Усилитель выключен.\n";
            _lightsOn(); // Используем LightsToggleAction без параметров
            log += "Освещение включено.\n";
            _isActive = false;
            log += "Музыкальный режим завершён!\n";
            return log;
        }

        public string GetStatus()
        {
            return _isActive ? "Режим: Музыкальный режим активен." : "Режим: Музыкальный режим не активен.";
        }
    }
}