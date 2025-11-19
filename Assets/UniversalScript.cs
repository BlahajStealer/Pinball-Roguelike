using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalScript : MonoBehaviour
{

    public GameObject Flap1;
    public GameObject Flap2;
    public float FlapSpeed;
    bool RightFlap;
    bool LeftFlap;
    bool RightFlapEnd;
    bool LeftFlapEnd;
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
    
}
