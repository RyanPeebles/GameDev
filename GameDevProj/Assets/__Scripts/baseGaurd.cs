using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//data Unique to gaurds
public class baseGaurd : baseUnit
{
    public ScriptableChar character;
    public bool moving = false;
    public GameObject startTile;
    public List<GameObject> path = null;
    public GameObject target;
    public GameObject FinalTarget;
    public direction dir;
    public type TYPE;
    public float gspeed = .015f;
    public bool chasing = false;
    public bool playerSpotted = false;
    public int baseSpeed = 1;
    public playerControl player;


    public int cnt;
    public int q = 1;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<playerControl>();

        if (this.TYPE == type.gaurd)
        {
            this.foot = gameObject.transform.GetChild(1).gameObject;
        }
        if (this.TYPE == type.wizard)
        {
            this.foot = gameObject.transform.GetChild(0).gameObject;
        }
        this.path = null;

    }
    void FixedUpdate()
    {


        if (player != null && player.starNum <= 4 && player.stars[player.starNum].GetComponent<Image>().color == Color.white)
        {
            baseSpeed = player.starNum + 1;
        }
        else
        {
            baseSpeed = 1;
        }
        gspeed = .015f * baseSpeed;

        if (this.startTile == null)
        {
            this.startTile = this.tile;
        }

        if (this.moving == true & this.path != null)
        {


            var t = TileManager.tileList[this.target.name];
            Vector3 targetPos = TileManager.map.GetCellCenterWorld(t.pos);
            targetPos += new Vector3(0, .5f, -1);


            StartCoroutine(step(targetPos));



        }

    }
    IEnumerator step(Vector3 tp)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, tp, this.gspeed);

        yield return new WaitUntil(() => (this.transform.position == tp));
        this.moving = false;
    }
    public void move()
    {

        if (this.path != null)
        {

            StartCoroutine(this.walkDaLine(this.path));
        }

    }

    IEnumerator walkDaLine(List<GameObject> t)
    {
        if (this.FinalTarget != this.startTile && !this.playerSpotted)
        {
            this.chasing = true;
        }
        else
        {
            this.chasing = false;
        }
        this.cnt = t.Count;

        foreach (var tl in t)
        {
            if (this.path == null)
            {
                Debug.Log("Breaking");
                yield break;
            }

            this.moving = true;
            this.target = tl;


            yield return new WaitUntil(() => (this.moving == false));
            this.cnt--;


        }



        this.path = null;



        if (this.tile != this.FinalTarget && this.playerSpotted == false)
        {

            if (this.FinalTarget != null)
            {
                this.path = TileManager.FindPath(this.tile, this.FinalTarget);
            }
            if (this.path != null)
            {
                StartCoroutine(this.walkDaLine(this.path));
            }
        }
        else { this.chasing = false; this.FinalTarget = this.startTile; }
    }

}
