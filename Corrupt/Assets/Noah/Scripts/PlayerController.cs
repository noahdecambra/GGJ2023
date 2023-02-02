using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 20f;
    public float dashCooldown = 2f;
    public float dashDuration = 0.2f;
    float dashStartTime;
    bool isDashing;

    public Camera camera;
    public float rotationSpeed = 10f;

    public ParticleSystem flame;

    private Vector3 velocity;
    private float dashTime;

    private void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized;

        if (Input.GetKey(KeyCode.Space) && Time.time > dashTime + dashCooldown)
        {
            dashStartTime = Time.time;
            isDashing = true;
            velocity = movement * dashSpeed;
            dashTime = Time.time;
        }
        if (isDashing && Time.time - dashStartTime < dashDuration)
        {
            velocity = movement * dashSpeed;
        }
        else
        {
            isDashing = false;
            velocity = movement * speed;
        }

        transform.position += velocity * Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 lookDirection = (hit.point - transform.position);
                lookDirection.y = 0f;
                lookDirection = lookDirection.normalized;
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            var emission = flame.emission;
            emission.rateOverTime = 200f;
        }
        else 
        {
            var emission = flame.emission;
            emission.rateOverTime = 0f;

            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 1.228f, transform.position.z);
    }
}
