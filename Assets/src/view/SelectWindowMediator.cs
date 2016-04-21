using UnityEngine;
using System.Collections;

public class SelectWindowMediator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Hide();
        EventManager.StartListening(EventManager.SHOW_SELECT_WINDOW, Show);
        EventManager.StartListening(EventManager.HIDE_SELECT_WINDOW, Hide);
    }

    void Show ()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
