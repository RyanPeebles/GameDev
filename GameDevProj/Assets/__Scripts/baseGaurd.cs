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
    public float gspeed = .015f;
    public bool chasing = false;
    public bool playerSpotted = false;
    
   
    public int cnt;
    public int q = 1;


    void Start()
    {
        this.foot = gameObject.transform.GetChild(1).gameObject;
        this.path = null;
    }
    void FixedUpdate(){
     
        
        
        if(this.moving == true){
            
           
           var t = TileManager.tileList[this.target.name];
            Vector3 targetPos = TileManager.map.GetCellCenterWorld(t.pos);
            targetPos += new Vector3(0,.5f,-1);
            float angle  = Vector3.SignedAngle(new Vector3(1,0,0),targetPos - this.transform.position,Vector3.up);
           Debug.Log(angle);
           if(angle >= 0f){
                if(angle <= 45f){
                    this.dir = direction.east;
                }
                if(45f < angle && angle <= 135f){
                    this.dir = direction.north;
                }
                    
                if(135f < angle && angle <= 180f){
                    this.dir = direction.west;
                }
           }else{
            if(angle > -45f){
                this.dir = direction.east;
            }
            if(angle > -135f && angle <= -45f){
                    this.dir = direction.south;
             }
             if(angle >= -180 && angle <= -135){
                this.dir = direction.west;
             }
           }
            
            StartCoroutine(step(targetPos));
           
            

    }
    else{this.moving = false;}
    }
    IEnumerator step(Vector3 tp){
        this.transform.position = Vector3.MoveTowards(this.transform.position, tp, this.gspeed);
        
        yield return new WaitUntil(()=>(this.transform.position == tp ));
        this.moving = false;
    }
    public void move(){
       
        if(this.path != null && this.chasing == false){
           
            StartCoroutine(this.walkDaLine(this.path));
        }
       
    }
    
    IEnumerator walkDaLine(List<GameObject> t){
        this.chasing = true;
        this.cnt = t.Count;
       
           foreach(var tl in t){
            if (this.path == null){
                Debug.Log("Breaking");
                yield break;
            }
            
            this.moving = true;
            this.target = tl;
         
         
      yield return new WaitUntil(()=> (this.moving == false));
      this.cnt--;
     
      
           }

    
   
    this.path = null;
   
   
   
     if(this.tile != this.FinalTarget){
        this.playerSpotted = false;
        this.moving = false;
        this.path = TileManager.FindPath(this.tile,this.FinalTarget);
         StartCoroutine(this.walkDaLine(this.path));
    }
    else{this.chasing = false;}
    }
    
}
