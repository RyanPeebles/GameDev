using TMPro;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public int goldValue = 0;
    public Rigidbody2D rigid;
    public BoxCollider2D coll;
    public BaseItem item = null;
    public TMP_Text text;
    private bool pickupable = false;
    private GameObject item2;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        text.text = "Gold: " + goldValue;
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

    }
}
