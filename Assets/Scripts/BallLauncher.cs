using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
   [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform slingShot;

    [SerializeField] private float despawnTime;
    [SerializeField] private float releaseDelay;
    [SerializeField] private float respawnTime;

    private Rigidbody2D ballRB;
    private SpringJoint2D ballSpringJoint;
    private GameObject newBall;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        SpawnBall();
    }

    private void Update()
    {
        if (newBall == null)
            return;

        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    UpdateBallPosition();
                    break;
                case TouchPhase.Moved:
                    UpdateBallPosition();
                    break;
                case TouchPhase.Stationary:
                    UpdateBallPosition();
                    break;
                case TouchPhase.Ended:
                    ReleaseFinger();
                    break;
                case TouchPhase.Canceled:
                    ReleaseFinger();
                    break;
            }
        }
    }

    private void SpawnBall()
    {
        newBall = Instantiate(ballPrefab, slingShot.position, Quaternion.identity);
        ballRB = newBall.GetComponent<Rigidbody2D>();
        ballSpringJoint = newBall.GetComponent<SpringJoint2D>();
        ballSpringJoint.connectedBody = slingShot.GetComponent<Rigidbody2D>();
        ballSpringJoint.distance = -1;
    }

    private void UpdateBallPosition()
    {
        ballRB.bodyType = RigidbodyType2D.Kinematic;
        Vector2 screenTouchPosition = Input.GetTouch(0).position;
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenTouchPosition);
        newBall.transform.position = worldPosition;
    }

    private void ReleaseFinger()
    {
        ballRB.bodyType = RigidbodyType2D.Dynamic;
        newBall = null;
        Invoke("DelayRelease", releaseDelay);
        Invoke("SpawnBall", respawnTime);
        Destroy(newBall, despawnTime);
    }

    private void DelayRelease()
    {
        ballSpringJoint.enabled = false;
        ballSpringJoint = null;
    }
}
