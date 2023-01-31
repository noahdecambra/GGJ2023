using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 20f;
    public Camera camera;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.velocity = movement * dashSpeed;
        }
        else
        {
            rigidBody.velocity = movement * speed;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, camera.transform.position.y - transform.position.y));

        Quaternion targetRotation = Quaternion.LookRotation(mousePosition - transform.position);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }
}