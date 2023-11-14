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

    void Start()
    {
    }
    void Update(){
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
         StartCoroutine(walkDaLine());
    }
    IEnumerator walkDaLine(){
        foreach(var tile in path){
       moving = true;
       target = tile;
      yield return new WaitUntil(() => moving == false);
    }
    }
    void OnTriggerEnter2D(Collider2D c){
        if(c.tag == "Floor"){
            
            var temp = TileManager.tileList[c.gameObject.name];
            tile = temp.obj;
        }
    }
    
}
