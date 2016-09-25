using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public Text gameOverText, instructionsText, gameNameText, counter;

	void Awake () {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        gameNameText.enabled = true;
        instructionsText.enabled = true;
        gameOverText.enabled = false;
        counter.enabled = false;
	}
	
	void Update () {
        counter.text = ((int)Runner.distanceTraveled).ToString();
#if UNITY_ANDROID
        if(Input.GetTouch(0).phase == TouchPhase.Began && gameNameText.enabled)
#elif UNITY_WINDOWS || UNITY_EDITOR
        if(Input.GetButtonDown("Jump") && gameNameText.enabled)
#endif
        {
            GameEventManager.TriggerGameStart();
        }
	}

    private void GameStart()
    {
        gameOverText.enabled = false;
        instructionsText.enabled = false;
        gameNameText.enabled = false;
        counter.enabled = true;
    }

    private void GameOver()
    {
        gameOverText.enabled = true;
        gameNameText.enabled = true;
    }
}
