using UnityEngine;

public class Wall : MonoBehaviour
{
     void Start()
    {
        if (GetComponent<Collider2D>() == null)
        {
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = false;
        }
        
        gameObject.tag = "Wall";
    }
}
