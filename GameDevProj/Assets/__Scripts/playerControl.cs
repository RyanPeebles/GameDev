using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerControl : basePlayer
{
    public int goldValue = 0;
    public Rigidbody2D rigid;
    public BoxCollider2D coll;
    public BaseItem item = null;
    public TMP_Text text;
    private bool pickupable = false;
    private GameObject item2;
    [SerializeField]public basePlayer Instance;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        text.text = "Gold: " + goldValue;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupable)
        {
            pickup();
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
        if(coll.gameObject.tag == "Floor"){
            
            var temp = TileManager.tileList[coll.gameObject.name];
           
           // Debug.Log(temp.tile);
            Instance.tile = temp.obj;
          
        }

    }
}
