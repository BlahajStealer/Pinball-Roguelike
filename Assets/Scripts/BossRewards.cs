using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework.Internal.Commands;
public class BosssRewards : MonoBehaviour
{
    public RawImage Opt1;
    public TextMeshProUGUI text;
    public RawImage Opt2;
    bool started;
    public GameObject self;
    public GameObject Shop;          
    ShopAnimations sa;    
    public Texture[] Prizes;
    public GameObject descriptionObj;
    public string[] descs;
    public int random1;
    public int random2;
    public GameObject GameObj;
    UniversalScript us;
    public TextMeshProUGUI[] LevelDisp;
    public int[] Levels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = 0;
        }
        GameObj = GameObject.FindGameObjectWithTag("Empty");
        us = GameObj.GetComponent<UniversalScript>();
        started = false;
        sa = Shop.GetComponent<ShopAnimations>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (self.activeSelf == true)
        {
            if (!started)
            {
                started=true;
                random1 = Random.Range(0, Prizes.Length);
                random2 = Random.Range(0, Prizes.Length);
                Debug.Log("Prizes.Length: " + (Prizes.Length-1));
                if (random1 == random2)
                {
                    if (random1 == Prizes.Length-1)
                    {
                        random2--;
                    } else
                    {
                        random2++;
                    }
                }
                Opt1.texture = Prizes[random1];
                LevelDisp[0].text = "Level: " + Levels[random1].ToString();
                Opt2.texture = Prizes[random2];
                LevelDisp[1].text = "Level: " + Levels[random2].ToString();

            }
        }
    }

    public void continueReward()
    {
        StopAllCoroutines();
        StartCoroutine(sa.ShopAnim());
        
        started = false;

    }
    public void Chosen(int placement)
    {
        if (placement == 1)
        {
            Levels[random1]++;
            if (random1 == 2)
            {
                us.Lives++;
            }
        } else
        {
            Levels[random2]++;
            if (random2 == 2)
            {
                us.Lives++;
            }
        }
        continueReward();

    }
}
