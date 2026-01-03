using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossManager : MonoBehaviour
{
    [Header("--GameObjects--")]
    GameObject UniversalScriptObj;
    public GameObject Shop;
    [Header("--Scripts--")]
    UniversalScript us;
    ShopScript ss;
    [Header("--RoundManger--")]
    public int Round;
    bool BossActive;
    [Header("--BossDeterminer--")]
    public int[] CompletedBosses;
    public int RandomNum;

    [Header("--BossVariables--")]
    bool oneStarted;
    bool twoStarted;
    public GameObject[] FakePingers;
    public GameObject[] GoldPingers;
    public GameObject[] Badges;
    bool End6;
    bool NotChanged;
    int EvilPinger1;
    int EvilPinger2;
    public Material red;
    public Material white;
    GameObject FirstEvil;
    GameObject SecondEvil;
    public GameObject BossWin;
    BosssRewards BR;
    public bool NextLevelRestarter = false;
    public bool randomGen = false;
    int tries;



    //makes sure first bools

    bool Boss1 = true;
    bool Boss2 = true;
    bool Boss5 = true;
    bool Boss52 = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        BossWin.SetActive(false);

    }
    void Start()
    {
        End6 = false;
        oneStarted = false;
        twoStarted = false;
        Round = 1;
        UniversalScriptObj = GameObject.FindGameObjectWithTag("Empty");
        us = UniversalScriptObj.GetComponent<UniversalScript>();
        ss = Shop.GetComponent<ShopScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Round % 3 == 1 && !randomGen)
        {
            RandomNum = Random.Range(0, CompletedBosses.Length);
            if (CompletedBosses[RandomNum] != 2)
            {
                randomGen = true;
            } else
            {
                tries++;
            }
            if (tries >= 100)
            {
                for (int i = 0; i < CompletedBosses.Length; i++)
                {
                    if (CompletedBosses[i] == 0)
                    {
                        RandomNum = i;
                    } else if (i == CompletedBosses.Length - 1)
                    {
                    }
                }
                tries = 0;
            }
        }
        Badges = ss.Photos;
        if (Round % 3 == 0 && !BossActive)
        {
            Debug.Log("Boss Round");
            BossActive = true;
            if (CompletedBosses[RandomNum] != 2)
            {
                CompletedBosses[RandomNum] = 1;
                if (RandomNum == 4)
                {
                    NotChanged = true;
                }
            } else
            {
                BossActive = false;

            }


        } else if (Round % 3 != 0 && BossActive && CompletedBosses[6] != 1)
        {
            BossActive = false;
            CompletedBosses[RandomNum] = 2;
            BossWin.SetActive(true);
            NextLevelRestarter = true;
            randomGen = false;
        }
        if (CompletedBosses[1] == 1 && !oneStarted)
        {
            oneStarted = true;
            if (Boss1)
            {
                FakePingers = new GameObject[GameObject.FindGameObjectsWithTag("AddedPinger").Length];
                FakePingers = GameObject.FindGameObjectsWithTag("AddedPinger");
                Boss1 = false;
            }

            for (int i = 0;i < FakePingers.Length;i++)
            {
                FakePingers[i].SetActive(false);
            }
        } else if (CompletedBosses[1] == 2)
        {
            for (int i = 0; i < FakePingers.Length; i++)
            {
                FakePingers[i].SetActive(true);
            }
        }
        if (CompletedBosses[2] == 1 && !twoStarted)
        {
            oneStarted = true;
            if (Boss2)
            {
                GoldPingers = new GameObject[GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length];
                GoldPingers = GameObject.FindGameObjectsWithTag("Gold Pingy Thing");
                Boss2 = false;
            }

            for (int i = 0; i < GoldPingers.Length; i++)
            {
                GoldPingers[i].SetActive(false);
            }
        }
        else if (CompletedBosses[2] == 2)
        {
            for (int i = 0; i < GoldPingers.Length; i++)
            {
                
                GoldPingers[i].SetActive(true);
            }
        }
        if (CompletedBosses[5] == 1 && us.score < (1f/3f * us.target))
        {
            Debug.Log("Hiya");
            for (int i = 0; i < Badges.Length; i++)
            {
                if (Badges[i] != null)
                {
                    Badges[i].SetActive(false);
                    Debug.Log("Hiya");

                }
            }
        } else if (us.score > (1f/3f * us.target) && CompletedBosses[5] == 1)
        {
            for (int i = 0; i < Badges.Length; i++)
            {
                if (Badges[i] != null)
                {
                    Badges[i].SetActive(true);
                    Debug.Log("Hiya2");

                }

            }
        }
        if (CompletedBosses[6] == 1 && Round % 3 != 0)
        {
            Debug.Log("Hello");
            if (ss.inShop && !End6)
            {
                int totalSellValue = 0;
                for (int i = 0; i < ss.currentBadgeIDs.Length; i++)
                {
                    if (ss.currentBadgeIDs[i] != 7)
                    {
                        totalSellValue += ss.BadgeSellValue[ss.currentBadgeIDs[i]];

                    }
                }
                if ((ss.Money - totalSellValue) <= 0)
                {
                    End6 = true;
                    ss.Money = 0;
                } else
                {
                    End6 = true;
                    Debug.Log("Total Sell Value was" + totalSellValue + " The amount now should be: " + (ss.Money - totalSellValue) + " The current amount of money is: " + ss.Money);
                    ss.Money -= totalSellValue;
                }
            } else if (End6)
            {
                BossActive = false;
                CompletedBosses[RandomNum] = 2;
                BossWin.SetActive(true);
                NextLevelRestarter = true;
                randomGen = false;
            }
        }
        if (CompletedBosses[4] == 1 && NotChanged)
        {
            if (Boss5)
            {

                EvilPinger1 = Random.Range(0, GameObject.FindGameObjectsWithTag("Pingy Thing").Length);

                EvilPinger2 = Random.Range(0, GameObject.FindGameObjectsWithTag("Pingy Thing").Length);
                if (GameObject.FindGameObjectsWithTag("Pingy Thing").Length >= 3)
                {
                    FirstEvil = GameObject.FindGameObjectsWithTag("Pingy Thing")[EvilPinger1];
                    SecondEvil = GameObject.FindGameObjectsWithTag("Pingy Thing")[EvilPinger2];
                }


                Boss5 = false;
            }
            FirstEvil.tag = "Evil Pinger";
            FirstEvil.GetComponentInChildren<Renderer>().material = red;
            SecondEvil.tag = "Evil Pinger";
            SecondEvil.GetComponentInChildren<Renderer>().material = red;

            NotChanged = false;
        } if (CompletedBosses[4] == 2 && Boss52)
        {

            if (FirstEvil != null)
            {

                FirstEvil.tag = "Pingy Thing";
                FirstEvil.GetComponentInChildren<Renderer>().material = white;
            }
            if (SecondEvil != null)
            {

                SecondEvil.tag = "Pingy Thing";
                SecondEvil.GetComponentInChildren<Renderer>().material = white;
            }
            Boss52 = false;


        }

    }
    
}
/*

1. 3x Score - done                                                                          Works
2. Remove all Added pingers for this round - done                                           Works
3. Remove all Gold Pingers- done                                                            Works
4. Lose 50 Points every 10 seconds for every pinger on board - done - nerf -                Works
5. Evil Pingers, lose 100 score                                                             Works
6. All badges null until x score - Close to done, need to finish the rest for it to work    Works
7. Lose half sell value of all badges at end of round - Done                                Works
8. Pingers give half score originally                                                       Works
9. Every time the ball hits a flap lose 50 points
*/