using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Mono.Cecil.Cil;
public class UniversalScript : MonoBehaviour
{
    [Header("--Ball--")]
    public GameObject ball;
    public float score;
    public float force;
    public int Lives = 1;
    public Material[] ballColors;
    float forceTime = 0;
    bool down;
    bool cooldown;
    double cooldownTimer;
    public float timeMult;
    public TextMeshProUGUI livesCounter;
    public Slider ForceCounterText;

    [Header("--Flaps--")]
    public GameObject Flap1; //right
    public GameObject Flap2; //left
    public bool RightFlap;
    public bool LeftFlap;
    public bool RightFlapEnd;
    public bool LeftFlapEnd;
    public float FlapSpeed;

    [Header("--Respawning--")]
    public float RespawnSpeed;
    public bool Respawning;
    public bool Respawning2;
    [Header("--Positioning--")]
    Vector3 BallStop;
    public GameObject transformFirst;
    [Header("--Objects--")]
    public GameObject ForceCounter;
    public GameObject DDOL;
    public GameObject Shop;
    GameObject BossManager;
    [Header("--Scripts--")]
    ShopScript sp;
    BallScript bs;
    DontDestroyOnLoadScript DDOLS;
    Rigidbody rb;
    BossManager bm;

    [Header("--Camera--")]
    CameraScript cs;

    public GameObject Camera;

    public bool StopFollow;
    [Header("--Goals and Gameover--")]
    public TextMeshProUGUI MoneyDisp;
    public char endlessGoal;
    public GameObject GameOver;
    public GameObject goalTextObj;
    public TextMeshProUGUI goalText;
    public int target = 10000;

    Rigidbody Flap1rb;
    Rigidbody Flap2rb;

    [Header("--AddPts--")]

    public bool AddPtsSold;

    [Header("--Add50--")]

    float actTimer;
    public bool Add50;
    public int AddedPoints;
    void Awake()
    {
        Shop.SetActive(false);
        sp = Shop.GetComponent<ShopScript>();
    }
    void Start()
    {
        BossManager = GameObject.FindGameObjectWithTag("BossMan");
        bm = BossManager.GetComponent<BossManager>();
        Flap1rb = Flap1.GetComponent<Rigidbody>();
        Flap2rb = Flap2.GetComponent<Rigidbody>();

        DDOL = GameObject.FindGameObjectWithTag("DDOL");
        if (DDOL != null)
        {
            DDOLS = DDOL.GetComponent<DontDestroyOnLoadScript>();
            Debug.Log("DDOL is not NUll");
            endlessGoal = DDOLS.goalOrEndless;
            ball.GetComponent<Renderer>().material = ballColors[DDOLS.colorChoiceInt];
            

        } else
        {
            endlessGoal = 'g';
            ball.GetComponent<Renderer>().material = ballColors[6];
        }
        if (endlessGoal == 'g')
        {
            goalText.text = "Goal: " + target;
            goalTextObj.SetActive(true);
        } else
        {
            goalTextObj.SetActive(false);

        }

        cs = Camera.GetComponent<CameraScript>();
        ball.transform.position = transformFirst.transform.position;
        bs = ball.GetComponent<BallScript>();
        score = 0f;
        Respawning = true;
        rb = ball.GetComponent<Rigidbody>();
        GameOver.SetActive(false);
        ForceCounterText.value = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Flap();
        FlapAct();
    }

