using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : basePlayer
{
    public int goldValue = 0;
    public Rigidbody2D rigid;
    public BoxCollider2D coll;
    public BaseItem item = null;
    public TMP_Text text;
    private bool pickupable = false;
    private GameObject item2;
    [SerializeField] public basePlayer Instance;
    public movement stealthed;
    public fov guardCheck;
    public bool isSeen;
    public GameObject[] stars;
    public int interval = 5;
    public Timer time;
    public float initialTime;
    public bool running = false;
    int starNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        text.text = "Gold: " + goldValue;
        Instance = this;
        isSeen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (guardCheck != null)
        {
            isSeen = guardCheck.playerSpotted;
        }
        else
        {
            isSeen = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && pickupable)
        {
            pickup();
        }
        if (stealthed.Instance.stealth == stealth.stealthMode && isSeen && !running)
        {
            StarLevel();
        }
        else if (stealthed.Instance.stealth == stealth.StandMode && isSeen)
        {

        }

    }

    public void increaseGold()
    {
        goldValue += item.goldValue;
        text.text = "Gold: " + goldValue;
    }

    public void pickup()
    {
        increaseGold();
        item2.SetActive(false);
        pickupable = false;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            pickupable = true;
            item = coll.gameObject.GetComponent<BaseItem>();
            item2 = coll.gameObject;
        }
        /*
        if(coll.gameObject.tag == "Floor"){
            
            var temp = TileManager.tileList[coll.gameObject.name];
           
           // Debug.Log(temp.tile);
            Instance.tile = temp.obj;
          
        }
*/
    }

    public void StarLevel()
    {
        StartCoroutine(Delay());
        running = true;
    }

    IEnumerator Delay()
    {
        Debug.Log("start blinking");
        StartCoroutine(Blink());
        yield return new WaitForSeconds(.01f);
        StopCoroutine(Blink());
        stars[starNum].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        starNum++;
        Debug.Log("make next blink");
        if (isSeen)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Blink()
    {
        stars[starNum].GetComponent<Image>().color = new Color32(255, 255, 225, 255);
        yield return new WaitForSeconds(0.2f);
        stars[starNum].GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);


    }
}
