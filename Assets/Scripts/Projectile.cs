using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float movementSpeed = 3;

    // private float[] _fixedPositionY = new float[] {-4.5f, 0.0f, 4.5f};
    private float[] _fixedPositionX = new float[] {-3, 3};
    private string startPosition;

    // Start is called before the first frame update
    void OnEnable()
    {
        int randomPositionX = Random.Range(0, 2); // only range with float is maximally inclusive, int is not.
        float randomPositionY = Random.Range(-4.5f, 4.5f);
        if (_fixedPositionX[randomPositionX] == -3) {
            startPosition = "left";
        } else {
            startPosition = "right";
        }

        transform.position = new Vector3(_fixedPositionX[randomPositionX], randomPositionY, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startPosition == "left") {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            if (transform.position.x >= 3.0f) {
                gameObject.SetActive(false);
            }
        } else {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            if (transform.position.x <= -3.0f) {
                gameObject.SetActive(false);
            }
        }
    }
}
