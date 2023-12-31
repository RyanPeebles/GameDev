using UnityEngine;

public class footColScript : MonoBehaviour
{
    [SerializeField] public GameObject daddy;
    public baseGaurd bgaurd;
    public basePlayer bPlayer;
    public Transform tr;
    public Transform tr2;
    public bool colliding = false;
    public float speed = 1f;
    public int baseSpeed = 2;
    [SerializeField] public footColScript Instance;
    public Animator Player_anim;
    // Start is called before the first frame update
    // private Animator anim;

    void Start()
    {
        //anim = GetComponent<Animator>();
        Player_anim = this.transform.parent.gameObject.GetComponent<Animator>();
        Instance = this;
        Instance.daddy = Instance.transform.parent.gameObject;
        if (Instance.daddy.tag == "gaurd")
        {
            Instance.bgaurd = daddy.GetComponent<baseGaurd>();
        }
        if (Instance.daddy.tag == "Player")
        {
            Instance.bPlayer = daddy.GetComponent<playerControl>();
            Instance.tr = daddy.GetComponent<Transform>();
            Instance.tr2 = Instance.GetComponent<Transform>();
            speed = 1 * baseSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Instance.bPlayer != null && this.colliding == false)
        {
            if (Instance.tr2.localPosition != Vector3.zero)
            {
                Instance.tr.position = Instance.tr2.TransformPoint(Instance.tr2.localPosition);
            }
            Instance.tr2.localPosition = Vector3.zero;

            if (Input.GetKey("right") && !Input.GetKey("left"))
            {

                Instance.tr.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * speed);
                Debug.Log("set left");
                Player_anim.SetBool("right", true);
            }
            else
            {
                Player_anim.SetBool("right", false);
            }
            if (Input.GetKey("up"))
            {

                Instance.tr.Translate(new Vector3(0f, 1f, 0) * Time.deltaTime * speed);
                Player_anim.SetBool("backward", true);
            }
            else
            {
                Player_anim.SetBool("backward", false);
            }
            if (Input.GetKey("down"))
            {


                Instance.tr.Translate(new Vector3(0f, -1f, 0) * Time.deltaTime * speed);
                Player_anim.SetBool("foward", true);
            }
            else
            {
                Player_anim.SetBool("foward", false);
            }
            if (Input.GetKey("left") && !Input.GetKey("right"))
            {


                Instance.tr.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * speed);
                Player_anim.SetBool("left", true);
            }
            else
            {
                Player_anim.SetBool("left", false);
            }


        }


    }


    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "decor" || c.gameObject.tag == "walls")
        {
            if (!this.colliding)
            {
                this.colliding = true;
                speed = 0;
            }


        }
    }
    void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.tag == "decor" || c.gameObject.tag == "walls")
        {
            if (this.colliding)
            {

                speed = -1 * baseSpeed;
                this.colliding = false;
            }

            else
            {
                speed = 1f * baseSpeed;
            }
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "decor" || c.gameObject.tag == "walls")
        {
            if (!this.colliding)
            {

                speed = 1f * baseSpeed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(c.gameObject.name);
        if (c.gameObject.tag == "Floor")
        {
            var temp = TileManager.tileList[c.gameObject.name];

            if (Instance.bgaurd != null)
            {

                if (Instance.bgaurd.tile != temp.obj)
                {
                    //Debug.Log("tielhit : " + temp.obj);
                    Instance.bgaurd.tile = temp.obj;
                }
            }
            if (Instance.bPlayer != null)
            {
                if (Instance.bPlayer.tile != temp.obj)
                {
                    Instance.bPlayer.tile = temp.obj;
                }

            }

        }
    }
}
