using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    public float force;
    public float RightForce;
    public float LeftForce;
    GameObject GameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GameObject = GameObject.FindGameObjectWithTag("Empty");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlapRight")
        {
            rb.AddForce(RightForce, force, 0);
        }
        if (other.gameObject.tag == "FlapLeft")
        {
            rb.AddForce(LeftForce, force, 0);
        }
    }
}
