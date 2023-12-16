using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer leftWall;
    [SerializeField] private SpriteRenderer rightWall;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private int speed;
    private Rigidbody2D rb;

    private int coins = 0;
    private bool gameOver;

    private void Start()
    {
        SetPosition();
        rb = GetComponent<Rigidbody2D>();
        gameOverCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!gameOver)
        {
            rb.MovePosition(rb.position + new Vector2(0, 1) * speed * Time.fixedDeltaTime);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    SetPosition();
                }
            }
        }
    }

    private void SetPosition()
    {
        Vector2 playerPosition = transform.position;
        if (GetComponent<SpriteRenderer>().flipY == false)
        {
            transform.position = new Vector3(rightWall.bounds.min.x, playerPosition.y);
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
            transform.position = new Vector3(leftWall.bounds.max.x, playerPosition.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                gameOverCanvas.SetActive(true);
                gameOver = true;
            }
            else if (collision.CompareTag("Beer"))
            {
                Destroy(collision.gameObject);
                coins++;
                UpdateCoinText();
            }
        }
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins.ToString();
        }
    }

    public bool GameOver()
    {
        return gameOver;
    }
}