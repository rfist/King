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
        //gameObject.active = false;
        //SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        //gameObject.

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
