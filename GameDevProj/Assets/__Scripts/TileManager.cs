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
    public GameObject tmap;

    [SerializeField]public static Dictionary<String,tileInfo> tileList;
    [SerializeField]public TileManager Instance;
    //public tileInfo ti;
 
    


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        map = tmap.GetComponent<Tilemap>();
       // map = this.GetComponent<Tilemap>();
        tileList = Instance.getTiles();
    }


    public Dictionary<String,tileInfo> getTiles(){

        Dictionary<String,tileInfo> spots = new Dictionary<String,tileInfo>();
        Debug.Log(map.cellBounds.xMin);
        Debug.Log(map.cellBounds.yMin);
 
        for (int n = map.cellBounds.xMin; n < map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p < map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)map.transform.position.z));
                Vector3 place = TileManager.map.GetCellCenterWorld(localPlace);
                Debug.Log("LP: "+ localPlace);
                if (map.HasTile(localPlace))
                {
                    String name = $"{n},{p}";

                    //Debug.Log(name);
                    TileBase t = TileManager.map.GetTile(localPlace);
                    //tileInfo ti = new tileInfo();
                    GameObject go = new GameObject();
                    go.AddComponent<BoxCollider2D>();
                    go.transform.position = place;
                    Debug.Log(t);
                    var ti = tileInfo.CreateInstance<tileInfo>();
                    t.name = name;
                    go.name = name;
                    ti.Init(localPlace,t.name,go);
                    
                   
                    
                    //Tile at "place"
                    spots.Add(t.name,ti);
                
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        foreach(KeyValuePair<String,tileInfo> t in spots){
        t.Value.SetColl();
        t.Value.cacheNeighbors();
        t.Value.SetLayer();
        Debug.Log("Look Here Nerd: " + map.GetTile(t.Value.pos));
        }
        return spots;
    }
    // Update is called once per frame
    






        public static List<TileBase> FindPath(TileBase start, TileBase target){
        var toSearch = new List<TileBase>() {start};
        var processed = new List<TileBase>();
        Debug.Log("look" + target.name);
        var targetInfo = tileList[target.name];
        while(toSearch.Any()){
            var current = toSearch[0];
            Debug.Log("Here" + current.name);
            var currentInfo = tileList[current.name];
            foreach(var t in toSearch){
                 var info = tileList[t.name];
                
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
                        var currentPathInfo = tileList[currentPath.name];
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
                    var neighborInfo = tileList[neighbor.name];
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





}
