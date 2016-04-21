using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchKingMediator : MonoBehaviour {

    private bool IsSelected = false;
    private SpriteRenderer renderer;
    public bool IsKing2 = false;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        EventManager.StartListening(EventManager.MENU_DESELECT_ALL, Deselect);
    }
	
	// Update is called once per frame
	void Update () {
        renderer.enabled = IsSelected;
	}

    void Deselect()
    {
        IsSelected = false;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameController.inst.currentLevelIndex = -1;
            if (!IsSelected)
            {
                EventManager.TriggerEvent(EventManager.MENU_DESELECT_ALL);
                IsSelected = true;
            }
            else
            {
                GameModel.inst.IsOriginalKing = !IsKing2;
                startGame();
            }
        }
    }

    void startGame()
    {
        SceneManager.LoadScene("SelectPlayer");
    }
}
