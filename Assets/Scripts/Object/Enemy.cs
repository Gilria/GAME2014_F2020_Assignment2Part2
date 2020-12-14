using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed;
    float distance;
    Vector3 StartPos;
    int direction; //0 left

    public float[] Speed;
    public float[] Distance;
    public Sprite[] Sprites;
    
    int EnemyType;
    SpriteRenderer sp;




    private void Update()
    {
        if (direction == 0)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            if (StartPos.x - transform.position.x > distance)
            {
                transform.localScale = new Vector3(transform.localScale.x *(-1), transform.localScale.y, transform.localScale.z);
                direction = 1;
            }
                
        }
        else
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            if (StartPos.x - transform.position.x < -distance)
            {
                transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
                direction = 0;
            }
        }

        if (GameManager.Instance.floor.transform.position.y > transform.position.y + 1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Enemy);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().gravityScale = 3;
            //Stop Player Collider
            player.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.EndGame();
        }
        if (collision.tag == "Bullet" && collision.GetComponent<Rigidbody2D>().velocity.y >=0)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Enemy);
            GameManager.Instance.AddInActiveObjectToPool(collision.gameObject, ObjectType.Bullet);
            //Add Score
            GameManager.Instance.Score += 10;

        }
    }


    void Init()
    {
        sp = GetComponent<SpriteRenderer>();
        EnemyType = int.Parse(gameObject.name);
        sp.sprite = Sprites[EnemyType];

        speed = Speed[EnemyType];
        StartPos = transform.position;
        distance = Distance[EnemyType];
    }

    private void OnEnable()
    {
        Init();
    }
}
