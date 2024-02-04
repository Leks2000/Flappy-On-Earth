using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    [Header("Network Settings")]
    [Tooltip("Подключения")]
    [SerializeField] private List<string> urlsToCheck = new List<string>();
    [SerializeField] private Canvas ErrorConnection;
    [SerializeField] private GameObject Player;
    [SerializeField] private Text Error;
    [SerializeField] private Text Mess;
    [SerializeField] private Text TryAgain;


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
        if (Error != null)
        {
            Error.text = (PlayerPrefs.GetInt("LanguageInd") == 0 ? "Отсутсвует подключение к интернету. Проверьте подключение к интернету и попробуйте снова." : "No internet connection. Check your internet connection and try again.");
        }

        if (Mess != null)
        {
            Mess.text = (PlayerPrefs.GetInt("LanguageInd") == 0 ? "Ошибка подключения" : "Connection error");
        }
        if (TryAgain != null)
        {
            TryAgain.text = (PlayerPrefs.GetInt("LanguageInd") == 0 ? "Попробовать снова" : "Try again");
        }
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
