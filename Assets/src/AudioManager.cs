using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip ErrorSound;
    public AudioClip EndTurn;
    public AudioClip Blinking;
    public AudioClip Grabbing;
    public AudioClip Results;
    public AudioClip Select;
    public AudioClip Prize;
    public AudioClip PrizeFinish;

    private static AudioManager instance = null;
    private AudioSource audio;


    public static AudioManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }

    public void playError()
    {   
        if (Config.IS_SOUNDS_ON)     
            audio.PlayOneShot(ErrorSound);
    }

    public void playEndTurn()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(EndTurn);
    }

    public void playGrab()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(Grabbing);
    }

    public void playBlinking()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(Blinking);
    }

    public void playResults()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(Results);
    }

    public void playSelect()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(Select);
    }

    public void playAddPrize()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(Prize);
    }

    public void playAllPrizesAdded()
    {
        if (Config.IS_SOUNDS_ON)
            audio.PlayOneShot(PrizeFinish);
    }

    // Use this for initialization
    void Start () {
        instance = this;
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
