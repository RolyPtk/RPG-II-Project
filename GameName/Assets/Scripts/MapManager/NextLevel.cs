using UnityEngine;

public class NextLevel : MonoBehaviour
{
    //[SerializeField] bool goNextLevel;
    //[SerializeField] string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
        //go to next level
        SceneController.Instance.NextLevel();
                
                
        }
    }
}
