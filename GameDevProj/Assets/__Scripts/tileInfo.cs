using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "_Prefabs", menuName = "ScriptableObjects/tileInfo", order =1)]
public class tileInfo : ScriptableObject{

    public void Init(Vector3Int vc, String N, GameObject obg,TileBase t){
        this.pos = vc;
        this.Name = N;
        this.tile = t;
        this.obj = obg;
        this.tm = GameObject.Find("TileManager");
        this.TileManager = tm.GetComponent<TileManager>();
    }
    public GameObject obj;
    public BoxCollider2D col;
    public String Name;
    public Vector3Int pos;
    public TileBase tile;
   public float G {get; private set;}
    public float H {get; private set;}
    public float F => G + H;
    public GameObject Connection {get; private set;}
    public List<GameObject> neighbors {get; protected set;}
    public TileManager TileManager;
    public GameObject tm;
    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };
    public void SetColl(){
        col= obj.GetComponent<BoxCollider2D>();
        String s = "Floor";
        col.tag = s;
        col.isTrigger=(true);
        col.includeLayers = 4;
        //col.offset = new Vector2(.5f,.5f);
    }
    public void SetLayer(){
        obj.layer = 2;
    }

    public void SetConnection(GameObject tile){
        Connection = tile;
    }
    public void SetH(float h){
        H = h;
    }
    public void SetG(float g){
        G=g;
    }

    public void cacheNeighbors(){
        neighbors = new List<GameObject>();
        
        
        foreach (var tile in Dirs.Select(dir => TileManager.map.GetTile(new Vector3Int((int)(pos.x + dir.x),(int)(pos.y + dir.y),0))).Where(tile => tile != null)) {
               // neighbors.Add(TileManager.tileList[tile.name].obj);
               Debug.Log("neighbors: " + tile.name);
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

