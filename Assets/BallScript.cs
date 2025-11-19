using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    UniversalScript us;
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
        us = GameObject.GetComponent<UniversalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlapRight" && (us.RightFlap || us.RightFlapEnd))
        {
            Debug.Log("Bounce");
            rb.AddForce(RightForce,force,0);
        }
        if (other.gameObject.tag == "FlapLeft" && (us.RightFlap || us.RightFlapEnd))
        {
            Debug.Log("Bounce");
            rb.AddForce(LeftForce,force,0);

        }
    }
}
