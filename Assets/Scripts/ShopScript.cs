using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class ShopScript : MonoBehaviour
{
    public GameObject Camera;
    CameraScript cs;
    public Sprite Transparent;
    public idDeterminer IdDet;
    public float Money;
    public TextMeshProUGUI MoneyText;
    public GameObject Univ;
    UniversalScript us;
    public Sprite[] Badges;
    public Sprite[] MachineMods;
    public Button[] BadgeButtons;
    public Button[] MachineModsButton;
    public bool shopMoneyStart;
    public bool shopMoneyStarted;
    public int[] BadgeArray;
    public int[] MachineArray;
    public TextMeshProUGUI NewScore;
    public GameObject Shop;
    public Sprite outOfStock;
    public bool cutPts;
    public bool Leaving;
    public float DivisionPts = 1;
    public bool Halfpts;
    public float normalTarget;
    public GameObject[] Photos;
    public Image[] Swap;
    public Sprite[] IDSprites;
    public float NumberBadges = 1;
    public GameObject SellButton;
    RectTransform rt;
    public RectTransform rtc;
    public bool TwoThirdsSell;
    bool inShop;
    public bool Percent20;
    public Sprite[] IDConsumables;
    public Image[] ConsumableSpots;
    public GameObject[] Consumables;
    public Sprite[] ConsumeSprites;
    public GameObject SellConsume;

    public TextMeshProUGUI Description;
    public string[] Descriptions;
    public GameObject DescriptionObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DescriptionObj.SetActive(false);
        cs = Camera.GetComponent<CameraScript>();
        SellConsume.SetActive(false);
        DivisionPts = 1;
        IdDet = GetComponent<idDeterminer>();
        us = Univ.GetComponent<UniversalScript>();

        BadgeArray = new int[BadgeButtons.Length];
        MachineArray = new int[MachineModsButton.Length];
        SellButton.SetActive(false);
        rt = SellButton.GetComponent<RectTransform>();
        rtc = SellConsume.GetComponent<RectTransform>();
        normalTarget = us.target;

    }

    // Update is called once per frame
    void Update()
    {
        Leaving = false;


        if (shopMoneyStart && !shopMoneyStarted)
        {
            if (Percent20)
            {
                Money = Mathf.RoundToInt(Money * 1.2f);
                us.AddOnAct();

            }
            Money = Mathf.RoundToInt(Money * NumberBadges);
            Money += Mathf.RoundToInt((10/DivisionPts));
            normalTarget = Mathf.RoundToInt((normalTarget * 1.25f)/10);
            if (normalTarget % 5 == 0)
            {
                normalTarget*=10;
            } else
            {
                normalTarget = (Mathf.RoundToInt(normalTarget/5)*5)*10;
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
            for (int i = 0; i < BadgeButtons.Length; i++)
            {
                int RandomInt = Random.Range(0,Badges.Length);
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
                int RandomInt = Random.Range(0,MachineMods.Length);
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
        }
        if (!inShop)
        {
            if (TwoThirdsSell)
            {
                us.score += Mathf.RoundToInt((2 * us.target / 3)/10);
                if (us.score % 5 == 0)
                {
                    us.score*=10;
                } else
                {
                    us.score = (Mathf.RoundToInt(us.score/5)*5)*10;
                }
                TwoThirdsSell = false;
            }
        }

        if (cutPts)
        {
            float Next;
            Next = Mathf.RoundToInt((2*normalTarget/3)/10);
            if (Next%5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt((2*normalTarget/3)/10)*10) + " Points Needed";

            } else
            {
                NewScore.text = Mathf.RoundToInt(Next/5)*5*10 + " Points Needed";
            }
        } else if (Halfpts)
        {
            float Next;
            Next = Mathf.RoundToInt((normalTarget/2)/10);
            if (Next%5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt((normalTarget/2)/10)*10) + " Points Needed";

            } else
            {
                NewScore.text = Mathf.RoundToInt(Next/5)*5*10 + " Points Needed";
            }
        } else
        {
            float Next;
            Next = Mathf.RoundToInt((normalTarget)/10);
            if (Next%5 == 0)
            {
                NewScore.text = (Mathf.RoundToInt(normalTarget/10)*10) + " Points Needed";

            } else
            {
                NewScore.text = Mathf.RoundToInt(Next/5)*5*10 + " Points Needed";
            }
        }

        MoneyText.text = "Money: " + Money.ToString();

    }
    public void BadgeButtonHit(int ID)
    {

        if ((BadgeButtons[ID].image.sprite != outOfStock))
        {
            IdDet.Badge(BadgeArray[ID], ID);
        }

    }
    public void MachineButtonHit(int ID)
    {
        if (MachineModsButton[ID].image.sprite != outOfStock)
        {
            IdDet.Machine(MachineArray[ID], ID);
        }


    }
    public void Leave()
    {

        if (cutPts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((2*normalTarget/3)/10);

        } else if (Halfpts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((normalTarget/2)/10);
        } else
        {
            us.target = Mathf.RoundToInt((normalTarget)/10);
        }

        if (us.target%5 == 0)
        {
            us.target *= 10;

        } else
        {
            us.target = Mathf.RoundToInt(us.target/5)*5*10;
        }
        us.score = 0;
        if (TwoThirdsSell)
        {
            us.score += Mathf.RoundToInt((2 * us.target / 3)/10)*10;
            TwoThirdsSell = false;
        }
        Shop.SetActive(false);
        shopMoneyStarted = false;
        Leaving = true;
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
        Swap[ID].sprite = Transparent;
        switch (Photos[ID].tag)
        {
            case "AddPts":
                DivisionPts = 1;
                TwoThirdsSell = true;
                break;
            case "CutPoints":
                cutPts = false;
                break;
            case "HalfPointsMoney":
                DivisionPts = 1f;
                Halfpts = false;
                break;
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
        }
        else
        {
            Parent = GameObject.FindGameObjectWithTag("Second Image").GetComponent<Image>();
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














