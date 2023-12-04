using System;
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
    public GameObject FinalTarget;
    public direction dir;
    
    //public Vector3 targetPos;
    public int cnt;
    public int q = 1;


    void Start()
    {
        this.foot = gameObject.transform.GetChild(1).gameObject;
    }
    void FixedUpdate(){
       // Debug.Log(moving);
        
        
        if(this.moving == true && this.path !=null){
            
            //Debug.Log("target" +target.name);
           var t = TileManager.tileList[this.target.name];
            Vector3 targetPos = TileManager.map.GetCellCenterWorld(t.pos);
            targetPos += new Vector3(0,.5f,-1);
            //Debug.Log(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, .015f);
            //Debug.Log("this tile = " + this.tile.name);
            //Debug.Log("this target = " + this.target.name);
            if(this.tile == this.target){
              
                 this.moving = false;
                  if(this.tile != this.FinalTarget && cnt == 0){
       
                        this.path = null;
                    }
                
                 
                 //this.q = 1;
                
            }

    }
    }
    public void move(){
       
        
            StartCoroutine(this.walkDaLine(this.path));
        
       
    }
    
    IEnumerator walkDaLine(List<GameObject> t){
        this.cnt = t.Count;
        //Debug.Log(this.path.Count);
       
            //Debug.Log(tile.name);
           foreach(var tile in t){
            
            this.moving = true;
            this.target = tile;
           
            //Debug.Log(cnt);
            this.cnt--;
      yield return new WaitUntil(()=> (this.moving == false));
      //this.tile = this.target;
      //this.moving = false;
      
           }

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
