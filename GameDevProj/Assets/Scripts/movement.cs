using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform tr;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = tr.position;
    }

    // Update is called once per frame
    void Update()
    {   
    
       if(Input.GetKey("right")){
        
        tr.Translate( new Vector3(1f,0,0) *Time.deltaTime);
       }
       if(Input.GetKey("up")){
        
        tr.Translate( new Vector3(0f,1f,0) *Time.deltaTime);
       }
       if(Input.GetKey("down")){
        
        tr.Translate( new Vector3(0f,-1f,0) *Time.deltaTime);
       }
       if(Input.GetKey("left")){
        
        tr.Translate( new Vector3(-1f,0,0) *Time.deltaTime);
       }
    }
}
