using UnityEngine;
using System.Collections;

public class GameEventManager {
    public delegate void GameEvent();

    public static event GameEvent GameStart, GameOver;

    public static void TriggerGameStart()
    {
        Debug.Log("starting");
        if(GameStart != null)
        {
            GameStart();
        }
    }

    public static void TriggerGameOver()
    {
        Debug.Log("ending");
        if(GameOver != null)
        {
            GameOver();
        }
    }
}