using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
public class UniversalScript : MonoBehaviour
{
    [Header("--Ball--")]
    public float score;
    public float force;
    public int Lives = 1;
    public Material[] ballColors;
    public float forceTime = 0;
    bool down;
    public bool cooldown;
    public double cooldownTimer;
    public float timeMult;
    public TextMeshProUGUI livesCounter;
    public Slider ForceCounterText;
    public GameObject BossRewards;

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
    public DontDestroyOnLoadScript DDOLS;
    Rigidbody rb;
    BossManager bm;

    [Header("--Camera--")]
    CameraScript cs;

    public GameObject Camera;

    public bool StopFollow;
    [Header("--Goals and Gameover--")]
    Vector2 StartPos = new Vector2(2050, 40);
    RectTransform rtShop;
    public GameObject animMan;
    public TextMeshProUGUI MoneyDisp;
    public char endlessGoal;
    public GameObject GameOver;
    public GameObject goalTextObj;
    public TextMeshProUGUI goalText;
    public int target = 10000;
    public int StartingPinger;

    Rigidbody Flap1rb;
    Rigidbody Flap2rb;

    [Header("--AddPts--")]

    public bool AddPtsSold;

    [Header("--Add50--")]

    float actTimer;
    public bool Add50;
    public int AddedPoints;

    [Header("GoldPerc")]
    public float Division = 0;

    [Header("--GeneralBadges--")]
    public int Addition;
    public float multiplication;

    public bool AllNormalPingers;
    public int hitc;
    public bool JustHit;
    public bool Every50Norms;
    public bool gPinger100;
    public bool hits15;
    public int hitc15;
    public bool Remove100Pinger;
    public bool hits;
        void Awake()
    {
        Shop.SetActive(false);
        sp = Shop.GetComponent<ShopScript>();
    }
    void Start()
    {
        rtShop = animMan.GetComponent<RectTransform>();

        StartingPinger = GameObject.FindGameObjectsWithTag("Pingy Thing").Length;
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
            
            

        } else
        {
            endlessGoal = 'g';
            
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
        
        score = 0f;
        Respawning = true;
        GameOver.SetActive(false);
        ForceCounterText.value = 0;
        multiplication = 1;
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
            if (Division != 0)
            {
                float mult = Division + 1;
                score *= mult;
                score = Mathf.Round(score/10);
                if (score % 5 == 0)
                {
                    score *= 10;
                }
                else
                {
                    score = Mathf.RoundToInt(score / 5) * 50;
                }
            }
            score += Addition;
            score *= multiplication;



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
                cs.doingAnims = true;
                Shop.SetActive(true);
                if (!sp.shopMoneyStarted)
                {
                    sp.shopMoneyStart = true;
                }

            }
            else
            {
                rtShop.anchoredPosition = StartPos;

                Shop.SetActive(false);
            }
        }
        livesCounter.text = "Lives: " + Lives.ToString();



        
        if (Respawning)
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
