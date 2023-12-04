using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

//data Unique to gaurds
public class baseGaurd : baseUnit
{
    public ScriptableChar character;
    public bool moving = false;
    
    public List<GameObject> path = null;
    public GameObject target;
    public GameObject FinalTarget;
    public direction dir;
    public bool chasing = false;
    public bool playerSpotted = false;
    
    //public Vector3 targetPos;
    public int cnt;
    public int q = 1;


    void Start()
    {
        this.foot = gameObject.transform.GetChild(1).gameObject;
        this.path = null;
    }
    void FixedUpdate(){
       // Debug.Log(moving);
        
        
        if(this.moving == true){
            
            Debug.Log("going");
           var t = TileManager.tileList[this.target.name];
            Vector3 targetPos = TileManager.map.GetCellCenterWorld(t.pos);
            targetPos += new Vector3(0,.5f,-1);
            Debug.Log(" target : " + targetPos);
            this.transform.position = Vector3.MoveTowards(transform.position, targetPos, .015f);
            //Debug.Log("this tile = " + this.tile.name);
            //Debug.Log("this target = " + this.target.name);
            if(this.tile == this.target){
              
                 this.moving = false;
                  
                
                 
                 //this.q = 1;
                
            }

    }
    else{this.moving = false;}
    }
    public void move(){
       
        if(this.path != null && this.chasing == false){
            Debug.Log("starting walk");
            StartCoroutine(this.walkDaLine(this.path));
        }
       
    }
    
    IEnumerator walkDaLine(List<GameObject> t){
        this.chasing = true;
        this.cnt = t.Count;
        //Debug.Log(this.path.Count);
       
            //Debug.Log(tile.name);
           foreach(var tl in t){
            if (this.path == null){
                Debug.Log("Breaking");
                yield break;
            }
            
            this.moving = true;
            this.target = tl;
           Debug.Log("Next!" + tl);
            //Debug.Log(cnt);
            this.cnt--;
      yield return new WaitUntil(()=> (this.moving == false));
      //this.tile = this.target;
      //this.moving = false;
      
           }

      //this.q = 1;
   
    this.path = null;
    //this.q =1;
   this.chasing = false;
    Debug.Log("test out path finished");
     if(this.tile != this.FinalTarget){
        this.playerSpotted = false;
        this.moving = false;
        this.path = TileManager.FindPath(this.tile,this.FinalTarget);
        this.move();
    }
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
