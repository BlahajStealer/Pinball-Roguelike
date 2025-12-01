using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CameraScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Ball;
    Vector3 OriginalPos;
    public Button button;
    public ColorBlock tsp;
    public ColorBlock notTsp;
    public TextMeshProUGUI Text;

    bool Following;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Following = true;
        OriginalPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Following)
        {
            Follows();
        } else
        {
            FollowsNot();
        }
    }
    void Follows()
    {
        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y, OriginalPos.z);

    }
    void FollowsNot()
    {
        transform.position = new Vector3(0,0.8f,-34.7f);
    }    
    public void ChangePersp()
    {
        if (Following)
        {
            Following = false;
            button.colors = tsp;
            Text.text = "Overview";
            Debug.Log("Overview");
        } else
        {
            Following = true;
            button.colors = notTsp;
            Text.text = "Following";
            Debug.Log("Following");
            
        }
    }
}
