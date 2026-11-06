

using TMPro;

using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerControllerTask2 : MonoBehaviour

{

    [SerializeField] public int Score = 0;

    [SerializeField] public float Timer = 60.0f;

    [SerializeField] public bool LevelComplete = false;

    [SerializeField] GameObject BlueDoor;
    [SerializeField] GameObject FailTextBox;
    [SerializeField] TextMeshProUGUI ScoreTextBox;
    [SerializeField] TextMeshProUGUI TimerTextBox;
    [SerializeField] TextMeshProUGUI CompeletionTextBox;

    [SerializeField] float moveSpeed = 5.0f;

    [SerializeField] AudioSource playerSound;
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioClip gameOverClip;

    [SerializeField] Rigidbody physicsBody;

    // Camera reference

    [SerializeField] private Transform playerCamera;

    float xInput;

    float zInput;

    float updown;

    void Start()

    {

        // Hide the fail text at the beginning

        FailTextBox.SetActive(false);

    }

    void Update()

    {

        // Open the blue door when the player collects 3 coins

        if (Score >= 3)

        {

            BlueDoor.SetActive(false);

        }

        // Stop the timer at zero

        if (Timer > 0)

        {

            Timer -= Time.deltaTime;

            Timer = Mathf.Clamp(Timer, 0, 60);

        }

        // Update the UI

        UpdateTextBoxes();

        // -------------------------

        // CAMERA-RELATIVE MOVEMENT

        // -------------------------

        xInput = 0f;

        zInput = 0f;

        updown = 0f;

        if (Keyboard.current != null)

        {

            if (Keyboard.current.aKey.isPressed)

            {

                xInput = -1f;

            }

            if (Keyboard.current.dKey.isPressed)

            {

                xInput = 1f;

            }

            if (Keyboard.current.wKey.isPressed)

            {

                zInput = 1f;

            }

            if (Keyboard.current.sKey.isPressed)

            {

                zInput = -1f;

            }
            if (Keyboard.current.uKey.wasPressedThisFrame)

            {

                physicsBody.position = new Vector3(3, 0, 0);

            }

        }

        // Get the camera's forward and right directions

        Vector3 cameraForward = playerCamera.forward;

        Vector3 cameraRight = playerCamera.right;

        // Keep forward and sideways movement horizontal

        cameraForward.y = 0f;

        cameraRight.y = 0f;

        cameraForward.Normalize();

        cameraRight.Normalize();

        // Calculate movement relative to the camera

        Vector3 movement =

            cameraRight * xInput +

            cameraForward * zInput +

            Vector3.up * updown;

        // Prevent diagonal movement from being faster

        movement = movement.normalized;

        // Get movement direction

        Vector3 direction = new Vector3(

            movement.x,

            0,
            movement.z
        );

        // Move the player using physics

        physicsBody.linearVelocity = new Vector3(

            direction.x * moveSpeed,
            0,
            direction.z * moveSpeed
        );

        if (Keyboard.current.f1Key.isPressed)

            PlayerPrefs.SetInt("PlayerScore", Score);

        if (Keyboard.current.f2Key.isPressed)

            Score = PlayerPrefs.GetInt("PlayerScore");
 

    }

    void UpdateTextBoxes()

    {

        ScoreTextBox.text = "Score = " + Score.ToString();

        TimerTextBox.text = "Time = " + Timer.ToString("F1");

        CompeletionTextBox.text =

            "Level Complete = " + LevelComplete.ToString();

        // Show the fail text when time runs out

        if (Timer <= 0)

        {

            Timer = 0;

            FailTextBox.SetActive(true);

            if (gameOverClip != null && playerSound != null)

            {

                playerSound.PlayOneShot(gameOverClip);

                this.enabled = false;

            }

        }

    }

    void AddPoints(int amount)

    {

        Score += amount;

    }

    void OnTriggerEnter(Collider other)

    {

        // Check if the object has the Coin tag

        if (other.gameObject.CompareTag("Coin"))

        {

            // Increase score by 1

            AddPoints(1);

            // Destroy the collected coin

            Destroy(other.gameObject);

            if (playerSound != null && coinClip != null)

            {

                playerSound.PlayOneShot(coinClip);

            }

        }

    }

}

