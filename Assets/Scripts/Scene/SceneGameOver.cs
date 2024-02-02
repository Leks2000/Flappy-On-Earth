using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameOver : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
                        LoadMainMenu();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (player != null)
                {
                    LoadMainMenu();
                }
            }
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
