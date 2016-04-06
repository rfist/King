using UnityEngine;
using System.Collections;

public class SoundMediator : MonoBehaviour
{

    public GameObject Icon;
    public GameObject IconDisabled;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IconDisabled.SetActive(!Config.IS_SOUNDS_ON);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Config.IS_SOUNDS_ON = !Config.IS_SOUNDS_ON;
        }
    }
}
