using UnityEngine;

public class Every4 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;

    GameObject Shop;
    ShopScript sc;
    int pingers;
    public int amountToAdd;
    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();

        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        pingers = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length;
        amountToAdd = Mathf.RoundToInt(Mathf.Floor(pingers / 4f) * 100);
        us.Addition += amountToAdd;
    }

    void Update()
    {
        if (sc.PercChanged)
        {
            us.Addition -= amountToAdd;
            pingers = GameObject.FindGameObjectsWithTag("Pingy Thing").Length + GameObject.FindGameObjectsWithTag("AddedPinger").Length;
            amountToAdd = Mathf.RoundToInt(Mathf.Floor(pingers / 4f) * 100);
            us.Addition += amountToAdd;
        }
    }
}
