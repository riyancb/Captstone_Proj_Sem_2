using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRBoatMovement : MonoBehaviour
{
    public Rigidbody boatRB;

    public Transform paddle;
    public XRGrabInteractable paddleGrab;
    public Collider paddleCollider;

    public float riverCurrent = 0.5f;
    public float rowingForce = 20f;
    public float steerSpeed = 0.2f;
    public float maxSpeed = 20f;

    public float steerDeadZone = 20f;

    Vector3 lastPaddlePos;

    void Start()
    {
        lastPaddlePos = paddle.position;
        Physics.IgnoreCollision(paddleCollider, boatRB.GetComponent<Collider>());
    }

    void FixedUpdate()
    {
        // River drift
        boatRB.AddForce(transform.forward * riverCurrent, ForceMode.Acceleration);

        // Only allow rowing if paddle is grabbed
        if (paddleGrab.isSelected)
        {
            Vector3 velocity = (paddle.position - lastPaddlePos) / Time.fixedDeltaTime;

            if (velocity.z < -0.6f || velocity.y < -0.6f)
            {
                boatRB.AddForce(transform.forward * rowingForce, ForceMode.Acceleration);
            }
        }

        // Head steering
        Vector3 lookDir = Camera.main.transform.forward;
        lookDir.y = 0;

        float angle = Vector3.Angle(transform.forward, lookDir);

        if (angle > steerDeadZone)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);

            boatRB.MoveRotation(
                Quaternion.Slerp(
                    boatRB.rotation,
                    targetRot,
                    steerSpeed * Time.fixedDeltaTime
                )
            );
        }

        boatRB.angularVelocity = Vector3.zero;

        boatRB.velocity = Vector3.ClampMagnitude(boatRB.velocity, maxSpeed);

        lastPaddlePos = paddle.position;
    }
}