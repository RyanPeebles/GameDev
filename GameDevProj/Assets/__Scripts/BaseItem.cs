using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public int goldValue;
    protected Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
