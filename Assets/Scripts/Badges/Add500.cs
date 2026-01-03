using UnityEngine;

public class Add500 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Shop;
    ShopScript sc;
    public int Add;
    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();

        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();

        Add = 500;
        int totalGold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
        for (int i = 0; i < totalGold; i++)
        {
            Add -= 100;
        }
        if (Add <= 0)
        {
            Add = 0;
        }
        us.Addition += Add;
    }

    void Update()
    {
        if (sc.PercChanged)
        {
            Add = 500;
            int totalGold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
            for (int i = 0; i < totalGold; i++)
            {
                Add -= 100;
            }
            if (Add <= 0)
            {
                Add = 0;
            }
            us.Addition += Add;
        }
    }
}
