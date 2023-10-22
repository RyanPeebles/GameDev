using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class TileManager : MonoBehaviour
{
    public TileBase tile;
    public static Tilemap map;

    public static Dictionary<TileBase,tileInfo> tileList;

 
    


    // Start is called before the first frame update
    void Start()
    {
        map = this.GetComponent<Tilemap>();
        tileList = getTiles();
    }


    public Dictionary<TileBase,tileInfo> getTiles(){

        Dictionary<TileBase,tileInfo> spots = new Dictionary<TileBase,tileInfo>();
 
        for (int n = map.cellBounds.xMin; n < map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p < map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)map.transform.position.y));
                Vector3 place = map.CellToWorld(localPlace);
                if (map.HasTile(localPlace))
                {
                    var info = new tileInfo(place);
                    info.cacheNeighbors();
                    TileBase t = map.GetTile(localPlace);
                    //Tile at "place"
                    spots.Add(t,info);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        return spots;
    }
    // Update is called once per frame
    






        public static List<TileBase> FindPath(TileBase start, TileBase target){
        var toSearch = new List<TileBase>() {start};
        var processed = new List<TileBase>();
        var targetInfo = tileList[target];
        while(toSearch.Any()){
            var current = toSearch[0];
            var currentInfo = tileList[current];
            foreach(var t in toSearch){
                 var info = tileList[t];
                
                if(info.F < currentInfo.F || info.F == currentInfo.F && info.H < currentInfo.H) {current = t;}
            }

                processed.Add(current);
                toSearch.Remove(current);

                if( current == target){
                    var currentPath = target;
                    var path = new List<TileBase>();
                    var count = 100;
                    while(currentPath != start){
                        path.Add(currentPath);
                        var currentPathInfo = tileList[currentPath];
                        //currentPath.inPath = true;
                        currentPath = currentPathInfo.Connection;
                        count--;
                        if(count < 0) {throw new Exception();}
                        //Debug.Log("sdfsdf");
                    
                    }
                    //path.Reverse();
        
                    return path;
                    }
                    
                
                foreach (var neighbor in currentInfo.neighbors.Where(t => !processed.Contains(t))) {
                    var inSearch = toSearch.Contains(neighbor);
                    var neighborInfo = tileList[neighbor];
                    var costToNeighbor = currentInfo.G + currentInfo.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighborInfo.G) {
                        neighborInfo.SetG(costToNeighbor);
                        neighborInfo.SetConnection(current);

                        if (!inSearch) {
                            neighborInfo.SetH(neighborInfo.GetDistance(target));
                            toSearch.Add(neighbor);
                           
                        }
                    }
                    }
                }
                return null;
            }




public class tileInfo{

    public tileInfo(Vector3 vc){
        pos = vc;
    }
    public Vector3 pos;
   public float G {get; private set;}
    public float H {get; private set;}
    public float F => G + H;
    public TileBase Connection {get; private set;}
    public List<TileBase> neighbors {get; protected set;}

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
        var info = tileList[tile];
        var dist = new Vector2Int(Mathf.Abs((int)pos.x - (int)info.pos.x), Mathf.Abs((int)pos.y - (int)info.pos.y));

        var lowest = Mathf.Min(dist.x, dist.y);
        var highest = Mathf.Max(dist.x, dist.y);

        var horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10 ;
    }
    
}
}