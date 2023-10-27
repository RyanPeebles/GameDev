using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//data Unique to gaurds
public class baseGaurd : MonoBehaviour
{
    public ScriptableChar character;
    public bool moving = false;
    public TileManager TM;

    void Start()
    {
    }
    void Update(){
        //this.character.tile = 

    }
    void OnTriggerEnter2D(Collider2D c){
        if(c.tag == "Floor"){
            
            tileInfo temp = TileManager.tileList[c.name];
            this.character.tile = temp.tile;
        }
    }
    
}
