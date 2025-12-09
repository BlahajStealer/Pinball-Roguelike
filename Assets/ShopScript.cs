using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Random;

public class ShopScript : MonoBehaviour
{
    public idDeterminer IdDet;
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
    public int[] BadgeArray;
    public int[] BallArray;
    public int[] MachineArray;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IdDet = GetComponent<idDeterminer>();
        us = Univ.GetComponent<UniversalScript>();

        BadgeArray = new int[BadgeButtons.Length];
        BallArray = new int[BallButtons.Length];
        MachineArray = new int[MachineModsButton.Length];

    }

    // Update is called once per frame
    void Update()
    {

        if (shopMoneyStart && !shopMoneyStarted)
        {
            Money += us.score/100;
            shopMoneyStart = false;
            shopMoneyStarted = true;
            for (int i = 0; i < BadgeButtons.Length; i++)
            {
                int RandomInt = Random.Range(0,Badges.Length);
                BadgeButtons[i].image.sprite = Badges[RandomInt];
                BadgeArray[i] = RandomInt;
            }
            for (int i = 0; i < BallButtons.Length; i++)
            {
                int RandomInt = Random.Range(0,BallMods.Length);
                BallButtons[i].image.sprite = BallMods[RandomInt];
                BallArray[i] = RandomInt;
            }
            for (int i = 0; i < MachineModsButton.Length; i++)
            {
                int RandomInt = Random.Range(0,MachineMods.Length);
                MachineModsButton[i].image.sprite = MachineMods[RandomInt];
                MachineArray[i] = RandomInt;
            }
        }
        MoneyText.text = "Money: " + Money.ToString();

    }
    public void BadgeButtonHit(int ID)
    {
        IdDet.Badge(BadgeArray[ID]);
    }
    public void BallButtonHit(int ID)
    {
        IdDet.Ball(BallArray[ID]);
    }
    public void MachineButtonHit(int ID)
    {
        IdDet.Machine(MachineArray[ID]);
    }
}


