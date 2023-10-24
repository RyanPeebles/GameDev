using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "_Prefabs", menuName = "ScriptableObjects/tileInfo", order =1)]
public class tileInfo : ScriptableObject{

    public void Init(Vector3 vc, String N, TileBase t){
        this.pos = vc;
        this.Name = N;
        this.tile = t;
    }
    public String Name;
    public Vector3 pos;
   public TileBase tile;
   public float G {get; private set;}
    public float H {get; private set;}
    public float F => G + H;
    public TileBase Connection {get; private set;}
    public List<TileBase> neighbors {get; protected set;}
    public TileManager TileManager;

    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

    public void SetConnection(TileBase tile){
        Connection = tile;
    }
    public void SetH(float h){
        H = h;
    }
    public void SetG(float g){
        G=g;
    }

    public void cacheNeighbors(){
        neighbors = new List<TileBase>();
        
        
        foreach (var tile in Dirs.Select(dir => TileManager.map.GetTile(new Vector3Int((int)(pos.x + dir.x),(int)(pos.y + dir.y),0))).Where(tile => tile != null)) {
                neighbors.Add(tile);
            }

    }
    public float GetDistance(TileBase tile){
        //var index = from t in TileManager.tileList where t.Item1;
        var info = TileManager.tileList[tile.name];
        var dist = new Vector2Int(Mathf.Abs((int)pos.x - (int)info.pos.x), Mathf.Abs((int)pos.y - (int)info.pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
    
}

