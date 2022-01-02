
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Letter letter = coll.GetComponent<Letter>();
        if(letter != null && letter.isMainCharacter)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
