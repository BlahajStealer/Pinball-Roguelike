using UnityEngine;
using UnityEngine.InputSystem;
public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    UniversalScript us;

    GameObject GameObject;
    bool Started;
    Vector3 initial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Started = false;
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
        if (!Started)
        {
            rb.useGravity = false;
            transform.position = initial;
            
        } else
        {
            rb.useGravity = true;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame && !rb.useGravity)
        {
            initial = new Vector3(initial.x+.5f, initial.y, initial.z);
            Debug.Log("d");
        }
        if (Keyboard.current.aKey.wasPressedThisFrame && !rb.useGravity)
        {
            initial = new Vector3(initial.x-.5f, initial.y, initial.z);

            Debug.Log("a");

        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !rb.useGravity)
        {
            rb.useGravity = true;
            Started = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlapRight" && (us.RightFlap || us.RightFlapEnd))
        {
            Debug.Log("Bounce");
        }
        if (other.gameObject.tag == "FlapLeft" && (us.RightFlap || us.RightFlapEnd))
        {
            Debug.Log("Bounce");

        }
    }
}
