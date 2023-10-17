using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class movement : basePlayer
{
    public GameManager gm;
    public Transform tr;
  
    [SerializeField] public basePlayer Instance;
    
    
    //public EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Instance.character.spr =Instance.GetComponent<SpriteRenderer>();
        tr = Instance.GetComponent<Transform>();
        Instance.character.pos = tr.position;
        Instance.stealth = stealth.StandMode;
        Instance.character.skinsList = Resources.LoadAll<Sprite>("Skins").ToList();

    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown("c")){
            if(Instance.stealth == stealth.StandMode){
                Instance.stealth = stealth.stealthMode;
                 
                Debug.Log("crouch");
            }
            else{
                Instance.stealth = stealth.StandMode;
             
            }
            
        }
    
       if(Input.GetKey("right")){
        this.changeSkin("right");
        tr.Translate( new Vector3(1f,0,0) *Time.deltaTime);
       }
       if(Input.GetKey("up")){
        this.changeSkin("up");
        tr.Translate( new Vector3(0f,1f,0) *Time.deltaTime);
       }
       if(Input.GetKey("down")){
        this.changeSkin("down");
        
        tr.Translate( new Vector3(0f,-1f,0) *Time.deltaTime);
       }
       if(Input.GetKey("left")){
        Debug.Log(Instance.character.skinsList.Count);
        this.changeSkin("left");
        tr.Translate( new Vector3(-1f,0,0) *Time.deltaTime);
       }

    }
    public void changeSkin(string dir){
        if(Instance.stealth == stealth.stealthMode){
            
           switch(dir){
            case "left":
                Instance.character.spr.sprite = Instance.character.skinsList[6];
                break;
            case "right":
                Instance.character.spr.sprite = Instance.character.skinsList[5];
                break;
            case "up":
                Instance.character.spr.sprite = Instance.character.skinsList[7];
                break;
            case "down":
                Instance.character.spr.sprite = Instance.character.skinsList[4];
                break;
            }
        }
        else if(Instance.stealth == stealth.StandMode){

        
        switch(dir){
            case "left":
                Instance.character.spr.sprite = Instance.character.skinsList[2];
                break;
            case "right":
                Instance.character.spr.sprite = Instance.character.skinsList[1];
                break;
            case "up":
                Instance.character.spr.sprite = Instance.character.skinsList[3];
                break;
            case "down":
                Instance.character.spr.sprite = Instance.character.skinsList[0];
                break;
      
        }
        }
    
    }
}
