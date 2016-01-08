using UnityEngine;
using System.Collections;

public class RuleBoxMediator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening(Config.ON_LEVEL_CHANGED, onLevelChanged);
        onLevelChanged();
    }

    void onLevelChanged()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(GameModel.inst.level.RuleImage);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
