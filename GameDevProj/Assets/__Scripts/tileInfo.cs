using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "_Prefabs", menuName = "ScriptableObjects/tileInfo", order =1)]
public class tileInfo : ScriptableObject{

    public void Init(Vector3Int vc, String N, GameObject obg,TileBase t,bool walk){
        this.pos = vc;
        this.Name = N;
        this.tile = t;
        this.obj = obg;
        this.tm = GameObject.Find("TileManager");
        this.TileManager = tm.GetComponent<TileManager>();
        this.Instance = this;
        this.walkable = walk;
    }
    public GameObject obj;
    public BoxCollider2D col;
    public Rigidbody2D rb;
    public String Name;
    public Vector3Int pos;
    public TileBase tile;
    public tileInfo Instance;
    public bool walkable = true;
   public float G {get; private set;}
    public float H {get; private set;}
    public float F => G + H;
    public GameObject Connection {get; private set;}
    public List<GameObject> neighbors {get; protected set;}
    public TileManager TileManager;
    public GameObject tm;
    public Collider2D[] cols = new Collider2D[100];
    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };
    public void SetColl(){
        Instance.col= Instance.obj.GetComponent<BoxCollider2D>();
        String s = "Floor";
        Instance.col.tag = s;
        Instance.col.isTrigger=(true);
        Instance.col.includeLayers = 4;
        if(Instance.walkable == false){
            try{
            Instance.rb = Instance.obj.GetComponent<Rigidbody2D>();
            Instance.rb.gravityScale = 0;
            Instance.col.isTrigger = false;
            }
            catch(Exception e){
                return;
            }
        }
   

      
        //col.offset = new Vector2(.5f,.5f);
    }
   
    public void SetLayer(){
        Instance.obj.layer = 2;
    }

    public void SetConnection(GameObject tile){
        Instance.Connection = tile;
    }
    public void SetH(float h){
        Instance.H = h;
    }
    public void SetG(float g){
        Instance.G=g;
    }

    public void cacheNeighbors(){
        neighbors = new List<GameObject>();
        //Debug.Log(pos.x + "pos");
        //Debug.Log(pos.y + "pos");
       
        
        foreach (var tile in Dirs)
          {
               var tempx = pos.x + tile.x;
               var tempy = pos.y + tile.y;

                var tn = $"{tempx},{tempy}";
                //Debug.Log(tn);
                //Debug.Log(TileManager.tileList);
                if(TileManager.tileList.ContainsKey(tn)){
                    var temp = TileManager.tileList[tn];
                    this.neighbors.Add(temp.obj);
          }
                //Debug.Log("neighbor" + temp);
               //this.neighbors.Add(TileManager.tileList[tn].obj);
               
            }
           

    }
    public float GetDistance(GameObject tile){
        //var index = from t in TileManager.tileList where t.Item1;
        var info = TileManager.tileList[tile.name];
        var dist = new Vector2Int(Mathf.Abs((int)pos.x - (int)info.pos.x), Mathf.Abs((int)pos.y - (int)info.pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
    
}

