using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

    public static float distanceTraveled;
    public float acceleration;
    public Vector3 jumpVelocity;
    public float deathY;

    private bool isTouchingPlatform;
    private Rigidbody rigidbody;
    private bool isPlaying = false;
    private Vector3 startPosition;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
    }

    void Update() {
        if (isPlaying)
        {
            if (transform.localPosition.y <= deathY) { 
                GameEventManager.TriggerGameOver();
            }

#if UNITY_ANDROID
        if(Input.GetTouch(0).phase == TouchPhase.Began && isTouchingPlatform)
#elif UNITY_WINDOWS || UNITY_EDITOR
            if (Input.GetButtonDown("Jump") && isTouchingPlatform)
#endif
            {
                rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
                isTouchingPlatform = false;
            }

            distanceTraveled = transform.localPosition.x;
        }
	}

    void FixedUpdate() {
        if (isPlaying)
        {
            if (isTouchingPlatform)
            {
                rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
            }
        }
    }

    void OnCollisionEnter() {
        isTouchingPlatform = true;
    }

    void OnCollisionExit() {
        isTouchingPlatform = false;
    }

    void GameStart()
    {
        distanceTraveled = 0f;
        rigidbody.isKinematic = false;
        transform.localPosition = startPosition;
        isPlaying = true;
    }

    void GameOver()
    {
        rigidbody.isKinematic = true;
        isPlaying = false;
    }
}