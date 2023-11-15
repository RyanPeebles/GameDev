using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//data Unique to gaurds
public class baseGaurd : baseUnit
{
    public ScriptableChar character;
    public bool moving = false;
    public TileManager TM;
    public List<GameObject> path;
    public GameObject target;
    public direction dir;
    public Transform tr;

    void Start()
    {
        
    }
    void Update(){
         if (Input.GetKey("right"))
        {
            tr.Translate(new Vector3(1f, 0, 0) * Time.deltaTime);
        }
        if(Input.GetKeyDown("up")){
            this.dir = direction.north;
        }
        else if(Input.GetKeyDown("left")){
            this.dir = direction.east;
        }
        if(moving == true){
           var t = TileManager.tileList[target.name];
            Vector3 targetPos = new Vector3(t.pos.x,t.pos.y,-1);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, .001f);
            if(transform.position==targetPos){
                 moving = false;
            
            }

    }
    }
    public void move(){
         StartCoroutine(this.walkDaLine());
    }
    IEnumerator walkDaLine(){
        Debug.Log(this.path);
        foreach(var tile in this.path){
            
       this.moving = true;
       this.target = tile;
      yield return new WaitUntil(() => moving == false);
    }
    }
    void OnTriggerEnter2D(Collider2D c){
        Debug.Log("hellppp");
        if(c.gameObject.tag == "Floor"){
            Debug.Log("this is the one");
            var temp = TileManager.tileList[c.gameObject.name];
            this.tile = temp.obj;
        }
    }
    
}
