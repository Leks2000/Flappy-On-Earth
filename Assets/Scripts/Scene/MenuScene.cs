using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                transform.Find("ButtonStart").GetComponent<Button_UI>().ClickFunc = () =>
                {
                    SceneManager.LoadScene("Game");
                };
                transform.Find("ButtonShop").GetComponent<Button_UI>().ClickFunc = () =>
                {
                    SceneManager.LoadScene("Game");
                };
            }
        }
    }
}
