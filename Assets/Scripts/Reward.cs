using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public float movementSpeed = 3;

    private float[] _fixedPositionX = new float[] {-1.4f, 0.0f, 1.4f};

    // Start is called before the first frame update
    void OnEnable()
    {
        int randomPositionX = Random.Range(0, 3);
        transform.position = new Vector3(_fixedPositionX[randomPositionX], 6.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        if (transform.position.y <= -6.0f) {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            gameObject.SetActive(false);
        }
    }
}
