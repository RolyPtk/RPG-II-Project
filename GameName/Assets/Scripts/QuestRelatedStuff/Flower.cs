using UnityEngine;

public class Flower : MonoBehaviour
{
    private bool playerNearby = false;
    void Update(){
        if (playerNearby && InputManager.Instance.IsPressed(InputManager.Instance.interactKey) && QuestManager.Instance.IsQuestActive("2")){
            GameEvents.ItemGathered("flower");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}