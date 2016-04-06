using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchKingMediator : MonoBehaviour {

    private bool IsSelected = false;
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        renderer.enabled = IsSelected;
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsSelected)
            {
                IsSelected = true;
            }
            else
            {
                startGame();
            }
        }
    }

    void startGame()
    {
        SceneManager.LoadScene("SelectPlayer");
    }
}
