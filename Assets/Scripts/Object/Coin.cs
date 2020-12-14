using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Coin);
            GameManager.Instance.Coin++;
        }
    }
    private void Update()
    {
        if(GameManager.Instance.floor.transform.position.y > transform.position.y +1)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Coin);
        }
    }
}
