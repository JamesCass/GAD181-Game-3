using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public float speed = 16;

    //wall prefab.
    public GameObject wallPrefab;
    //current wall.
    Collider2D wall;
    //last wall's end.
    Vector2 lastWallEnd;

    public MenuFunctionality menuFunctionality;


    Vector2 currentDirection;

    private void Start()
    {
        //initial velocty 
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        SpawnWall();
        currentDirection = Vector2.up;
    }
    void Update()
    {
        if (!menuFunctionality.IsGamePaused())
        {

        
            if (Input.GetKeyDown(up) && currentDirection != Vector2.down)
            {
                currentDirection = Vector2.up;
                GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
                SpawnWall();
            }
            if (Input.GetKeyDown(down) && currentDirection != Vector2.up)
            {
                currentDirection = -Vector2.up;
                GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
                SpawnWall();
            }
            if (Input.GetKeyDown(left) && currentDirection != Vector2.right)
            {
                currentDirection = -Vector2.right;
                GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
                SpawnWall();
            }
            if (Input.GetKeyDown(right) && currentDirection != Vector2.left)
            {
                currentDirection = Vector2.right;
                GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
                SpawnWall();
            }

            FitColliderBetween(wall, lastWallEnd, transform.position);
            }
    }

    void SpawnWall()
    {
        //save last wall's position
        lastWallEnd = transform.position;

        //spawn a new lightwall.
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        //calculate the centre position
        co.transform.position = a + (b - a) * 0.5f;

        //Scale it horizontally or vertically 
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1, 1);
        }
        else
        {
            co.transform.localScale = new Vector2(1, dist + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
        //not the current wall
        if (co != wall)
        {
            menuFunctionality.PlayerDestroyed(name);
            Destroy(gameObject);
        }
    }
}
