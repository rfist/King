using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPlayerContext : MonoBehaviour {

    public static int Count = 1;
    public Text text;
    // Use this for initialization
    void Start () {
        Count = 1;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Выберите " + Count + "-го партнера...";
    }
}
