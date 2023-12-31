using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//Data that all players/NPC will contain.


[CreateAssetMenu(fileName = "_Prefabs", menuName = "ScriptableObjects/scriptableCharacter", order = 1)]
public class ScriptableChar : ScriptableObject
{
    public baseUnit unitPrefab;
   public type Type;
   public skins Skin;
   public Vector3 pos;
   public List<Sprite> skinsList;
   public SpriteRenderer spr;
   public GameObject tile;
   public floor Floor;
   //public direction dir;

}
public enum type{
    player,
    npc,
    gaurd,
    wizard
}
public enum skins{
    Knight,
    Jester,
    King,
    Naked

}
public enum direction{
    north,
    south,
    east,
    west
}
public enum floor{
    basement,
    main,
    lower
}