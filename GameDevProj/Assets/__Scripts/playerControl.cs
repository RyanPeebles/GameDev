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
    public int starNum = 0;
    public bool blinking = false;
    public bool isPicking = false;
    public bool blink2;
    public bool trouble = false;
    public int health = 3;
    public bool invincible = false;
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.5f;
    public bool startBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        text.text = "Gold: " + goldValue;
        Instance = this;
        isSeen = false;
        if (PlayerPrefs.GetInt("health") != 0) health = PlayerPrefs.GetInt("health");

        if (PlayerPrefs.GetInt("gold") != 0)
        {
            goldValue = PlayerPrefs.GetInt("gold");
            text.text = "Gold: " + goldValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSeen && running)
        {
            StartCoroutine(waitFive());
        }
        else if (isSeen && running)
        {
            StopCoroutine(waitFive());
        }
        if (Input.GetKeyDown(KeyCode.E) && pickupable)
        {
            pickup();
            isPicking = true;
        }
        if (stealthed.Instance.stealth == stealth.stealthMode && isSeen && !running)
        {
            running = true;
            StarLevel();

        }
        else if (stealthed.Instance.stealth == stealth.StandMode && isSeen && isPicking)
        {
            trouble = true;
            for (int i = 0; i <= 4; i++)
            {
                stars[i].GetComponent<Image>().color = Color.white;
                starNum++;
            }
            running = true;
        }
        isPicking = false;
        if (starNum > 4)
        {
            starNum = 4;
        }
        if (startBlinking)
        {
            SpriteBlinkingEffect();
        }

        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("gold", goldValue);
    }

    public void increaseGold()
    {
        goldValue += item.goldValue;
        text.text = "Gold: " + goldValue;
    }

    public void pickup()
    {
        isPicking = true;
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
        if (coll.CompareTag("gaurd") && !invincible)
        {
            health--;
            invincible = true;
            startBlinking = true;
            StartCoroutine(DamageDelay());
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {

        pickupable = false;
        item = null;
        item2 = null;

    }

    public void StarLevel()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {

        Debug.Log("start blinking");
        blinking = true;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(5f);
        StopCoroutine(Blink());
        blinking = false;
        stars[starNum].GetComponent<Image>().color = Color.white;
        starNum++;
        Debug.Log("make next blink");
        if (isSeen && starNum <= 4)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Blink()
    {

        while (blinking)
        {
            Blinking();
            yield return new WaitForSeconds(.3f);
        }

    }

    public void Blinking()
    {
        if (stars[starNum].GetComponent<Image>().color == Color.white)
        {
            stars[starNum].GetComponent<Image>().color = Color.black;
        }
        else
        {
            stars[starNum].GetComponent<Image>().color = Color.white;
        }
    }

    IEnumerator waitFive()
    {
        yield return new WaitForSeconds(5f);
        StopCoroutine(Delay());
        StopCoroutine(Blink());
        for (int i = 4; i >= 0; i--)
        {
            stars[i].GetComponent<Image>().color = Color.black;
            starNum = i;
        }
        running = false;
        yield break;
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(1.5f);

        invincible = false;
    }

    public void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;

            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 

            spriteBlinkingTotalTimer = 0.0f;//your sprite
            return;
        }
        Debug.Log(Time.deltaTime);
        spriteBlinkingTimer += Time.deltaTime;
        Debug.Log(spriteBlinkingTimer);
        Debug.Log(spriteBlinkingMiniDuration);
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
            spriteBlinkingTimer = 0.0f;
        }
    }
}
