using UnityEngine;
using System.Collections;

public class ShowScoreCommand : MonoBehaviour, ICommand
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void execute()
    {       
        EventManager.StartListening(EventManager.HIDE_SCORE_TABLE, startNextRound);
        EventManager.TriggerEvent(EventManager.SHOW_SCORE_TABLE);       
    }

    static void startNextRound()
    {
        Debug.Log("startNextRound " + GameController.inst.currentLevelIndex);
        EventManager.StopListening(EventManager.HIDE_SCORE_TABLE, startNextRound);
        GameController.inst.changeLevel();
    }
}
