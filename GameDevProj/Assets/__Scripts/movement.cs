using System.Linq;
using UnityEngine;
public class movement : basePlayer
{
    public GameManager gm;
    public Transform tr;
    public int speed = 1;

    [SerializeField] public basePlayer Instance;
    public playerControl pc;


    //public EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Instance.character.spr = Instance.GetComponent<SpriteRenderer>();
        tr = Instance.GetComponent<Transform>();
        Instance.character.pos = tr.position;
        Instance.stealth = stealth.StandMode;
        Instance.character.skinsList = Resources.LoadAll<Sprite>("Skins").ToList();
        pc = Instance.GetComponent<playerControl>();

    }

    // Update is called once per frame
    void Update()
    {
        Instance.tile = pc.tile;
        if (Input.GetKeyDown("c"))
        {
            if (Instance.stealth == stealth.StandMode)
            {
                Instance.stealth = stealth.stealthMode;

                Debug.Log("crouch");
            }
            else
            {
                Instance.stealth = stealth.StandMode;

            }

        }

        if (Input.GetKey("right"))
        {
            this.changeSkin("right");
            tr.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("up"))
        {
            this.changeSkin("up");
            tr.Translate(new Vector3(0f, 1f, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("down"))
        {
            this.changeSkin("down");

            tr.Translate(new Vector3(0f, -1f, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("left"))
        {
           
            this.changeSkin("left");
            tr.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * speed);
        }

    }
    public void changeSkin(string dir)
    {
        if (Instance.stealth == stealth.stealthMode)
        {

            switch (dir)
            {
                case "left":
                    Instance.character.spr.sprite = Instance.character.skinsList[6];
                    break;
                case "right":
                    Instance.character.spr.sprite = Instance.character.skinsList[5];
                    break;
                case "up":
                    Instance.character.spr.sprite = Instance.character.skinsList[7];
                    break;
                case "down":
                    Instance.character.spr.sprite = Instance.character.skinsList[4];
                    break;
            }
        }
        else if (Instance.stealth == stealth.StandMode)
        {


            switch (dir)
            {
                case "left":
                    Instance.character.spr.sprite = Instance.character.skinsList[2];
                    break;
                case "right":
                    Instance.character.spr.sprite = Instance.character.skinsList[1];
                    break;
                case "up":
                    Instance.character.spr.sprite = Instance.character.skinsList[3];
                    break;
                case "down":
                    Instance.character.spr.sprite = Instance.character.skinsList[0];
                    break;

            }
        }

    }
}
