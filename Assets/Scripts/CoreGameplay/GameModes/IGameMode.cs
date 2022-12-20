using System;

public interface IGameMode
{
    event Action LevelCompleted;
    void StartLevel();
}
