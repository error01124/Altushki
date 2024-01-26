using UnityEngine;

public class GameController : IService
{
    private Storyline _storyline;

    public GameController()
    {
        _storyline = ServiceLocator.Instance.Get<Storyline>();  
    }

    public void StartGame(ISaveData? data)
    {
        Debug.Log("StartGame");
        _storyline.Start();
    }

    public void StopGame()
    {

    }
}
