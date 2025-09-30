// SimpleCarController.cs
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCarController : MonoBehaviour
{
    public GameObject speed;
    public float motorPower = 800f;    // forward/backward force
    public float steerTorque = 200f;   // turning torque
    public float maxSpeed = 40f;       // m/s
    public float brakeDrag = 2f;       // extra drag when braking
    public float normalDrag = 0.1f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f); // lowers center for stability
        rb.linearDamping = normalDrag;
    }

    void FixedUpdate()
    {
        float accel = Input.GetAxis("Vertical");   // W/S or up/down
        float steer = Input.GetAxis("Horizontal"); // A/D or left/right
        bool braking = Input.GetKey(KeyCode.Space);

        Vector3 forward = transform.forward;

        // limit forward speed
        float forwardVel = Vector3.Dot(rb.linearVelocity, forward);
        if (accel > 0f && forwardVel > maxSpeed)
        {
            // don't add more forward force if at speed cap
        }
        else
        {
            rb.AddForce(forward * accel * motorPower * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        // steering: apply torque around Y-axis, reduced at low speeds for stability
        float steerFactor = 10f; //Mathf.Clamp01(Mathf.Abs(forwardVel) / 1f);
        rb.AddRelativeTorque(Vector3.up * steer * steerTorque * steerFactor * Time.fixedDeltaTime, ForceMode.Acceleration);

        // simple drift / lateral friction: remove some sideways velocity
        Vector3 right = transform.right;
        float lateralVel = Vector3.Dot(rb.linearVelocity, right);
        Vector3 lateralImpulse = -right * lateralVel * 0.8f;
        rb.AddForce(lateralImpulse, ForceMode.VelocityChange);

        // braking
        rb.linearDamping = braking ? brakeDrag : normalDrag;

        speed.GetComponent<TMP_Text>().text = $"{Mathf.RoundToInt(forwardVel*2.237f)}";

    }
}
