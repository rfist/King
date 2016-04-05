using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectPlayerMediator : MonoBehaviour {

    public Text text;
    public SpriteRenderer sprite;
    public int Type;

    bool AlreadySelected = false;
	// Use this for initialization
	void Start () {
        text.text = "";        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !AlreadySelected)
        {
            string Name = "";
            string Description1 = "";
            string Description2 = "";

            AlreadySelected = true;
          
            sprite.enabled = false;

            AudioManager.inst.playSelect();

            switch (Type)
            {
                case 101:
                    Name = "Винни Пух";
                    Description1  = "ОН ИГРАЕТ";
                    Description2 = "НЕПЛОХО";
                    break;

                case 102:
                    Name = "Кролик";
                    Description1 = "ОН ИГРАЕТ";
                    Description2 = "НЕПЛОХО";
                    break;

                case 103:
                    Name = "Иа-Иа";
                    Description1 = "ОН ИГРАЕТ";
                    Description2 = "НЕПЛОХО";
                    break;

                case 104:
                    Name = "Пятачок";
                    Description1 = "ОН ИГРАЕТ";
                    Description2 = "НЕПЛОХО";
                    break;

                case 105:
                    Name = "Фрекен Бок";
                    Description1 = "ОНА ИГРАЕТ";
                    Description2 = "ОТЛИЧНО";
                    break;

                case 106:
                    Name = "Багира";
                    Description1 = "ОНА ИГРАЕТ";
                    Description2 = "ОТЛИЧНО";
                    break;

                case 107:
                    Name = "Сова";
                    Description1 = "ОНА ИГРАЕТ";
                    Description2 = "ОТЛИЧНО";
                    break;

                case 108:
                    Name = "Оля";
                    Description1 = "ОНА ИГРАЕТ";
                    Description2 = "ОТЛИЧНО";
                    break;

                case 109:
                    Name = "Мишка";
                    Description1 = "ОН ВСЕГДА";
                    Description2 = "МУХЛЮЕТ";
                    break;

                case 110:
                    Name = "Башуров";
                    Description1 = "ОН ВСЕГДА";
                    Description2 = "МУХЛЮЕТ";
                    break;

                case 111:
                    Name = "Карлсон";
                    Description1 = "ОН ВСЕГДА";
                    Description2 = "МУХЛЮЕТ";
                    break;

                case 112:
                    Name = "Борька";
                    Description1 = "ОН ВСЕГДА";
                    Description2 = "МУХЛЮЕТ";
                    break;

            }

            text.text = Name + "\n" + SelectPlayerContext.Count + " - й" + "\n" + "ПАРТНЕР" + "\n" + Description1 + "\n" + Description2;


            string ContainerName = "";
            switch (SelectPlayerContext.Count)
            {
                case 1:
                    ContainerName = "PlayerA";
                    break;
                case 2:
                    ContainerName = "PlayerB";
                    break;
                case 3:
                    ContainerName = "PlayerC";
                    break;
            }
            PlayerVO player = new PlayerVO(SelectPlayerContext.Count, "", Type.ToString(), ContainerName);


            GameModel.inst.Players[SelectPlayerContext.Count] = player;

            SelectPlayerContext.Count += 1;

            if (SelectPlayerContext.Count > 3)
            {
                SceneManager.LoadScene("GameField");
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
