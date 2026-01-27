using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ShopScript : MonoBehaviour
{
    public Sprite Transparent;
    public idDeterminer IdDet;
    public TextMeshProUGUI MoneyText;



    public bool shopMoneyStart;
    public bool shopMoneyStarted;
    public int[] BadgeArray;

    public TextMeshProUGUI NewScore;
    public Sprite outOfStock;
    public bool cutPts;
    public bool Leaving;
    public float normalTarget;
    public Image[] Swap;
    public Sprite[] IDSprites;

    public bool inShop;
    [Header("--Shop--")]
    public float Money;

    [Header("--GameObjects--")]
    GameObject BossManager;
    public GameObject SellConsume;
    public GameObject SellButton;
    public GameObject[] Photos;
    public GameObject Shop;
    public GameObject Univ;
    public GameObject Camera;
    public GameObject NextLevelObj;
    public GameObject Ball;
    public GameObject Canvas;
    [Header("--Scripts--")]
    BossManager bm;
    UniversalScript us;
    CameraScript cs;
    NextLevels nl;
    BallScript bs;
    ShopAnimations sa;
    [Header("--Descriptions--")]
    public TextMeshProUGUI Description;
    public string[] Descriptions;
    public GameObject DescriptionObj;

    [Header("--DescriptionsConsumes--")]
    public string[] ConsumeDescription;
    public bool[] ConsumeBools;

    [Header("--Badges--")]
    public int[] BadgeBuyValue;
    public int[] BadgeSellValue;
    public bool[] BadgeBools; 
    public Sprite[] Badges;
    public Button[] BadgeButtons;
    RectTransform rt;
    public int[] currentBadgeIDs;
    public TextMeshProUGUI[] BadgeSellTexts;
   
    [Header("--Consumables--")]
    public int[] ConsumeBuyValue;
    public int[] ConsumeSellValue;
    public GameObject[] Consumables;
    public Sprite[] IDConsumables;
    public Image[] ConsumableSpots;
    public Sprite[] ConsumeSprites;
    public int[] MachineArray;
    public RectTransform rtc;
    public Sprite[] MachineMods;
    public Button[] MachineModsButton;
    public int[] currentConsumeIDs;
    public TextMeshProUGUI[] ConsumeSellTexts;

    [Header("--BadgeSpecifics--")]
    public bool Percent20;
    public float NumberBadges = 1;
    public bool TwoThirdsSell;
    public bool Halfpts;
    public float DivisionPts = 0;
    public bool PercChanged;
    public bool AllGoldDivide;
    public bool OneRandomActive;
    public bool BadgeChanged;

    [Header("--NextLevel--")]
    public GameObject NextLevels;
    public float backup;
    public bool BadgeBoughtNext = false;
    public bool noTab;
    private void Awake()
    {
    }

    void Start()
    {
        bs = Ball.GetComponent<BallScript>();
        NextLevels.SetActive(false);
        sa = Shop.GetComponent<ShopAnimations>();
        nl = NextLevelObj.GetComponent<NextLevels>();
        currentBadgeIDs = new int[5];
        currentConsumeIDs = new int[2];
        for (int i = 0; i < currentBadgeIDs.Length; i++)
        {
            currentBadgeIDs[i] = 7;
        }        
        for (int i = 0; i < currentConsumeIDs.Length; i++)
        {
            currentConsumeIDs[i] = 7;
        }

        BossManager = GameObject.FindGameObjectWithTag("BossMan");
        bm = BossManager.GetComponent<BossManager>();
        BadgeBools = new bool[3];
        ConsumeBools = new bool[5];
        DescriptionObj.SetActive(false);
        cs = Camera.GetComponent<CameraScript>();
        SellConsume.SetActive(false);
        DivisionPts = 0;
        IdDet = GetComponent<idDeterminer>();
        us = Univ.GetComponent<UniversalScript>();

        BadgeArray = new int[BadgeButtons.Length];
        MachineArray = new int[MachineModsButton.Length];
        SellButton.SetActive(false);
        rt = SellButton.GetComponent<RectTransform>();
        rtc = SellConsume.GetComponent<RectTransform>();
        normalTarget = 5000;

    }

    // Update is called once per frame
    void Update()
    {

        Leaving = false;


        OnStart();
        NotInShop();
        PointMods();


        MoneyText.text = "Money: " + Money.ToString();

    }
    void OnStart()
    {
        if (shopMoneyStart && !shopMoneyStarted)
        {
            noTab = true;
            Canvas.SetActive(false);
            StartCoroutine(cs.ShopMove());
            StartCoroutine(sa.ShopAnim());
            bm.Round++;
            for (int i = 0; i < BadgeBools.Length; i++)
            {
                BadgeBools[i] = true;
            }
            for (int i = 0; i < ConsumeBools.Length; i++)
            {
                ConsumeBools[i] = true;
            }
            if (Percent20)
            {
                Money = Mathf.RoundToInt(Money * 1.2f);
                us.AddOnAct();

            }
            Money = Mathf.RoundToInt(Money * NumberBadges);
            Money += Mathf.RoundToInt((10 - DivisionPts));
            if (nl.DiffMult == 0)
            {
                normalTarget = Mathf.RoundToInt((normalTarget * backup) / 10);
                Debug.Log("Defaulted");
            }
            else
            {
                Debug.Log("Didn't Default");
                normalTarget = Mathf.RoundToInt((normalTarget * nl.DiffMult) / 10);

            }
            Debug.Log(normalTarget);
            if (normalTarget % 5 == 0)
            {
                normalTarget *= 10;
            }
            else
            {
                normalTarget = (Mathf.RoundToInt(normalTarget / 5) * 5) * 10;
            }
            us.Lives = 4;
            NumberBadges = 1;
            inShop = true;
            shopMoneyStart = false;
            for (int n = 0; n < IDSprites.Length; n++)
            {

                IDSprites[n] = null;

            }
            for (int n = 0; n < IDConsumables.Length; n++)
            {
                IDConsumables[n] = null;
            }
            shopMoneyStarted = true;
            int[] TakenBadges = new int[3];
            for (int i = 0; i < BadgeButtons.Length; i++)
            {
                int RandomInt = Random.Range(0, Badges.Length);
                for (int k = 0; k < TakenBadges.Length; k++)
                {
                    while (TakenBadges[0] == RandomInt || TakenBadges[1] == RandomInt || TakenBadges[2] == RandomInt)
                    {
                        RandomInt = Random.Range(0, Badges.Length);
                    }
                }
                TakenBadges[i] = RandomInt;
                BadgeButtons[i].image.sprite = Badges[RandomInt];
                BadgeArray[i] = RandomInt;
                for (int n = 0; n < IDSprites.Length; n++)
                {
                    if (IDSprites[n] == null)
                    {
                        IDSprites[n] = BadgeButtons[i].image.sprite;
                        break;
                    }
                }

            }


            for (int i = 0; i < MachineModsButton.Length; i++)
            {
                int RandomInt = Random.Range(0, MachineMods.Length);
                MachineModsButton[i].image.sprite = MachineMods[RandomInt];
                MachineArray[i] = RandomInt;
                for (int n = 0; n < IDConsumables.Length; n++)
                {
                    if (IDConsumables[n] == null)
                    {
                        IDConsumables[n] = MachineModsButton[i].image.sprite;
                        break;
                    }
                }
            }
            for (int i = 0; i < BadgeSellTexts.Length; i++)
            {
                BadgeSellTexts[i].text = "$" + BadgeBuyValue[BadgeArray[i]];
            }
            for (int i = 0; i < ConsumeSellTexts.Length; i++)
            {
                ConsumeSellTexts[i].text = "$" + ConsumeBuyValue[MachineArray[i]];
                Debug.Log(ConsumeBuyValue[MachineArray[i]]);
            }
        }

    }
    void NotInShop()
    {
        if (!inShop)
        {
            if (TwoThirdsSell)
            {
                us.score += Mathf.RoundToInt((2 * us.target / 3) / 10);
                if (us.score % 5 == 0)
                {
                    us.score *= 10;
                }
                else
                {
                    us.score = (Mathf.RoundToInt(us.score / 5) * 5) * 10;
                }
                TwoThirdsSell = false;
            }
        }
    }
    void PointMods()
    {
        if (cutPts)
        {
            float Next;
            Next = Mathf.RoundToInt((2 * normalTarget / 3) / 10);
            if (Next % 5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt((2 * normalTarget / 3) / 10) * 10) + " Points Needed";

            }
            else
            {
                NewScore.text = Mathf.RoundToInt(Next / 5) * 5 * 10 + " Points Needed";
            }
        }
        else if (Halfpts)
        {
            float Next;
            Next = Mathf.RoundToInt((normalTarget / 2) / 10);
            if (Next % 5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt((normalTarget / 2) / 10) * 10) + " Points Needed";

            }
            else
            {
                NewScore.text = Mathf.RoundToInt(Next / 5) * 5 * 10 + " Points Needed";
            }
        }
        else if (bm.CompletedBosses[0] == 1)
        {
            float Next;
            Next = Mathf.RoundToInt(normalTarget * 3 / 10);
            if (Next % 5 == 0)
            {
                Next *= 10;
            }
            else
            {
                Next = Mathf.RoundToInt(Next / 5) * 50;

            }
            NewScore.text = Next + " Points Needed";

        } else if (AllGoldDivide)
        {
            float Next;
            Next = Mathf.RoundToInt(normalTarget * 0.75f / 10);
            if (Next % 5 == 0)
            {
                Next *= 10;
            }
            else
            {
                Next = Mathf.RoundToInt(Next / 5) * 50;

            }
            NewScore.text = Next + " Points Needed";
        }
        else
        {
            float Next;
            Next = Mathf.RoundToInt((normalTarget) / 10);
            if (Next % 5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt(normalTarget / 10) * 10) + " Points Needed";

            }
            else
            {
                NewScore.text = Mathf.RoundToInt(Next / 5) * 5 * 10 + " Points Needed";
            }
        }
    }
    public void BadgeButtonHit(int ID)
    {

        if ((BadgeButtons[ID].image.sprite != outOfStock))
        {
            BadgeBoughtNext = true;
            IdDet.Badge(BadgeArray[ID], ID);
            BadgeBools[ID] = false;
        }

    }
    public void MachineButtonHit(int ID)
    {
        if (MachineModsButton[ID].image.sprite != outOfStock)
        {
            IdDet.Machine(MachineArray[ID], ID);
            ConsumeBools[ID] = false;
        }


    }
    public void Leave()
    {
        StopAllCoroutines();
        NextLevels.SetActive(true);
        StartCoroutine(sa.NextLevelAnim());
        StartCoroutine(cs.moveCameraThree());
        StartCoroutine(cs.ResetRot());
    }

    public void Sell(int ID)
    {
        
        if (Photos[ID] != null)
        {
            int Y;
            switch (ID)
            {
                case 0:
                    Y = 287;
            Debug.Log("287");

                    break;
                case 1:
                    Y = 137;
            Debug.Log("137");

                    break;
                case 2:
                    Y = -13;
            Debug.Log("-13");

                    break;
                case 3:
                    Y = -163;
            Debug.Log("-163");

                    break;
                case 4:
                    Y = -313;
            Debug.Log("-313");

                    break;
                default:
                    Y = 287;
            Debug.Log("Defaulted");

                    break;
            }
            Y += 37;
            rt.anchoredPosition = new Vector2(98, Y);
            SellButton.SetActive(true);

        }

    }
    public void SellButtonFunc()
    {
        float y;
        y = rt.anchoredPosition.y;
        int ID;
        switch (y)
        {
            case 287+37:
                ID = 0;
                break;
            case 137 + 37:
                ID = 1;
                break;
            case -13 + 37:
                ID = 2;
                break;
            case -163 + 37:
                ID = 3;
                break;
            case -313 + 37:
                ID = 4;
                break;
            default:
                ID = 0;
                break;
        }
        Money += BadgeSellValue[currentBadgeIDs[ID]];
        currentBadgeIDs[ID] = 7;
        Swap[ID].sprite = Transparent;
        if (Photos[ID].TryGetComponent<AddPts>(out _))
        {
            TwoThirdsSell = true;
        } else if (Photos[ID].TryGetComponent<CutPoints>(out _))
        {
            cutPts = false;

        } else if (Photos[ID].TryGetComponent<HalfPtsMny>(out _))
        {
            DivisionPts -= 5f;
            Halfpts = false;
        } else if (Photos[ID].TryGetComponent<EveryGoldPinger>(out _))
        {
            bs.gPinger100 = false;

        } else if (Photos[ID].TryGetComponent<goldperc>(out _))
        {
            us.Division = 0;
        } else if (Photos[ID].TryGetComponent<Every2Gold>(out _))
        {
            DivisionPts -= 2;

        } else if (Photos[ID].TryGetComponent<AllGold>(out _))
        {
            AllGoldDivide = false;
        } else if (Photos[ID].TryGetComponent<Every4>(out _))
        {
            us.Addition -= Photos[ID].GetComponent<Every4>().amountToAdd;
        } else if (Photos[ID].TryGetComponent<AllNormal>(out _)) {
            bs.AllNormalPingers = false;
        } else if (Photos[ID].TryGetComponent<Every50>(out _))
        {
            bs.AllNormalPingers = false;
        } else if (Photos[ID].TryGetComponent<Add500>(out _))
        {
            us.Addition -= Photos[ID].GetComponent<Add500>().Add;
        } else if (Photos[ID].TryGetComponent<Remove100>(out _))
        {
            bs.Remove100Pinger = false;
        } else if (Photos[ID].TryGetComponent<PingerDestroyMoney>(out _))
        {
            cs.DestroyPinger = false;
        } else if (Photos[ID].TryGetComponent<OneRandom>(out _))
        {
            OneRandomActive = false;
        } else if (Photos[ID].TryGetComponent<Every10>(out _))
        {
            us.Addition -= Mathf.RoundToInt(Photos[ID].GetComponent<Every10>().total);
        } else if (Photos[ID].TryGetComponent<MoneyPoints>(out _))
        {
            us.Addition -= Mathf.RoundToInt(Photos[ID].GetComponent<MoneyPoints>().lastAdd);
        } else if (Photos[ID].TryGetComponent<Money15>(out _))
        {
            bs.hitc15 = 0;
            bs.hits15 = false;
        }


            Destroy(Photos[ID]);
        Photos[ID] = null;
        SellButton.SetActive(false);
    }

    public void SellConsumable()
    {
        float y;
        y = rtc.anchoredPosition.y;
        int ID = 0;
        if (y == 180)
        {
            ID = 0;
        }
        else if (y == -120)
        {
            ID = 1;

        }

        Money += ConsumeSellValue[currentConsumeIDs[ID]];
        currentConsumeIDs[ID] = 7;

        ConsumableSpots[ID].sprite = Transparent;
        Destroy(Consumables[ID]);
        Consumables[ID] = null;
        SellConsume.SetActive(false);
    }
    public void UseConsume()
    {
        Image Parent;
        if (rtc.anchoredPosition.y == 180)
        {
            Parent = GameObject.FindGameObjectWithTag("First Image").GetComponent<Image>();
            currentConsumeIDs[0] = 7;

        }
        else
        {
            Parent = GameObject.FindGameObjectWithTag("Second Image").GetComponent<Image>();
            currentConsumeIDs[0] = 7;

        }
        if (Parent.sprite == ConsumeSprites[0])
        {
            cs.GoldPinger(0);
        } else if (Parent.sprite == ConsumeSprites[1])
        {
            cs.AddPinger(0);
        } else if (Parent.sprite == ConsumeSprites[2])
        {
            cs.RemovePinger(0);
        }


    }


    public void ConsumableButtons(int ID)
    {
        if (Consumables[ID] != null)
        {
            int y = 180;
            if (ID == 0)
            {
                y = 180;
            } else if (ID == 1)
            {
                y = -120;
            }
            
            rtc.anchoredPosition = new Vector2(759, y);
            SellConsume.SetActive(true);
            
        }

    }
    public void ExitSell()
    {
        SellButton.SetActive(false);
    }
    public void ExitConsume()
    {
        SellConsume.SetActive(false);
        cs.GoldPinger(1);
        cs.AddPinger(1);
        cs.RemovePinger(1);

    }
}














