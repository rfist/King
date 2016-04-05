using UnityEngine;
using System.Collections;

public class MaskTableMediator : MonoBehaviour {

    public Material[] materials;
    public Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
        EventManager.StartListening(EventManager.SHOW_SCORE_TABLE, Show);

        EventManager.StartListening(EventManager.HIDE_SCORE_TABLE, Hide);
        gameObject.SetActive(false);
    }

    void Show()
    {
        rend.enabled = true;
        rend.sharedMaterial = materials[1];
        gameObject.SetActive(true);
    }

    void Hide()
    {
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
