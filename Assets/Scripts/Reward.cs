using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public float movementSpeed;
    public AudioSource playSound;

    private float[] _fixedPositionX = new float[] {-1.4f, 0.0f, 1.4f};

    // Allows Sprite to have multiple colliders
    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    void OnEnable()
    {
        int randomPositionX = Random.Range(0, 3);
        transform.position = new Vector3(_fixedPositionX[randomPositionX], 15.0f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        if (transform.position.y <= -15.0f)
        {
            gameObject.SetActive(false); //if object enters deadzone
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player")
        {
            // playSound.Play();
            // Debug.Log(playSound.volume + "log volume");
            // Debug.Log(playSound.isPlaying + "log isPlaying");
            gameObject.SetActive(false);          
            
        }
        else if (collider.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);

            // Prevents overlapping reward and obstacle
            // if (transform.position.x == -1.4f)
            // {
            //     transform.position += Vector3.zero;
            // }
            // else if (transform.position.x == 0f)
            // {
            //     transform.position += Vector3.right * 1.4f;
            // }
            // else
            // {
            //     transform.position += Vector3.left * 1.4f;
            // }
        }
        else if (collider.gameObject.tag == "Projectile")
        {
            gameObject.SetActive(false);
        }
        else if (collider.gameObject.tag == "Big Reward")
        {
            gameObject.SetActive(false);
        }
    }

    //Collision Animation Marker
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
}
