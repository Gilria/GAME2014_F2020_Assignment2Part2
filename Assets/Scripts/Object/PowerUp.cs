using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int itemType;
    public SpriteRenderer sp;
    

    public Sprite[] sprites;
    public float[] powerTime;

    public GameObject[] used;

    GameObject player;
    bool fly;

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        sp.enabled = true;
        itemType = int.Parse(gameObject.name);
        sp.sprite = sprites[itemType];
        fly = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Item);
            player = collision.gameObject;

            sp.enabled = false;

            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //change the body type to kinematic, so the gravity will not effect the player
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            //player invincible
            player.GetComponent<BoxCollider2D>().enabled = false;
            used[itemType].SetActive(true);
            fly = true;
            StartCoroutine("StopFly");
            SoundManageer.Instance.PlaySFX(1);
        }
    }


    IEnumerator StopFly()
    {
        yield return new WaitForSeconds(powerTime[itemType]);
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        
        player.GetComponent<BoxCollider2D>().enabled = true;
        used[itemType].SetActive(false);
        fly = false;
        SoundManageer.Instance.StopSFX();
    }

    private void Update()
    {
        if(fly)
        {
            player.transform.Translate(new Vector2(0, 12 * Time.deltaTime));
        }
        else if (GameManager.Instance.floor.transform.position.y > transform.position.y + 1)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Item);

        }
    }
}
