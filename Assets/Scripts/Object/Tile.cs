using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileType;
    public Sprite[] sprites;
    SpriteRenderer sp;

    float speed;
    float distance;
    Vector3 StartPos;
    int direction; //0 left and up   1 right and down

    //private void Start()
    //{
    //    Init();
    //}

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        // NormalTile(Green) 0
        // BrokenTile(Brown) 1
        // OneTimeOnlyTile(Yellow) 2
        //SpringTile(Red) 3
        //MovingHorizontalTile(Blue) 4
        //MovingVerticalTile(Blue) 5
        sp = GetComponent<SpriteRenderer>();
        tileType = int.Parse(gameObject.name);
        switch (tileType)
        {
            case 0:
                sp.sprite = sprites[0];
                break;
            case 1:
                sp.sprite = sprites[1];
                break;
            case 2:
                sp.sprite = sprites[2];
                break;
            case 3:
                sp.sprite = sprites[3];
                break;
            case 4:
                sp.sprite = sprites[4];
                speed = GameManager.Instance.Advance.movingHorizontalTile.speed;
                distance = GameManager.Instance.Advance.movingHorizontalTile.distance;
                StartPos = transform.position;
                break;
            case 5:
                sp.sprite = sprites[5];
                speed = GameManager.Instance.Advance.movingVerticalTile.speed;
                distance = GameManager.Instance.Advance.movingVerticalTile.distance;
                StartPos = transform.position;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        switch (tileType)
        {
            case 4:
                if(direction == 0)
                {
                    transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                    if (StartPos.x - transform.position.x > distance)
                    {
                        direction = 1;
                    }
                }
                else
                {
                    transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                    if (StartPos.x - transform.position.x < -distance)
                    {
                        direction = 0;
                    }
                }
                break;
            case 5:
                if(direction == 0)
                {
                    transform.Translate(new Vector2(0, -speed * Time.deltaTime));
                    if (StartPos.y - transform.position.y > distance)
                    {
                        direction = 1;
                    }
                }
                else
                {
                    transform.Translate(new Vector2(0, speed * Time.deltaTime));
                    if (StartPos.y - transform.position.y < -distance)
                    {
                        direction = 0;
                    }
                }
                break;
        }
        
        if (GameManager.Instance.floor.transform.position.y > transform.position.y + 1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Tile);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            switch (tileType)
            {
                case 0:
                    collision.GetComponent<Player>().Jump(1);
                    break;
                case 1:
                    GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                    SoundManageer.Instance.PlaySFX(3);
                    break;
                case 2:
                    collision.GetComponent<Player>().Jump(1);
                    GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                    break;
                case 3:
                    collision.GetComponent<Player>().Jump(1.5f);
                    break;
                case 4:
                    collision.GetComponent<Player>().Jump(1);
                    break;
                case 5:
                    collision.GetComponent<Player>().Jump(1);
                    break;
                default:
                    break;
            }
        }

    }
}
