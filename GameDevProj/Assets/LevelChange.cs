using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneID;
    public bool passPortal = false;
    public bool gameEnder = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!gameEnder)
            {
                SceneManager.LoadScene(sceneID);
            }
            passPortal = true;
        }
    }
}
