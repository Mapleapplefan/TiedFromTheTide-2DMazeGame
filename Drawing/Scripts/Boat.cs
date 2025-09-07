using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boat : MonoBehaviour
{
    [Header("Movement")]
    public DrawMouse drawControll;
    public float speed = 2f;

    [Header("Collision Detection")]
    public bool enableCollision = true;
    public bool debugCollision = true;

    bool startMove = false;
    int moveIndex = 0;
    Vector3[] positions;

    void OnMouseDown()
    {
        drawControll.StartLine(transform.position);
    }

    void OnMouseDrag()
    {
        drawControll.UpdateLine();
    }

    private void Update()
    {
        if (startMove == true)
        {
            if (positions != null && moveIndex < positions.Length) //null check
            {
                //translate
                Vector2 currentPos = positions[moveIndex];
                transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
                //rotate
                Vector2 dir = currentPos - (Vector2)transform.position;
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg), speed);

                float distance = Vector2.Distance(currentPos, transform.position);
                if (distance <= 0.05f)
                {
                    moveIndex++;
                }
                if (moveIndex >= positions.Length)
                {
                    startMove = false;
                }
            }
        }
    }

    void OnMouseUp()
    {
        positions = new Vector3[drawControll.line.positionCount];
        drawControll.line.GetPositions(positions);
        startMove = true;
        moveIndex = 0;
    }

    // COLLISION DETECTION
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enableCollision) return;

        if (IsTilemapWall(collision.gameObject))
        {
            if (debugCollision)
            {
                GameManager.isGameOver = true;
            }
            StopMovement();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enableCollision) return;

        if (IsTilemapWall(other.gameObject))
        {
            if (debugCollision)
            {
                GameManager.isGameOver = true;
                // gameObject.SetActive(false);
                // Debug.Log($"🚢 Boat triggered wall: {other.gameObject.name}");
            }
            StopMovement();
        }

        if (IsDock(other.gameObject))
        {
            if (debugCollision)
            {
                Debug.Log($"🚢 MADE IT TO DOCK: {other.gameObject.name}");
                GameManager.isWin = true;
            }
            StopMovement();
        }

        if (IsWave(other.gameObject))
    {
        if (debugCollision)
        {
            Debug.Log($"🌊 Boat hit by wave: {other.gameObject.name}");
            GameManager.isGameOver = true;
        }
        StopMovement();
    }

        
    }

    bool IsWave(GameObject hitObject)
    {
        // Check by name
        if (hitObject.name.ToLower().Contains("wave"))
        {
            return true;
        }
        
        // Check by tag (if you want to tag your wave)
        if (hitObject.CompareTag("Wave"))
        {
            return true;
        }
        
        // Check by component (if your wave has the Wave script)
        if (hitObject.GetComponent<Wave>() != null)
        {
            return true;
        }
        
        return false;
    }
    

    bool IsDock(GameObject hitObject)
{
    // Check by name (since your dock is named "Dock")
    if (hitObject.name.ToLower().Contains("dock"))
    {
        return true;
    }
    
    // Optional: Check by tag if you've tagged your dock
    else if (hitObject.CompareTag("Dock"))
    {
        return true;
    }
    
    return false;
}
    
    bool IsTilemapWall(GameObject hitObject)
    {
        // Check if it's a tilemap by looking for tilemap components
        if (hitObject.GetComponent<UnityEngine.Tilemaps.Tilemap>() != null || 
            hitObject.GetComponent<UnityEngine.Tilemaps.TilemapCollider2D>() != null ||
            hitObject.GetComponent<CompositeCollider2D>() != null)
        {
            return true;
        }
        
        // Check by tag (if you want to use tags)
        if (hitObject.CompareTag("Wall"))
        {
            return true;
        }
        
        // Check by name (common tilemap GameObject names)
        string objName = hitObject.name.ToLower();
        if (objName.Contains("tilemap") || objName.Contains("wall") || objName.Contains("grid"))
        {
            return true;
        }
        
        return false;
    }
    
    public void StopMovement()
    {
        startMove = false;
        if (debugCollision)
        {
            Debug.Log("⛔ Boat stopped!");
        }
    }
    
    // Helper methods for other scripts if needed
    public bool IsMoving()
    {
        return startMove;
    }
    
    public Vector2 GetMovementDirection()
    {
        if (!startMove || positions == null || moveIndex >= positions.Length)
            return Vector2.zero;
            
        Vector2 currentPos = positions[moveIndex];
        Vector2 direction = (currentPos - (Vector2)transform.position).normalized;
        return direction;
    }
    
}
