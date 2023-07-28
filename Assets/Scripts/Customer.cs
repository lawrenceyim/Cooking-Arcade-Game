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
    float speed = 4f;
    State currentState = State.Walking;
    Timer timer;
    Action callback;


    void Start()
    {
        target = transform.position;
        target.x = -target.x;
        timer = gameObject.AddComponent<Timer>();
        timer.SetTimer(UnityEngine.Random.Range(2f, 5f), () => ChangeState());
    }

    void Update()
    {
        switch (currentState) {
            case State.Walking:
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                break;
            case State.Waiting:
                break;
            case State.Leaving:
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position == target) {
                    callback();
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

    public void ChangeState() {
        if (currentState == State.Walking) {
            currentState = State.Waiting;
            GameObject.Find("LevelManager").GetComponent<OrderManager>().AddOrder();
        } else {
            currentState = State.Leaving;
        }
    }

    public void SetDelegate(Action action) {
        callback = action;
    }
}
