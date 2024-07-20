using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float delay = 0.3f;

    private Vector3 movement;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    private int score = 0;

    // Flag to ensure the position is changed only once
    private bool hasMoved = false;
    public bool isGameStarted = false;
    
    [SerializeField] private TileController tilecontroller;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Animator sunflowerAnimator;
    [SerializeField] private PlayableDirector timeline;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (isGameStarted)
        {
            // Check if the key (e.g., spacebar) is pressed and the object hasn't been moved yet
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !hasMoved)
            {
                StartCoroutine(MoveAndReset("right"));
            }
        
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !hasMoved)
            {
                StartCoroutine(MoveAndReset("left"));
            }
        
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && !hasMoved)
            {
                StartCoroutine(MoveAndReset("up"));
            }
        
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))&& !hasMoved)
            {
                StartCoroutine(MoveAndReset("down"));
            }
        }

    }
    
    private IEnumerator MoveAndReset(string direction)
    {
        switch (direction)
        {
            case "right":
                if(transform.position.x > 4.99f)
                {
                    yield break;
                }
                transform.position += new Vector3(1.25f, 0, 0);
                break;
            
            case "left":
                if(transform.position.x == 0f)
                {
                    yield break;
                }
                transform.position += new Vector3(-1.25f, 0, 0);
                break;
            
            case "up":
                if (transform.position.z == 0f)
                {
                    yield break;
                }
                transform.position += new Vector3(0, 0, 1.25f);
                break;
            
            case "down":
                if(transform.position.z < -4.99f)
                {
                    yield break;
                }
                transform.position += new Vector3(0, 0, -1.25f);
                break;
            
        }
        

        // Set the flag to true to prevent immediate further movement
        hasMoved = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Reset the flag to allow the position change again
        hasMoved = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Debug.Log("I am on the tile" + other.gameObject.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile") && other.gameObject.GetComponent<Tile>().isSunlighted)
        {
            sunflowerAnimator.SetBool("lighted", true);
            score += 10;
        }
        else
        {
            sunflowerAnimator.SetBool("lighted", false);
        }
    }

    public void GameOver()
    {
        tilecontroller.isGameOver = true;
        tilecontroller.MakeAllTilesUnlighted();
        isGameStarted = false;
        Debug.Log("Game Over");
        scoreText.text = "";
        gameOverScoreText.text = $"You gained {score} Joule Energy!";
        gameOverPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        timeline.Play();
        isGameStarted = true;
    }
}