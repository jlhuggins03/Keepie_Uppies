using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float movementSpeed = 3;

    private float[] _fixedPositionX = new float[] {-1.3f, 0.0f, 1.3f};

    // Start is called before the first frame update
    void OnEnable()
    {
        int randomPositionX = Random.Range(0, 3);
        transform.position = new Vector3(_fixedPositionX[randomPositionX], 6.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        if (transform.position.y <= -6.0f) {
            gameObject.SetActive(false);
        }
    }
}
