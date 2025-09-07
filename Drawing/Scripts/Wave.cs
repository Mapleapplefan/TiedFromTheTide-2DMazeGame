using UnityEngine;

public class Wave : MonoBehaviour
{

    [Header("Wave Movement")]
    public float speed = 3f;
    public float range = 50;
    float startingX;
    int direction = 1;
    private bool isStopped = false;

    [Header("Timer Control")]
    public CountdownController countdownController;
    private bool canMove = false;
    private Boat pushedBoat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingX = transform.position.x;
        canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove && countdownController != null)
        {
            if (countdownController.countdownTime <= 0)
            {
                canMove = true;
            }
        }
        if (canMove && transform.position.x < startingX + range && !isStopped)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
            if (pushedBoat != null)
            {
                pushedBoat.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                pushedBoat.transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the boat
        Boat boat = other.GetComponent<Boat>();
        if (boat != null)
        {
            pushedBoat = boat;
            boat.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    
    public void StopWave()
    {
        isStopped = true;
    }

}
