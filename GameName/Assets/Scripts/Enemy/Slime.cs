using UnityEngine;
// simple class to identify Slimes and if need be add special behaviour which isn't needed for this project for simplicity sake
public class Slime : MonoBehaviour{
    public void OnDeath(){
        GameEvents.EnemyKilled("slime");
        Destroy(gameObject);
    }
}