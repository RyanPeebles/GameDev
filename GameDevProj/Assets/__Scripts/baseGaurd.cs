using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//data Unique to gaurds
public class baseGaurd : MonoBehaviour
{
    public ScriptableChar character;

    public baseGaurd Instance;
    public List<ScriptableChar> inSight;

    public GameObject visionArea;
    public Collider2D visionCollider;


    void Start()
    {
        Instance = this;
    }
    void update()
    {
        
    }
   
    
}
