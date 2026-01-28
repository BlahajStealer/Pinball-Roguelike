using UnityEngine;

public class Add50 : MonoBehaviour
{
    GameObject Universal;
    UniversalScript us;
    public int totalPoints;
    public int sellValue;
    public int buyValue;

    void Start()
    {

        Universal = GameObject.FindGameObjectWithTag("Empty");
        us = Universal.GetComponent<UniversalScript>();
        us.Add50 = true;
    }

    void Update()
    {

    }


}
