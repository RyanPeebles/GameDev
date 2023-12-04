using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footColScript : MonoBehaviour
{
    [SerializeField]public GameObject daddy;
    public baseGaurd bgaurd;
    public basePlayer bPlayer;
    public Transform tr;
    public Transform tr2;
    public float speed = 1f;
    [SerializeField]public footColScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Instance.daddy = Instance.transform.parent.gameObject;
        if(Instance.daddy.tag == "gaurd"){
            Instance.bgaurd = daddy.GetComponent<baseGaurd>();
        }
        if(Instance.daddy.tag == "Player"){
         Instance.bPlayer = daddy.GetComponent<playerControl>();
         Instance.tr = daddy.GetComponent<Transform>();
         Instance.tr2 = Instance.GetComponent<Transform>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Instance.bPlayer != null){
           
         if (Input.GetKey("right"))
        {
            
            Instance.tr.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("up"))
        {
            
            Instance.tr.Translate(new Vector3(0f, 1f, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("down"))
        {
            

            Instance.tr.Translate(new Vector3(0f, -1f, 0) * Time.deltaTime * speed);
        }
        if (Input.GetKey("left"))
        {
           
            
            Instance.tr.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * speed);
        }
        }
         
    }
        
    
    void OnCollisionEnter2D(Collision2D c){
        if(c.gameObject.tag == "decor"){
            
            speed = -1;
        }
    }
    void OnCollisionStay2D(Collision2D c){
        if(c.gameObject.tag == "decor"){
            speed = .25f;
        }
    }
    void OnCollisionExit2D(Collision2D c){
        if(c.gameObject.tag =="decor"){
            speed = 1f;
        }
    }
 
    void OnTriggerEnter2D(Collider2D c){
        //Debug.Log(c.gameObject.name);
        if(c.gameObject.tag == "Floor" ){
            var temp = TileManager.tileList[c.gameObject.name];
            if(temp.walkable == true){
            if(Instance.bgaurd != null){
                
                if(Instance.bgaurd.tile!=temp.obj){
                    //Debug.Log("tielhit : " + temp.obj);
                    Instance.bgaurd.tile = temp.obj;
                }
            }
            if(Instance.bPlayer != null){
                if(Instance.bPlayer.tile!=temp.obj){
                    Instance.bPlayer.tile = temp.obj;
                }
            
            }
            }
        }
    }
}
