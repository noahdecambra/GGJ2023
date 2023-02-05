using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCoolDown = 2f;
    private float dashTime;
    private bool isDashing;

    public new Camera camera;
    public float rotationSpeed = 10f;

    public ParticleSystem flame;
    public Slider fuelSlider;
    public float fuel;
    public float fuelRate;
    float maxFuel;

    public bool recharging;

    private Rigidbody rb;
    private Vector3 velocity;

    public int health;

    private void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        rb = GetComponent<Rigidbody>();

        maxFuel = fuel;
        var emission = flame.emission;
        emission.rateOverTime = 0f;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized;

        if (Input.GetKey(KeyCode.Space) && Time.time > dashTime + dashCoolDown)
        {
            isDashing = true;
            dashTime = Time.time;
        }

        if (isDashing && Time.time - dashTime < dashDuration)
        {
            velocity = movement * dashSpeed;
        }
        else
        {
            isDashing = false;
            velocity = movement * speed;
        }

        fuelSlider.maxValue = 100;
        fuelSlider.minValue = 0;
        fuelSlider.value = fuel;

        if (fuel == 0 && !recharging) 
        {
            recharging = true;
            StartCoroutine(RechargeFuel());

            //FindObjectOfType<AudioManager>().StopPlaying("Flamethrower");
        }

        if (Input.GetKey(KeyCode.Mouse1) && fuel > 0 && !recharging)
        {
            FindObjectOfType<AudioManager>().Play("Flamethrower");

            if (fuel != 0)
            {
                fuel -= fuelRate * Time.deltaTime;                
                var emission = flame.emission;
                emission.rateOverTime = 200f;

                //FindObjectOfType<AudioManager>().Play("Flamethrower");
            }

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
        }
        else
        {
            if (fuel == 100)
            {
                recharging = false;
            }

            else
            {
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    var emission = flame.emission;
                    emission.rateOverTime = 0f;

                    //FindObjectOfType<AudioManager>().StopPlaying("Flamethrower");
                }
                if (fuel != 100)
                {
                    fuel += fuelRate * Time.deltaTime;
                }
            }

            if (fuel != 100)
            {
                var emission = flame.emission;
                emission.rateOverTime = 0f;
            }

            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                //FindObjectOfType<AudioManager>().Play("Footsteps");
            }
            else
            {
                //FindObjectOfType<AudioManager>().StopPlaying("Footsteps");
            }
        }        
        
        if  (fuel < 0)
        {
            fuel = 0;
        }
        else if (fuel > 100)
        {
            fuel = 100;
        }
        
    }

    private IEnumerator RechargeFuel()
    {
        float waitStart = Time.time;
        float waitDuration = 4f;
        while (fuel < 100)
        {
            fuel += fuelRate * Time.deltaTime;
            if (Time.time - waitStart >= waitDuration) 
            {
                break;
            }
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(velocity - rb.velocity, ForceMode.VelocityChange);
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;
        Debug.Log(health);
    }
}
