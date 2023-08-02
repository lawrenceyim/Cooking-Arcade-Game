using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    enum State {
        Walking,
        Waiting,
        Leaving
    }

    Vector3 target;
    float orderPosition;
    float speed = 3f;
    State currentState = State.Walking;
    Timer timer;
    Action callback;
    bool shiftUp;
    float yUp;
    float yDown;
    float yOffset = .2f;

    void Start()
    {
        shiftUp = true;
        target = transform.position;
        target.x = -target.x + 1;
        yUp = target.y + yOffset;
        yDown = target.y - yOffset;
        target.y = yUp;
        timer = gameObject.AddComponent<Timer>();
        timer.SetTimer(UnityEngine.Random.Range(2f, 4.5f), () => ChangeState());
    }

    void Update()
    {
        switch (currentState) {
            case State.Walking:
                MoveX();
                MoveY();
                break;
            case State.Waiting:
                break;
            case State.Leaving:
                MoveX();
                MoveY();
                if (transform.position.x >= 9f) {
                    callback();
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

    void MoveY() {
        if (shiftUp) {
            if (transform.position.y >= target.y) {
                shiftUp = false;
                target.y = yDown;
            }
            transform.localPosition += new Vector3(0, Time.deltaTime, 0);
        } else if (!shiftUp) {
            if (transform.position.y <= target.y) {
                shiftUp = true;
                target.y = yUp;
            }
            transform.localPosition += new Vector3(0, -Time.deltaTime, 0);
        }
    }

    void MoveX() {
        transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    public void ChangeState() {
        if (currentState == State.Walking) {
            currentState = State.Waiting;
            GameObject.Find("LevelManager").GetComponent<OrderManager>().AddOrder(gameObject);
        } else {
            currentState = State.Leaving;
        }
    }

    public void SetDelegate(Action action) {
        callback = action;
    }
}
