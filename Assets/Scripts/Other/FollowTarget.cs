﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    Vector3 velocity = Vector3.zero;
    float dempTime = 0.5f;

    private void Update()
    {
        if(target)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, pos.z));
            Vector3 destination = transform.position + delta;

            destination.x = 0;
            if(destination.y > transform.position.y)
            {
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dempTime);
            }

        }
    }
}
