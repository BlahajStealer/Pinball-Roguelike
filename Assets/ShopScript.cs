using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;

public class ShopScript : MonoBehaviour
{
    public float Money;
    public TextMeshProUGUI MoneyText;
    public GameObject Univ;
    UniversalScript us;
    public Sprite[] Badges;
    public Sprite[] BallMods;
    public Sprite[] MachineMods;
    public Button[] BadgeButtons;
    public Button[] BallButtons;
    public Button[] MachineModsButton;
    public bool shopMoneyStart;
    public bool shopMoneyStarted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        us = Univ.GetComponent<UniversalScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Badges.Length; i++)
        {
            BadgeButtons[i].image.sprite = Badges[Random.Range(0,Badges.Length)];
        }
        if (shopMoneyStart && !shopMoneyStarted)
        {
            Money += us.score/100;
            shopMoneyStart = false;
            shopMoneyStarted = true;
        }
        MoneyText.text = "Money: " + Money.ToString();

    }
}
