using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Sprite Settings")]
    [SerializeField] private SpriteRenderer leftWall;
    [SerializeField] private SpriteRenderer rightWall;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Tooltip("Все скины на игрока")]
    [SerializeField] private List<Sprite> characterSprites;
    [Space]
    [Header("UI Settings")]
    [SerializeField] private Image gameOverCanvas;
    [SerializeField] private Text coinText;
    [SerializeField] private AudioSource Music;
    [SerializeField] private int speed;
    [SerializeField] private AudioSource AudioDead;

    private int characterId;
    private int coins = 0;
    private bool gameOver;

    private Rigidbody2D rb;
    [SerializeField] private SaveData save;
    private void Awake()
    {
        characterId = PlayerPrefs.GetInt("character");
        ChangeCharacterSkin(characterId);
    }

    private void ChangeCharacterSkin(int characterIndex)
    {
        if (characterIndex >= 0 && characterIndex < characterSprites.Count)
        {
            spriteRenderer.sprite = characterSprites[characterIndex];
        }
    }
    private void Start()
    {
        SetPosition();
        rb = GetComponent<Rigidbody2D>();
        gameOverCanvas.enabled = false;
        gameOver = false;
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (AudioDead.enabled)
            {
                AudioDead.Play();
            }
            rb.MovePosition(rb.position + new Vector2(0, 1) * speed * Time.fixedDeltaTime);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    CheckTouchPosition(touch.position);
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

                gameOverCanvas.enabled = true;
                gameOver = true;
                Music.Stop();
                save.AddCoins(coins);
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
            coinText.text = "кружок " + coins.ToString();
        }
    }

    public bool GameOver()
    {
        return gameOver;
    }
    private void CheckTouchPosition(Vector2 touchPosition)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        if (touchPosition.y < screenHeight / 2)
        {
            Vector2 playerPosition = transform.position;
            if (touchPosition.x > screenWidth / 2)
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
    }
}