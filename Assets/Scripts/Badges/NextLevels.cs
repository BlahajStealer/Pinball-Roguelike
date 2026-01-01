using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public class NextLevels : MonoBehaviour
{
    public bool[] completedLevels;
    public GameObject[] Starts;
    public TextMeshProUGUI[] StartsText;
    public GameObject ShopScriptObj;
    public GameObject BossManObj;
    public GameObject UniversalObj;
    public GameObject self;
    public TextMeshProUGUI[] Scores;
    public string[] BossTexts;
    public TextMeshProUGUI BossText;
    public Color Gray;
    public Color LesserGray;
    ShopScript ss;
    BossManager bm;
    UniversalScript us;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = UniversalObj.GetComponent<UniversalScript>();
        bm = BossManObj.GetComponent<BossManager>();
        ss = ShopScriptObj.GetComponent<ShopScript>();
        for (int i = 0; i < completedLevels.Length; i++)
        {
            completedLevels[i] = false;
        }

        int[] target = new int[3];

        target[0] = Mathf.RoundToInt(us.target);
        target[1] = Mathf.RoundToInt(us.target * 1.25f);
        target[2] = Mathf.RoundToInt(us.target * (1.25f*1.25f));
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = Mathf.RoundToInt(target[i] / 10);
            if (target[i] % 5 == 0)
            {
                target[i] *= 10;
            } else
            {
                target[i] = Mathf.RoundToInt(target[i] / 5) * 50;
            }
        }
        completedLevels[0] = true;
        for (int i = 0; i < Scores.Length; i++)
        {
            if (bm.RandomNum == 0 && i == 2)
            {
                Scores[i].text = "Score Needed: " + (target[i] * 3).ToString();

            } else
            {
                Scores[i].text = "Score Needed: " + target[i].ToString();
            }
        }
        BossText.text = BossTexts[bm.RandomNum];

    }

    // Update is called once per frame
    void Update()
    {
        if (self.activeSelf == true)
        {
            for (int i = 0; i < completedLevels.Length;i++)
            {
                if (!completedLevels[i])
                {
                    if (i != 0)
                    {
                        if (completedLevels[i-1])
                        {
                            Starts[i].GetComponent<Image>().color = LesserGray;
                            Starts[i].GetComponent<Button>().interactable = true;
                            StartsText[i].text = "Start";

                        }
                        else
                        {
                            Starts[i].GetComponent<Image>().color = Gray;
                            Starts[i].GetComponent<Button>().interactable = false;
                            StartsText[i].text = "Next";


                        }
                    } else
                    {
                        Starts[i].GetComponent<Image>().color = LesserGray;
                        Starts[i].GetComponent<Button>().interactable = true;
                        StartsText[i].text = "Start";

                    }
                } else
                {
                    Starts[i].GetComponent<Image>().color = Gray;
                    Starts[i].GetComponent<Button>().interactable = false;
                    StartsText[i].text = "Complete";

                }
            }
        }
        if (bm.NextLevelRestarter)
        {
            int[] target = new int[3];
            target[0] = Mathf.RoundToInt(us.target * 1.25f);
            target[1] = Mathf.RoundToInt(us.target * 1.25f * 1.25f);
            target[2] = Mathf.RoundToInt(us.target * (1.25f * 1.25f * 1.25f));
            for (int i = 0; i < completedLevels.Length; i++)
            {
                completedLevels[i] = false;
            }
            for (int i = 0; i < target.Length; i++)
            {
                target[i] = Mathf.RoundToInt(target[i] / 10);
                if (target[i] % 5 == 0)
                {
                    target[i] *= 10;
                }
                else
                {
                    target[i] = Mathf.RoundToInt(target[i] / 5) * 50;
                }
            }
            for (int i = 0; i < Scores.Length; i++)
            {
                if (bm.RandomNum == 0 && i == 2)
                {
                    Scores[i].text = "Score Needed: " + (target[i] * 3).ToString();

                }
                else
                {
                    Scores[i].text = "Score Needed: " + target[i].ToString();
                }
            }
            BossText.text = BossTexts[bm.RandomNum];
            bm.NextLevelRestarter = false;
        }
    }

    public void StartNextLevel(int ID)
    {
        if (ss.cutPts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((2 * ss.normalTarget / 3) / 10);

        }
        else if (ss.Halfpts)
        {
            us.AddOnAct();

            us.target = Mathf.RoundToInt((ss.normalTarget / 2) / 10);
        }
        else if (bm.CompletedBosses[0] == 1)
        {
            us.target = Mathf.RoundToInt((ss.normalTarget * 3) / 10);

        }
        else
        {
            us.target = Mathf.RoundToInt((ss.normalTarget) / 10);
        }

        if (us.target % 5 == 0)
        {
            us.target *= 10;

        }
        else
        {
            us.target = Mathf.RoundToInt(us.target / 5) * 5 * 10;
        }
        us.score = 0;
        if (ss.TwoThirdsSell)
        {
            us.score += Mathf.RoundToInt((2 * us.target / 3) / 10) * 10;
            ss.TwoThirdsSell = false;
        }
        completedLevels[ID] = true;

        ss.shopMoneyStarted = false;
        ss.Leaving = true;
        self.SetActive(false);
    }
}
//ss.leaving = true