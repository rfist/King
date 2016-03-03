using UnityEngine;
using System.Collections;

public class EndLevelScoreMediator : MonoBehaviour {

    public Material[] materials;
    public Renderer rend;

    private GameObject scorePlayerA;
    private GameObject scorePlayerB;
    private GameObject scorePlayerC;


    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerA = gameObject.transform.Find("ScorePlayerA").gameObject;
        rend = scorePlayerA.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerB = gameObject.transform.Find("ScorePlayerB").gameObject;
        rend = scorePlayerB.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];

        scorePlayerC = gameObject.transform.Find("ScorePlayerC").gameObject;
        rend = scorePlayerC.GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
