using UnityEngine;

public class goldperc : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    GameObject Ball;
    BallScript bs;
    GameObject Shop;
    ShopScript sc;
    public float totalGold;
    public float totalNormal;
    void Start()
    {
        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        Ball = GameObject.FindGameObjectWithTag("Player");
        bs = Ball.GetComponent<BallScript>();
        Shop = GameObject.FindGameObjectWithTag("Shop");
        sc = Shop.GetComponent<ShopScript>();
        totalGold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
        totalNormal = GameObject.FindGameObjectsWithTag("Pingy Thing").Length;
        totalNormal += GameObject.FindGameObjectsWithTag("AddedPinger").Length;
        totalNormal += totalGold;
        Debug.Log("Hai2" + (totalGold / totalNormal));

        us.Division = totalGold / totalNormal;
        Debug.Log(us.Division);
    }

    void Update()
    {
        if (sc.PercChanged)
        {
            
            totalGold = GameObject.FindGameObjectsWithTag("Gold Pingy Thing").Length;
            totalNormal = GameObject.FindGameObjectsWithTag("Pingy Thing").Length;
            totalNormal += GameObject.FindGameObjectsWithTag("AddedPinger").Length;
            totalNormal += totalGold;
            Debug.Log("Hai: " + (totalGold/totalNormal) + ":  " + totalGold + ":  " + totalNormal);
            us.Division = totalGold / totalNormal;
            Debug.Log(us.Division);

        }
    }

}
