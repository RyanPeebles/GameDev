using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision : MonoBehaviour
{


    public baseGaurd gaurd;
    public vision Instance;
    public Collider2D cldr;
    private List<GameObject> inView;
    void Start()
    {
        Instance = this;
        cldr = Instance.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    IEnumerator castRays(GameObject g){
        inView.Add(g);
        
        Vector2 direction = g.transform.position - Instance.gaurd.character.pos;
    
        float distance = Vector2.Distance(Instance.gaurd.character.pos, g.transform.position);
        direction = direction/distance;
            RaycastHit2D checkHit = Physics2D.Raycast(Instance.gaurd.character.pos,direction,distance);
             if(g.tag == "Player"){
                RaycastHit2D hit = Physics2D.Raycast(g.transform.position, direction, Mathf.Infinity); 
        } 
        yield return new WaitUntil(() => !inView.Contains(g));

    }
    public void OnCollisionEnter2D(Collision2D c)
    {
        StartCoroutine(castRays(c.gameObject));
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
    public void OnCollisionExit2D(Collision2D c){
        if(inView.Contains(c.gameObject)){
            inView.Remove(c.gameObject);
        }
    }
}
