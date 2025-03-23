using UnityEngine;

public class SubmarineManager : MonoBehaviour
{
    public float fuel = 100f; 
    public float maxFuel = 100f;
    [SerializeField] float fuelUsageSpeed = 1f;
    [SerializeField] float mineFuelReduction = 5f;
    [SerializeField] Vector3 impulseForce = Vector3.up * 10;
    [SerializeField] Vector3 constantForce = Vector3.up * 20;
    [SerializeField] Vector3 forwardForce = Vector3.right * 20;
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    bool thrust;
    Rigidbody rb;
    [SerializeField] float minRotation = 35;
    [SerializeField] float maxRotaion = -35;
    [SerializeField] float pitchSpeed = 1;
    [SerializeField] float speed = 1;
    [SerializeField] Transform ship;
    private bool resetted = false;
    [SerializeField] private float rightBorderX = 10f;
    [SerializeField] private ObstaclePlacer[] obstaclePlacers;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        fuel -= Time.deltaTime * fuelUsageSpeed;
        if (fuel <= 0)
        {
            gameManager.GameOver();
            enabled = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            thrust = true;
            resetted = false;
        }
        else if (Input.GetButton("Jump"))
        {
            Vector3 dest = new Vector3(maxRotaion, ship.transform.localRotation.eulerAngles.y, ship.transform.localRotation.eulerAngles.z);
            ship.transform.localRotation = Quaternion.Lerp(ship.transform.localRotation, Quaternion.Euler(dest), Time.deltaTime * pitchSpeed);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            thrust = false;
        }
        else
        {
            Vector3 dest = new Vector3(minRotation, ship.transform.localRotation.eulerAngles.y, ship.transform.localRotation.eulerAngles.z);
            ship.transform.localRotation = Quaternion.Lerp(ship.transform.localRotation, Quaternion.Euler(dest), Time.deltaTime * pitchSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (thrust)
        {
            if (forceMode == ForceMode.Impulse)
            {
                thrust = false;
                resetted = true;
                rb.AddForce(impulseForce, forceMode);
                ship.transform.localEulerAngles = new Vector3(maxRotaion, ship.transform.localRotation.eulerAngles.y, ship.transform.localRotation.eulerAngles.z);
            }
            else
            {
                rb.AddForce(constantForce, forceMode);
            }
        }
        rb.AddForce(forwardForce, forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger by: {other.gameObject}", other.gameObject);
        if (other.gameObject.CompareTag("LeftBorder"))
        {
            Vector3 newPosition = new Vector3(rightBorderX, transform.position.y, transform.position.z);
            transform.position = newPosition;

            foreach (ObstaclePlacer obstaclePlacer in obstaclePlacers)
            {
                obstaclePlacer.ResetObstaclePlacer();
            }
        }
        else if (other.gameObject.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            fuel = Mathf.Clamp(fuel + fuelUsageSpeed, 0, maxFuel);
            Debug.Log($"Fuel gained: {fuel}");
        }
        else if (other.gameObject.CompareTag("Mine"))
        {
            Destroy(other.gameObject);
            fuel = Mathf.Clamp(fuel - mineFuelReduction, 0, maxFuel);
            Debug.Log($"Fuel lost: {fuel}");
            if (fuel <= 0)
            {
                gameManager.GameOver();
                enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("LeftBorder"))
        {
            Vector3 newPosition = new Vector3(rightBorderX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        else
        {
            gameManager.GameOver();
            rb.isKinematic = true;
            enabled = false;
        }
    }

    public void StopAllObstaclePlacers()
    {
        foreach (ObstaclePlacer obstaclePlacer in obstaclePlacers)
        {
            obstaclePlacer.StopPlacingObstacles();
        }
    }
}