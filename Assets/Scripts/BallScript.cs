using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    UniversalScript us;
    public float ForceX;
    public float ForceY;
    AudioSource audioSource;
    public GameObject GameObject;
    public TextMeshProUGUI Text;
    public GameObject Camera;
    Vector3 initial;
    [SerializeField] bool startCooldown;
    [SerializeField] float cooldown;
    MeshCollider mc;
    MeshRenderer Main;
    MeshRenderer Sub;
    public float ScoreCooldown;
    bool wait;
    float waitToTruify;
    public float PointForceX;
    public float PointForceY;
    public bool JustHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        audioSource = Camera.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        GameObject = GameObject.FindGameObjectWithTag("Empty");
    }
    void Start()
    {
        initial = transform.position;
        us = GameObject.GetComponent<UniversalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Score: " + us.score.ToString();
        
        if (wait)
        {
            waitToTruify += Time.deltaTime;
            if (waitToTruify > .1f) {
                mc.enabled = true;
                Main.enabled = true;
                Sub.enabled = true;
                wait = false;
                waitToTruify = 0;
            }
        }
        if (startCooldown)
        {
            cooldown += Time.deltaTime;
            if (cooldown > ScoreCooldown)
            {
                startCooldown = false;
                cooldown = 0.0f;
            }
        }
        if (mc != null)
        {
            if (us.ForceCounter.activeSelf)
            {
                mc.enabled = false;
                Main.enabled = false;
                Sub.enabled = false;
            }
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.CompareTag("Pingy Thing") || collision.gameObject.CompareTag("AddedPinger")) && !startCooldown)
        {
            JustHit = true;

            startCooldown = true;
            audioSource.Play();
            us.score += 100;
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        } else if (collision.gameObject.CompareTag("Gold Pingy Thing") && !startCooldown)
        {
            JustHit = true;

            startCooldown = true;
            audioSource.Play();
            us.score += 300;
            Debug.Log(us.score);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * PointForceX, rb.linearVelocity.y * PointForceY, 0);

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Appear Trigger"))
        {
            mc = other.GetComponent<MeshCollider>();
            Main = other.GetComponent<MeshRenderer>();
            Sub = other.transform.GetChild(0).GetComponent<MeshRenderer>();
            wait = true;

        }
        else if (other.gameObject.CompareTag("FlapRight") && (us.RightFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);
        }
        else if (other.gameObject.CompareTag("FlapLeft") && (us.LeftFlap))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * ForceX, rb.linearVelocity.y * ForceY, 0);
        }

    }
    private void OnMouseOver()
    {
        Debug.Log("Hai :3");
    }
}
