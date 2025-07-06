using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Vector2 moveInput;

    public float moveSpeed = 5f;
    public float rotateSpeed = 100f; // Degrees per second
    private Rigidbody rb;
    private Animator animator;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RestartNotification; // reused for both death + level complete

    private int numberofplants = 0;
    public int maxPlants = 10; // configurable in Inspector

    void Awake()
    {
        controls = new InputSystem_Actions();
    }

    void OnEnable()
{
    controls.Enable();
    controls.Player.Enable();  // explicitly enable the Player map

    controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
}

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        updateScore();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (RestartNotification != null)
            RestartNotification.text = "";
    }

    void FixedUpdate()
    {
        float moveAmount = moveInput.y;
        float turnAmount = moveInput.x;

        Vector3 moveDirection = transform.forward * moveAmount;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (Mathf.Abs(turnAmount) > 0.1f)
        {
            float turnSpeedThisFrame = turnAmount * rotateSpeed * Time.fixedDeltaTime;
            Quaternion turnOffset = Quaternion.Euler(0f, turnSpeedThisFrame, 0f);
            rb.MoveRotation(rb.rotation * turnOffset);
        }

        // ✅ Proper animator speed parameter
        float currentSpeed = moveInput.magnitude;  // between 0 and 1
        animator.SetFloat("Speed", currentSpeed);
        Debug.Log("moveInput: " + moveInput);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Trigger] Banana Man collided with: {other.name}, Tag: {other.tag}");

        if (other.CompareTag("Plant"))
        {
            Debug.Log("[Trigger] It’s a Plant! Picking it up…");

            numberofplants++;
            updateScore();

            GrowCharacter();

            Destroy(other.gameObject);

            if (numberofplants >= maxPlants)
            {
                Debug.Log("[Level] Max plants collected. Level complete!");
                StartCoroutine(LevelComplete());
            }
        }
        else if (other.CompareTag("BadPlant"))
        {
            Debug.Log("[Trigger] Oh no! Bad Plant. Banana Man dies…");
            RestartGame(); // triggers coroutine
        }
        else
        {
            Debug.Log("[Trigger] Not a Plant — ignoring.");
        }
    }

    private void updateScore()
    {
        ScoreText.text = numberofplants.ToString();
    }

    private void GrowCharacter()
    {
        float growthFactor = 1.08f;
        transform.localScale *= growthFactor;

        Debug.Log($"[Growth] Banana Man grew! New scale: {transform.localScale}");
    }

    public void RestartGame()
    {
        StartCoroutine(RestartWithMessage());
    }

    private IEnumerator RestartWithMessage()
    {
        if (RestartNotification != null)
        {
            RestartNotification.text = "You have lost, Restarting now…";
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LevelComplete()
    {
        if (RestartNotification != null)
        {
            RestartNotification.text = "Level Complete! Loading next level…";
        }

        yield return new WaitForSeconds(2f);

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("No more levels! Reloading current level.");
            SceneManager.LoadScene(currentIndex);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Solid"))
        {
            Debug.Log("Player hit: " + collision.gameObject.name);

            if (collision.gameObject.name.Contains("TallTree"))
            {
                Debug.Log("Tall trees don’t count!");
                if (RestartNotification != null)
                {
                    RestartNotification.text = "Tall trees don’t count!";
                }
            }
            else
            {
                Debug.Log("Player hit a solid object.");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Solid") &&
            collision.gameObject.name.Contains("TallTree"))
        {
            Debug.Log("Moved away from tall tree.");
            if (RestartNotification != null)
            {
                RestartNotification.text = "";
            }
        }
    }
}
