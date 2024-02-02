using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    [Header("Network Settings")]
    [Tooltip("Подключения")]
    [SerializeField] private List<string> urlsToCheck = new List<string>();
    [SerializeField] private Canvas ErrorConnection;
    [SerializeField] private GameObject Player;

    private string currentSceneIndex;

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
        currentSceneIndex = SceneManager.GetActiveScene().name;
    }

    IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            foreach (string url in urlsToCheck)
            {
                UnityWebRequest www = UnityWebRequest.Get(url);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    HandleConnectionError();
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void HandleConnectionError()
    {
        if (currentSceneIndex == "Game")
        {
            Time.timeScale = 0;
            Player.GetComponentInChildren<AudioSource>().Stop();
            Player.GetComponentInChildren<PlayerController>().enabled = false;
        }
        ErrorConnection.enabled = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
