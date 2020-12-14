using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //Screen Bordary
    float rightBorder;
    float leftBorder;

    SpriteRenderer sp;

    public Sprite fire;
    public Sprite normal;

    void Start()
    {
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
        sp = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamestate == GameState.Running)
        {

            Vector3 acc = Vector3.zero;
            Vector3 diff;
            //used on simulator(Phone)
            if (Input.touchCount > 0)
            {
                Touch myTouch = Input.GetTouch(Input.touchCount - 1);
                if (myTouch.phase == TouchPhase.Stationary || myTouch.phase == TouchPhase.Moved)
                {
                    if (myTouch.position.y <= Screen.width / 2)
                    {
                        if (myTouch.position.x < Screen.width / 2) //move left
                        {
                            acc.x = -0.1f;
                            transform.localScale = new Vector3(-1, 1, 1);

                        }
                        else if (myTouch.position.x > Screen.width / 2) //move right
                        {
                            acc.x = 0.1f;
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
                        diff.y = transform.localPosition.y;
                        diff.z = 0;
                        //set bordary
                        if (diff.x < leftBorder)
                        {
                            diff.x = rightBorder;
                        }
                        if (diff.x > rightBorder)
                        {
                            diff.x = leftBorder;
                        }
                        transform.localPosition = diff;
                    }

                    //shoot Bullet
                    

                
                }
                if (myTouch.position.y > Screen.width / 2)
                {
                    if (myTouch.phase == TouchPhase.Began)
                    {
                        GameManager.Instance.GetInactiveObject(ObjectType.Bullet);
                        sp.sprite = fire;
                    }

                    else if (myTouch.phase == TouchPhase.Ended)
                    {
                        sp.sprite = normal;
                    }

                }

            }
            //used on game(PC)
            //if (Input.GetKey(KeyCode.LeftArrow))
            //{
            //    acc.x = -0.1f;
            //}
            //if (Input.GetKey(KeyCode.RightArrow))
            //{
            //    acc.x = 0.1f;
            //}
            //diff = Vector3.MoveTowards(transform.localPosition, transform.localPosition + acc, 0.5f * Time.time);
        }  //transform.localPosition = diff;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            Jump(1);
        }
        if(collision.tag == "Floor")
        {
            GameManager.Instance.EndGame();
        }
    }

    public void Jump(float x)
    {
        if (GameManager.Instance.gamestate == GameState.GameOver)
            return;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13.5f * x), ForceMode2D.Impulse);
        SoundManageer.Instance.PlaySFX(2);
    }
}
