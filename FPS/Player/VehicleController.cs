using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Wheel Transforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    private float presentAcceleration = 0f;
    public float breakingForce = 300f;
    private float presentBreakForce = 0f;

    public GameObject VehicleCamera;


    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    [Header("Vehicle Security")]
    public PlayerMovement player;
    private float radius = 3f;
    private bool isOpened = false;

    [Header("Disable things")]
    public GameObject playerCharacter;

    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKey(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;
                playerCharacter.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
                playerCharacter.SetActive(true);
            }
        }

        if (isOpened == true)
        {
            playerCharacter.SetActive(false);
            VehicleCamera.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
        }
        else if (isOpened == false)
        {
            playerCharacter.SetActive(true);
            VehicleCamera.SetActive(false);
        }
    }

    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        SteeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        SteeringWheels(frontLeftWheelCollider, frontLeftWheelTransform);
        SteeringWheels(backRightWheelCollider, backRightWheelTransform);
        SteeringWheels(backLeftWheelCollider, backLeftWheelTransform);
    }

    void SteeringWheels(WheelCollider wc, Transform wt)
    {
        Vector3 position;
        Quaternion rotation;

        wc.GetWorldPose(out position, out rotation);
        wt.position = position;
        wt.rotation = rotation;
    }

    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBreakForce = breakingForce;
        }
        else
        {
            presentBreakForce = 0f;
        }

        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftWheelCollider.brakeTorque = presentBreakForce;
    }
}
