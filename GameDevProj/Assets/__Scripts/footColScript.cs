using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footColScript : MonoBehaviour
{
    [SerializeField]public GameObject daddy;
    public baseGaurd bgaurd;
    public basePlayer bPlayer;
    [SerializeField]public footColScript Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Instance.daddy = Instance.transform.parent.gameObject;
        if(Instance.daddy.tag == "gaurd"){
            Instance.bgaurd = daddy.GetComponent<baseGaurd>();
        }
        if(Instance.daddy.tag == "Player"){
         Instance.bPlayer = daddy.GetComponent<playerControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){
        //Debug.Log(c.gameObject.name);
        if(c.gameObject.tag == "Floor"){
            var temp = TileManager.tileList[c.gameObject.name];
            if(Instance.bgaurd != null){
                
                Instance.bgaurd.tile = temp.obj;
                
            }
            if(Instance.bPlayer != null){
                Instance.bPlayer.tile = temp.obj;
            }
        }
    }
}
