using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (GameManager.Instance.floor.transform.position.y > transform.position.y + 1)
        {
            GameManager.Instance.AddInActiveObjectToPool(gameObject, ObjectType.Bullet);

        }
    }
}
