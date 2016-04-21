using UnityEngine;
using System.Collections;

public class RuleBoxMediator : MonoBehaviour {

    public GameObject BackgroundPlus;
    public GameObject BackgroundMinus;
    public GameObject BackgroundDark;

    private static float BLINK_TIME = 0.15f;

    private bool IsAnimated = false;
    private bool IsBlinking = false;
    private float Timer = BLINK_TIME;
    private int AnimationCount = 3;


    // Use this for initialization
    void Start () {
        EventManager.StartListening(Config.ON_LEVEL_CHANGED, onLevelChanged);
        EventManager.StartListening(Config.ON_START_ANIMATION_FINISHED, onStartGame);
        onLevelChanged();
    }

    void onLevelChanged()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(GameModel.inst.level.RuleImage);
        BackgroundMinus.SetActive(GameModel.inst.level.isNegative);
        BackgroundPlus.SetActive(!GameModel.inst.level.isNegative);
        gameObject.SetActive(true);
    }

    void onStartGame()
    {
        AnimationCount = 5;
        Timer = BLINK_TIME;
        IsAnimated = true;
        AudioManager.inst.playBlinking();
    }

    // Update is called once per frame
    void Update () {
        bool IsActive = GameModel.inst.GameStatus != Config.GAME_STATUS_SELECT_LEVEL;
        if (!IsActive)
        {
            gameObject.SetActive(IsActive);
            BackgroundPlus.SetActive(IsActive);
            BackgroundMinus.SetActive(IsActive);
            BackgroundDark.SetActive(IsActive);
        }

        if (IsAnimated)
        {
            GameObject go = GameModel.inst.level.isNegative ? BackgroundMinus : BackgroundPlus;
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                if (IsBlinking)
                {
                    go.SetActive(true);
                    AnimationCount -= 1;
                    IsBlinking = false;
                }
                else
                {
                    go.SetActive(false);
                    IsBlinking = true;
                }
                Timer = BLINK_TIME;
                if (AnimationCount <= 0)
                {
                    go.SetActive(true);
                    IsAnimated = false;
                }
            }
        }
    }
}
