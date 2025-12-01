using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
public class UniversalScript : MonoBehaviour
{

    Coroutine rightCoroutine;
    Coroutine upCoroutine;
    public float score;
    Rigidbody rb;
    public GameObject Flap1; //right
    public float force;
    public GameObject Flap2; //left
    public GameObject ball;
    public float FlapSpeed;
    public float RespawnSpeed;
    public bool RightFlap;
    public bool LeftFlap;
    public bool RightFlapEnd;
    public bool LeftFlapEnd;
    public bool Respawning;
    public bool Respawning2;
    public GameObject transformFirst;

    public GameObject transformSecond;
    public GameObject GameOver;
    public int Lives = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

    }
    void Start()
    {
        score = 0f;
        Respawning = false;
        rb = ball.GetComponent<Rigidbody>();
        GameOver.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Flap();
        FlapAct();
        if (ball.transform.position.y <= -18.1f)
        {
            if (Lives == 0 && !Respawning && !Respawning2)
            {
                GameOver.SetActive(true);
            } else if (Lives > 0 && !Respawning && !Respawning2)
            {
                Lives -= 1;
            }
            Respawning = true;

            //Vector3 tempVect = ball.transform.position;
            //Vector3 NewVect = new Vector3(tempVect.x, 10.18f, tempVect.z);

        }
        if (Respawning && rightCoroutine == null)
        {
            rightCoroutine = StartCoroutine(moveObjectRight());


        }
        else if (Respawning2 && upCoroutine == null)
        
        {
            upCoroutine = StartCoroutine(moveObjectUp());

        }

        
    }

    void Flap()
    {
        if ((Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.leftShiftKey.wasPressedThisFrame) && !LeftFlap && !LeftFlapEnd)
        {
            Debug.Log("left Flap Activated");
            LeftFlap = true;
        }        
        if ((Mouse.current.rightButton.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame) && !RightFlap && !RightFlapEnd)
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
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, -90.0f);
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
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 0.0f);
            Flap1.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                RightFlapEnd = false;
            }
        }
        if (LeftFlap)
        {
            Quaternion currentRot = Flap2.transform.rotation;
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 90f);
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
            Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 0.0f);
            Flap2.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
            if (currentRot == targetRot)
            {
                LeftFlapEnd = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            Debug.Log("Bounce!");
        }
    }
    public IEnumerator moveObjectRight()
    {
        Vector3 Destination = transformSecond.transform.position;
        Vector3 Origin = ball.transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(ball.transform.localPosition, Destination) > -18.1f)
        {
            CurrentTime += Time.deltaTime;
            ball.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / RespawnSpeed);
            yield return null;
;
        }
        Debug.Log("Ended First Coroutine");
        Respawning2 = true;
        Respawning = false;
        rightCoroutine = null;
        yield return null;
    }
    public IEnumerator moveObjectUp()
    {
        Vector3 Destination = transformSecond.transform.position;
        Vector3 Origin = ball.transform.position;
        float CurrentTime = 0f;
        while (Vector3.Distance(ball.transform.localPosition, Destination) > -18.1f)
        {
            Debug.Log("3");

            CurrentTime += Time.deltaTime;
            ball.transform.localPosition = Vector3.Lerp(Origin, Destination, CurrentTime / RespawnSpeed);
            yield return null;
        }
        Debug.Log("Ended Second Coroutine");
        Respawning2 = false;
        Respawning = false;
        upCoroutine = null;
        StopCoroutine(moveObjectUp());
        rb.AddForce(force,0,0);
        yield return null;
    }
    
    
}
