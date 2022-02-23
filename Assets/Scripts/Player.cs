using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // public SoundController sound;
    // [SerializedField]

    // public AudioClip collectReward;
    // public AudioSource audioSource;

    public ScoreController score;

    public float[] lanes = new float[] { -3f, 0f, 3f }; // x position of each lane... for some reason im exceeding world bounds for it to work properly??
    private int targetLane = 1; // middle lane

    public float gravity = 2f;
    private float jumpHeight = 1f;
    private float targetJump = 0f;

    public float laneSwapSpeed = 2f;
    public float jumpSpeed = 2f;

    private bool isSwappingLanes = false;
    private bool isJumping = false;

    private float skyZone = 3.5f;
    private float deadZone = -4f;

    // void Awake()
    // {
    //     transform.position = new Vector3(0, 0, 0);
    //     Debug.Log(rows[targetRow]);
    // }

    // Start is called before the first frame update
    void Start()
    {
        targetJump = transform.position.x + jumpHeight; // Initialize tartgetJump position.

        // sound isnt working as intended currently...
        // audioSource = GetComponent<AudioSource>();
        // if (audioSource == null)
        // {
        //     Debug.Log("The AudioSource in the player is NULL!");
        // }
        // else
        // {
        //     audioSource.clip = collectReward;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);

        /* Receive user input */
        // Upwards
        if (isJumping == false && Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y <= skyZone)
        {
            targetJump = transform.position.y + jumpHeight;
            isJumping = true;
        }
        // Leftwards
        else if (isSwappingLanes == false && Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
        {
            targetLane--;
            isSwappingLanes = false;
        }
        // Rightwards
        else if (isSwappingLanes == false && Input.GetKeyDown(KeyCode.RightArrow) && targetLane < lanes.Length - 1.0)
        {
            targetLane++;
            isSwappingLanes = false;
        }

        /* Vertical movement */
<<<<<<< Updated upstream
=======
        // Gravity affecting player
        else
        {
            transform.position += Vector3.down * gravity * Time.deltaTime; // if isJumping = false?
        }
>>>>>>> Stashed changes
        // Player hitting the floor
        if (transform.position.y <= deadZone)
        {
            EndGame();
        }
        // Gravity affecting player
        else
        {
            transform.position += Vector3.down * gravity * Time.deltaTime; // if isJumping = false?
        }
        // Move player towards the target jump's position
        if (isJumping == true && Mathf.Abs(transform.position.y - targetJump) > 0.05f)
        {
            transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
        }
        // When close enough to the final vertical position, snap to it
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lanes[targetLane], targetJump, 0), Time.deltaTime); // may need to say the targetlane as well.
            isJumping = false;
        }

        /* Horizontal movement */
        // Move player towards the target lane's position
        if (isSwappingLanes == true && Mathf.Abs(transform.position.x - lanes[targetLane]) > 0.05f)
        {
            if (transform.position.x < lanes[targetLane])
            {
                transform.position += Vector3.right * laneSwapSpeed * Time.deltaTime;
            }
            else if (transform.position.x > lanes[targetLane])
            {
                transform.position += Vector3.left * laneSwapSpeed * Time.deltaTime;
            }
        }
        // When close enough to the final horizontal position, snap to it
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(lanes[targetLane], transform.position.y, 0), Time.deltaTime); // causes a slight player movement downward cause its on position y not targetjump...
            isSwappingLanes = false;
        }
    }

    /* Player collision */
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Projectile")
        {
            EndGame();
        }
        else if (collider.gameObject.tag == "Reward")
        {
            // sound.OnCollisionReward();
            // audioSource.PlayOneShot(collectReward, 1.0F); // cant hear it?
            score.GetReward();
        }
    }
    
    /* Resolve game over */
    void EndGame()
    {
        Debug.Log("Game Over!");
        score.SetHighScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // set to restart scene
    }
}
