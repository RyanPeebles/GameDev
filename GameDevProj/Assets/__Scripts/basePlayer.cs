using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//data unique to players
public class basePlayer : MonoBehaviour
{
    public ScriptableChar character;
    
    public stealth stealth;
    
   

 void Start(){
        
    }
    }
public enum stealth{
    stealthMode = 1,
    StandMode = 0
}
