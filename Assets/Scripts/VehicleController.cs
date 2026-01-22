using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    public InputAction accelerate;
    public InputAction brake;
    public InputAction steer;

    public float accelerationValue;
    public float brakeValue;
    public float steerValue;

    public float currentSpeed;
    public float maxSpeed;

    const float ACCELERATION_FACTOR = 30.0f;
    const float BRAKE_FACTOR = -5.0f;
    const float STEER_FACTOR = 30.0f;

    private Rigidbody rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        accelerate.Enable();
        brake.Enable();
        steer.Enable();

        accelerate.performed += AccelerateInput;
        accelerate.canceled += AccelerateInput;

        brake.performed += BrakeInput;
        brake.canceled += BrakeInput;

        steer.performed += SteerInput;
        steer.canceled += SteerInput;
    }

    public void AccelerateInput(InputAction.CallbackContext c)
    {
        accelerationValue = c.ReadValue<float>() * ACCELERATION_FACTOR;
    }

    public void BrakeInput(InputAction.CallbackContext c)
    {
        brakeValue = c.ReadValue<float>() * BRAKE_FACTOR;
    }

    public void SteerInput(InputAction.CallbackContext c)
    {
        steerValue = c.ReadValue<float>() * STEER_FACTOR;
    }

    private void FixedUpdate()
    {
        float currentSpeed = rb.linearVelocity.magnitude;

        if (accelerationValue > 0f)
        {
            rb.AddForce(transform.forward * accelerationValue, ForceMode.Acceleration);
        }

        if (brakeValue < 0f)
        {
            rb.AddForce(transform.forward * brakeValue, ForceMode.Acceleration);
        }

        
        if (currentSpeed > 0.1f)
        {
            float steerAmount = steerValue * Mathf.Sign(Vector3.Dot(rb.linearVelocity, transform.forward));
            transform.Rotate(0f, steerAmount * Time.fixedDeltaTime, 0f);
        }

        if (currentSpeed > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    public void SpeedBoost(float boost_)
    {
        StartCoroutine(BoostTimer(boost_, 10f));
    }

    private IEnumerator BoostTimer(float boost_, float duration_)
    {
        currentSpeed += boost_;
        maxSpeed += boost_ * 2;
        yield return new WaitForSeconds(duration_);
        currentSpeed -= boost_;
        maxSpeed -= boost_ * 2;
    }
}
