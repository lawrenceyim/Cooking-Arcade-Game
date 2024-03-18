using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {
    enum State {
        Walking,
        Waiting,
        Leaving
    }

    public Controller controller;
    Vector3 target;
    float speed = 3f;
    State currentState = State.Walking;
    float travelTime;
    Timer timer;
    Action DecreaseCustomerCount;
    bool shiftUp;
    float yUp;
    float yDown;
    float yOffset = .2f;
    CustomerSpawner customerSpawner;

    public void Initialize(CustomerSpawner customerSpawner, float travelTime) {
        this.customerSpawner = customerSpawner;
        this.travelTime = travelTime;
        timer = gameObject.AddComponent<Timer>();
        timer.SetTimer(travelTime, () => ChangeState());
    }

    void Start() {
        if (controller == null) {
            Debug.LogError("controller script is null");
        }
        shiftUp = true;
        target = transform.position;
        target.x = -target.x + 1;
        yUp = target.y + yOffset;
        yDown = target.y - yOffset;
        target.y = yUp;
    }

    void Update() {
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
                    DecreaseCustomerCount();
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
            controller.AddCustomerOrder(gameObject);
        } else {
            currentState = State.Leaving;
            customerSpawner.ReturnTravelTime(travelTime);
        }
    }

    public void SetDelegate(Action action) {
        DecreaseCustomerCount = action;
    }
}
