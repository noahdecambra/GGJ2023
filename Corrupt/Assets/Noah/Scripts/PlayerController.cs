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

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, camera.transform.position.y - transform.position.y));
        mousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);

        Quaternion targetRotation = Quaternion.LookRotation(mousePosition - transform.position);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 1.228f, transform.position.z);
    }
}
