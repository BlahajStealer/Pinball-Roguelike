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
    int RandomNum;

    [Header("--BossVariables--")]
    bool oneStarted;
    bool twoStarted;
    public GameObject[] FakePingers;
    public GameObject[] GoldPingers;
    public GameObject[] Badges;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oneStarted = false;
        twoStarted = false;
        Round = 1;
        UniversalScriptObj = GameObject.FindGameObjectWithTag("Empty");
        us = UniversalScriptObj.GetComponent<UniversalScript>();
        ss = Shop.GetComponent<ShopScript>();
        for (int i = 0; i < CompletedBosses.Length; i++)
        {
            CompletedBosses[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Badges = ss.Photos;
        if (Round % 3 == 0 && !BossActive)
        {
            Debug.Log("Boss Round");
            BossActive = true;
            RandomNum = Random.Range(0, CompletedBosses.Length);
            if (CompletedBosses[RandomNum] != 2)
            {
                CompletedBosses[RandomNum] = 1;
            } else
            {
                BossActive = false;

            }


        } else if (Round % 3 != 0 && BossActive)
        {
            BossActive = false;
            CompletedBosses[RandomNum] = 2;
        }
        if (CompletedBosses[1] == 1 && !oneStarted)
        {
            oneStarted = true;
            FakePingers = new GameObject[GameObject.FindGameObjectsWithTag("AddedPinger").Length];
            FakePingers = GameObject.FindGameObjectsWithTag("AddedPinger");
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
            GoldPingers = new GameObject[GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length];
            GoldPingers = GameObject.FindGameObjectsWithTag("Gold Pingy Thing");
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
        if (CompletedBosses[4] == 1 && us.score < (1f/3f * us.target))
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
        } else if (us.score > (1f/3f * us.target) && CompletedBosses[4] == 1)
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

    }
    
}
/*

1. 3x Score - done
2. Remove all Added pingers for this round - done
3. Remove all Gold Pingers- done
4. Lose 50 Points every 10 seconds for every pinger on board - done - nerf
5. All badges null until x score
6. Lose sell value of all badges at end of round
7. Pingers give half score originally
8. Every time the ball hits a flap lose 50 points
9. Evil Pingers, lose 100 score

*/