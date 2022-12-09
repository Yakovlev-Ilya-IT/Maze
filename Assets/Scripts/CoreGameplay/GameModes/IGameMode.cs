using System;

public interface IGameMode
{
    event Action LevelComplete;
    void StartLevel();
}
