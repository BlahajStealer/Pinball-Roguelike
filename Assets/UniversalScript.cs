using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class UniversalScript : MonoBehaviour
{

    public GameObject Flap1;
    public GameObject Flap2;
    public GameObject ball;
    public float FlapSpeed;
    public float RespawnSpeed;
    public bool RightFlap;
    public bool LeftFlap;
    public bool RightFlapEnd;
    public bool LeftFlapEnd;
    public bool Respawning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flap();
        FlapAct();
        if (ball.transform.position.y <= -1.5f)
        {
            Respawning = true;
            //Vector3 tempVect = ball.transform.position;
            //Vector3 NewVect = new Vector3(tempVect.x, 10.18f, tempVect.z);
            StartCoroutine(moveObjectRight());
            StartCoroutine(moveObjectUp());

        }
        
        
    }

    void Flap()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !LeftFlap && !LeftFlapEnd)
        {
            Debug.Log("left Flap Activated");
            LeftFlap = true;
        }        
        if (Mouse.current.rightButton.wasPressedThisFrame && !RightFlap && !RightFlapEnd)
        {
            Debug.Log("Right Flap Activated");
            RightFlap = true;

        }
        
    }
    void FlapAct()
    {
        if (RightFlap)
        {
            Quaternion currentRot = Flap1.transform.rotation;
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, -135.0f);
            Flap1.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                RightFlapEnd = true;
                RightFlap = false;
            }
        }
        if (RightFlapEnd)
        {
            Quaternion currentRot = Flap1.transform.rotation;
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, -45.0f);
            Flap1.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                RightFlapEnd = false;
            }
        }
        if (LeftFlap)
        {
            Quaternion currentRot = Flap2.transform.rotation;
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 135.0f);
            Flap2.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                LeftFlapEnd = true;
                LeftFlap = false;
            }
        }
        if (LeftFlapEnd)
        {
            Quaternion currentRot = Flap2.transform.rotation;
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 45.0f);
            Flap2.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                LeftFlapEnd = false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            Debug.Log("Bounce!");
        }
    }
    public IEnumerator moveObjectRight()
    {
        Vector3 Destination = new Vector3(-9.31f, -1.56f, .2829f);
        Vector3 Origin = ball.transform.position;

        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0)
        {
            CurrentTime += Time.deltaTime;
            ball.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / RespawnSpeed);
            yield return StartCoroutine(moveObjectUp());
;
        }
    }
    public IEnumerator moveObjectUp()
    {
        Vector3 Destination = new Vector3(-9.31f, 12.09f, .2829f);
        Vector3 Origin = ball.transform.position;

        float CurrentTime = 0f;
        while (Vector3.Distance(transform.localPosition, Destination) > 0)
        {
            CurrentTime += Time.deltaTime;
            ball.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / RespawnSpeed);
            yield return null;
        }

    }
    
}