    void Update()
    {

        if (sp != null)
        {
            MoneyDisp.text = "$" + sp.Money;
        } else
        {
            MoneyDisp.text = "$0";

        }
        actTimer += Time.deltaTime;
        if (actTimer >= 10 && !ForceCounter.activeSelf)
        {
            actTimer = 0;
            if (Add50)
            {
                score += AddedPoints;
            }
            if (bm.CompletedBosses[3] == 1)
            {
                int totalPingys;
                totalPingys = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + 
                    GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length + 
                    GameObject.FindGameObjectsWithTag("AddedPinger").Length;

                Debug.Log(totalPingys);
                totalPingys *= 50;
                if (score > totalPingys)
                {
                    score -= totalPingys;

                } else
                {
                    score = 0;
                }
            }
        } else if (actTimer >= 10 && ForceCounter.activeSelf)
        {
            actTimer = 0;
        }
            goalText.text = "Goal: " + target;


        if (sp.Leaving && AddPtsSold)
        {
            AddPtsSold = false;
            score = Mathf.RoundToInt(target * (2 / 3)/10);
            if (score % 5 == 0)
            {
                score *=10;
            } else
            {
                score =Mathf.RoundToInt(score/5)*50;
            }
            sp.DivisionPts = 2 / 3;
        }
        if (endlessGoal == 'g')
        {


            if (target <= score)
            {
                Shop.SetActive(true);
                rb.linearVelocity = Vector3.zero;
                ball.transform.position = transformFirst.transform.position;
                if (!sp.shopMoneyStarted)
                {
                    sp.shopMoneyStart = true;
                }

            }
            else
            {
                Shop.SetActive(false);
            }
        }
        livesCounter.text = "Lives: " + Lives.ToString();

        if (cooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= 5 || ball.transform.position.y >= (.02 + transformFirst.transform.position.y))
            {
                cooldown = false;
                cooldownTimer = 0;
            }
        }
        if (ball.transform.position.y <= transformFirst.transform.position.y + .1f && !Respawning && !cooldown)
        {
            if (Lives == 0)
            {
                GameOver.SetActive(true);
            }
            else if (Lives > 0)
            {
                Lives -= 1;
                rb.angularVelocity = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                StopFollow = true;
                ball.transform.position = transformFirst.transform.position;


                Respawning = true;

            }

            //Vector3 tempVect = ball.transform.position;
            //Vector3 NewVect = new Vector3(tempVect.x, 10.18f, tempVect.z);

        }
        else if (ball.transform.position.y > transformFirst.transform.position.y)
        {
            Respawning = false;
        }
        bool xAlligned = Mathf.Abs(ball.transform.position.x - transformFirst.transform.position.x) < 0.05f;
        bool yAlligned = Mathf.Abs(ball.transform.position.y - transformFirst.transform.position.y) < 0.05f;
        if (xAlligned && yAlligned)
        {
            ForceCounter.SetActive(true);
            if (forceTime <= 100 && !down)
            {
                forceTime += Time.deltaTime * timeMult;

            }
            else if (forceTime > 100 || down)
            {
                forceTime -= Time.deltaTime * timeMult;
                down = true;
                if (forceTime <= 0)
                {
                    down = false;
                }
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocity = new Vector3(0, forceTime, 0);
                Respawning = false;
                cooldown = true;
                StopFollow = false;

            }
            ForceCounterText.value = forceTime / 100;
        }
        else
        {
            ForceCounter.SetActive(false);

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
                Flap1rb.MoveRotation(Quaternion.Slerp(currentRot, targetRot, FlapSpeed));
                //Flap1.transform.rotation = Quaternion.Slerp(currentRot, targetRot, FlapSpeed);
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
                Flap1rb.MoveRotation(Quaternion.Slerp(currentRot, targetRot, FlapSpeed));
                if (currentRot == targetRot)
                {
                    RightFlapEnd = false;
                }
            }
            if (LeftFlap)
            {
                Quaternion currentRot = Flap2.transform.rotation;
                Quaternion targetRot = Quaternion.Euler(currentRot.x, currentRot.y, 90f);
                Flap2rb.MoveRotation(Quaternion.Slerp(currentRot, targetRot, FlapSpeed));
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
                Flap2rb.MoveRotation(Quaternion.Slerp(currentRot, targetRot, FlapSpeed));
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


    public void AddOnAct()
    {
        if (Add50)
        {
            AddedPoints += 50;
        }
    }

}
