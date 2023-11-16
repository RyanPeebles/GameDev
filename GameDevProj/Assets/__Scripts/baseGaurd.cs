using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//data Unique to gaurds
public class baseGaurd : baseUnit
{
    public ScriptableChar character;
    public bool moving = false;
    
    public List<GameObject> path;
    public GameObject target;
    public direction dir;
    public Transform tr;
    //public Vector3 targetPos;
    public int cnt;
    public int q = 1;

    void Start()
    {
        this.foot = gameObject.transform.GetChild(1).gameObject;
    }
    void Update(){
       // Debug.Log(moving);
        
        if(Input.GetKeyDown("up")){
            this.dir = direction.north;
        }
        else if(Input.GetKeyDown("left")){
            this.dir = direction.east;
        }
        if(this.moving == true && this.path !=null){
            
            //Debug.Log("target" +target.name);
           var t = TileManager.tileList[target.name].obj;
            Vector3 targetPos = new Vector3(t.transform.position.x,t.transform.position.y +1,-1);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, .005f);
            //Debug.Log("this tile = " + this.tile.name);
            //Debug.Log("this target = " + this.target.name);
            if(this.tile == this.target){
                 this.moving = false;
                 Debug.Log("Done");
                 //this.q = 1;
                if(this.cnt <= 0){
                    this.path = null;
                    this.q =1;
                    this.moving = false;
                }
            }

    }
    }
    public void move(){
        this.cnt = this.path.Count;
        foreach(var tile in this.path){
         StartCoroutine(this.walkDaLine(tile));
        }
         

    }
    
    IEnumerator walkDaLine(GameObject t){
        //Debug.Log(this.path.Count);
        
            //Debug.Log(tile.name);
           
            this.moving = true;
            this.target = t;
            
            //Debug.Log(cnt);
            this.cnt--;
      yield return new WaitUntil(()=> (this.moving == false));
      //this.q = 1;
    
    //this.path = null;
    //this.q =1;
    
    }
    /*void OnTriggerEnter2D(Collider2D c){
        //Debug.Log("hellppp");
        if(c.gameObject.tag == "Floor"){
            //Debug.Log("this is the one");
            var temp = TileManager.tileList[c.gameObject.name];
            this.tile = temp.obj;
        }
    }
    */
}
