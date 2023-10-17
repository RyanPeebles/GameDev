using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

     public static GameManager instance;
     public GameState GameState;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start(){
        changeState(GameState.SpawnUnits);
    }

    public void changeState(GameState newState){
        GameState = newState;
        switch(GameState){
            case GameState.SpawnUnits:
                break;
            case GameState.SpawnItems:
                break;
            case GameState.GameOver:
                break;
            case GameState.intoStealth:
                break;
            case GameState.outOfStealth:
                break;
        }
    }
  
}
 public enum GameState{
    SpawnUnits,
    SpawnItems,
    GameOver,
    intoStealth,
    outOfStealth
 }