using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    bool walking = true;
    Vector3 target;
    float speed = 2f;

    void Start()
    {
        target = transform.position;
        target.x = -target.x;
    }

    void Update()
    {
        if (walking) {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target) {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Seat")) {
            // if (other.gameObject.GetComponent<Seat>().IsEmpty()) {
            //     walking = false;
            // }
        }
    }

}
