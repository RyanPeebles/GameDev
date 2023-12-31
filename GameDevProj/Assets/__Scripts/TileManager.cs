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
    
    public static Tilemap mapWalls;
    public static Tilemap Decormap;
    
    public GameObject tmap;
    public GameObject tmap1;
   
    public GameObject tmapWalls;
    public bool walk = true;
    [SerializeField]public static Dictionary<String,tileInfo> tileList;
    [SerializeField]public TileManager Instance;
    //public tileInfo ti;
 
    


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        map = tmap.GetComponent<Tilemap>();
        Decormap = tmap1.GetComponent<Tilemap>();
       
        mapWalls = tmapWalls.GetComponent<Tilemap>();
       // map = this.GetComponent<Tilemap>();
        tileList = Instance.getTiles();
          foreach(KeyValuePair<String,tileInfo> t in tileList){
        t.Value.SetColl();
        t.Value.cacheNeighbors();
        t.Value.SetLayer();
        
        }
        Debug.Log("Map logged");
    }


    public Dictionary<String,tileInfo> getTiles(){

        Dictionary<String,tileInfo> spots = new Dictionary<String,tileInfo>();
       
 
        for (int n = map.cellBounds.xMin; n <= map.cellBounds.xMax; n++)
        {
            for (int p = map.cellBounds.yMin; p <= map.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)map.transform.position.z));
                Vector3 place = TileManager.map.GetCellCenterWorld(localPlace);
                
                if (map.HasTile(localPlace))
                {
                    
                    String name = $"{n},{p}";

                    TileBase t = TileManager.map.GetTile(localPlace);
                    
                    //tileInfo ti = new tileInfo();
                    GameObject go = new GameObject();
                    go.AddComponent<BoxCollider2D>();
                    go.transform.position = place;
                    
                    tileInfo ti = tileInfo.CreateInstance<tileInfo>();
                    //t.name = name;
                    go.name = name;
                    if(Decormap.HasTile(localPlace) || mapWalls.HasTile(localPlace)){
                        walk = false;
                        if(mapWalls.HasTile(localPlace)){
                            go.AddComponent<Rigidbody2D>();
                        }
                      
                    }else if(!Decormap.HasTile(localPlace) && !mapWalls.HasTile(localPlace)){
                        walk = true;
                        
                    }
                   
                    ti.Init(localPlace,t.name,go,t,walk);
                    
                   
                    
                    //Tile at "place"
                    spots.Add(name,ti);
                
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
    






        public static List<GameObject> FindPath(GameObject start, GameObject target){
            //Debug.Log("finding path");
          
        var toSearch = new List<GameObject>() {start};
        var processed = new List<GameObject>();
 
        var targetInfo = TileManager.tileList[target.name];
       
    
        while(toSearch.Any()){
            var current = toSearch[0];
         
         
            var currentInfo = TileManager.tileList[current.name];
            
            foreach(var t in toSearch){
               
                 var info = TileManager.tileList[t.name];
                
                if(info.F < currentInfo.F || info.F == currentInfo.F && info.H < currentInfo.H) {current = t;
                    currentInfo = TileManager.tileList[current.name];
                
                }
            }

                processed.Add(current);
                toSearch.Remove(current);

                if( current == target){
                   
                    var currentPath = target;
                    var path = new List<GameObject>();
                    var count = 100;
                    while(currentPath != start){
                        path.Add(currentPath);
                        var currentPathInfo = TileManager.tileList[currentPath.name];
                        //currentPath.inPath = true;
                        currentPath = currentPathInfo.Connection;
                        count--;
                        if(count < 0) {throw new Exception();}
                        
                    
                    }
                    path.Reverse();
                   // Debug.Log("Path Found");
                    return path;
                    }
                    
               
                foreach (var neighbor in currentInfo.neighbors.Where(t => !processed.Contains(t) && TileManager.tileList[t.name].walkable)) {
                    
                    var inSearch = toSearch.Contains(neighbor);
                    var neighborInfo = TileManager.tileList[neighbor.name];
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
               //Debug.Log("fail");
                return null;
            }

    
    



}
