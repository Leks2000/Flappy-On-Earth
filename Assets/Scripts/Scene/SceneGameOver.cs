using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameOver : MonoBehaviour
{
    private MovePlayer player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<MovePlayer>();
    }
    private void Update()
    {
        if (player.GameOver())
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (player != null)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        SceneManager.LoadScene("MainMenu");
                    }
                }
            }
        }
    }
}
