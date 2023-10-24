using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class vision : MonoBehaviour
{

    public TileManager tm;
    public GameObject gaurd;
    public vision Instance;
    public CircleCollider2D cldr;
    public List<GameObject> inView;
    private List<TileBase> path;
    void Start()
    {
        Instance = this;
        cldr = Instance.GetComponent<CircleCollider2D>();
        gaurd = Instance.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    IEnumerator castRays(GameObject g){

        
        
        Vector2 direction = g.transform.position - Instance.gaurd.transform.position;
    
        float distance = Vector2.Distance(Instance.gaurd.transform.position, g.transform.position);
        direction = direction/distance;
            //RaycastHit2D checkHit = Physics2D.Raycast(Instance.gaurd.transform.position,direction,distance);
             if(g.tag == "Player"){
                
                Debug.Log("hit player");
                Vector3Int playerPos = new Vector3Int((int)g.transform.position.x,(int)g.transform.position.y,0);
                this.follow(playerPos);
                
                //RaycastHit2D hit = Physics2D.Raycast(g.transform.position, direction, Mathf.Infinity); 
        } 
        yield return new WaitUntil(() => !inView.Contains(g));

    }
    public void follow(Vector3Int targ){
        Vector3Int start = new Vector3Int((int)Instance.gaurd.transform.position.x,(int)Instance.gaurd.transform.position.y,0);
        var startTile = TileManager.map.GetTile(start);
        var endTile = TileManager.map.GetTile(targ);
        Instance.path = TileManager.FindPath(startTile, endTile);
       
            foreach(var tile in path){
               // this.gaurd.transform.position = Vector2.MoveTowards(this.gaurd.transform.position, tile.transform.pos,8f*Time.deltaTime);
            }
        



    }
     void OnTriggerEnter2D(Collider2D c)
    {
        GameObject go = new GameObject();
        go = c.gameObject;
        Debug.Log("hit");
        inView.Add(go);
        StartCoroutine(castRays(go));
       // inView.push(c.gameObject);
        /*
        Vector2 direction = c.gameObject.character.pos - Instance.character.pos;
    
        float distance = Vector2.Distance(Instance.character.pos, c.gameObject.character.pos);
        direction = direction/distance;
       // float angle = (Vector2.Angle(Instance.character.pos, c.gameObject.character.pos)) * Mathf.Deg2Rad;
        
       
        if(c.gameObject.tag == "Player"){
            RaycastHit2D hit = Physics2D.Raycast(this.character.pos, direction, distance);
            if()
        }
        */
    }
    public void OnTriggerExit2D(Collider2D c){
        if(inView.Contains(c.gameObject)){
            inView.Remove(c.gameObject);
            Debug.Log("removed");
        }
    }
}
