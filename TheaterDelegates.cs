using System;

namespace HomeTheaterApp
{
    // Определение делегатов для управления подсистемами
    public delegate void LightsAction(int level); // Для методов с параметром (например, Dim)
    public delegate void LightsToggleAction(); // Для методов без параметров (например, On, Off)
    public delegate void ScreenAction();
    public delegate void ProjectorAction();
    public delegate void ProjectorSetInput(string input);
    public delegate void AmplifierAction();
    public delegate void AmplifierSetVolume(int level);
    public delegate void CdPlayerAction();
}