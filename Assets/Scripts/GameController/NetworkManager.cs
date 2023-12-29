using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private GameObject ErrorConnection;

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    System.Collections.IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                var currentSceneIndex = SceneManager.GetActiveScene().name;
                if (currentSceneIndex == "Game")
                {
                    Time.timeScale = 0;
                }
                ErrorConnection.SetActive(true);
            }
            else
            {
                ErrorConnection.SetActive(false);
            }
            yield return new WaitForSeconds(5f);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
