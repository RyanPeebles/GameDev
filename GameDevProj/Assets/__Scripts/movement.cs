using System.Linq;
using UnityEngine;
public class movement : basePlayer
{
    public GameManager gm;
    public Transform tr;
    public int speed = 1;


    [SerializeField] public basePlayer Instance;
    public playerControl pc;
    public Animator anim;

    //public EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        Instance = this;
        Instance.character.spr = Instance.GetComponent<SpriteRenderer>();
        tr = Instance.GetComponent<Transform>();
        Instance.character.pos = tr.position;
        Instance.stealth = stealth.StandMode;
        Instance.character.skinsList = Resources.LoadAll<Sprite>("Skins").ToList();
        pc = Instance.GetComponent<playerControl>();
        Instance.tile = pc.tile;

    }

    // Update is called once per frame
    void Update()
    {
        Instance.tile = pc.tile;
        //UpdateAnimation();



        if (Input.GetKeyDown("c"))
        {
            if (Instance.stealth == stealth.StandMode)
            {
                Instance.stealth = stealth.stealthMode;
                anim.SetBool("crouch", true);

                Debug.Log("crouch");
            }
            else
            {
                Instance.stealth = stealth.StandMode;
                anim.SetBool("crouch", false);
            }

        }




        if (Input.GetKey("right"))
        {
            this.changeSkin("right");

        }
        if (Input.GetKey("up"))
        {
            this.changeSkin("up");

        }
        if (Input.GetKey("down"))
        {
            this.changeSkin("down");


        }
        if (Input.GetKey("left"))
        {

            this.changeSkin("left");


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

    public void UpdateAnimation()
    {
        if (Input.GetKey("right"))
        {
            anim.SetBool("right", true);
        }
        else if (Input.GetKey("up"))
        {
            anim.SetBool("backward", true);
        }
        else if (Input.GetKey("down"))
        {
            anim.SetBool("foward", true);
        }
        else if (Input.GetKey("left"))
        {
            anim.SetBool("left", true);
        }
    }
}

