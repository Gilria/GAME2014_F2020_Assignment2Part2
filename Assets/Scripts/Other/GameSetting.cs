using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameSetting
{
    [Serializable]
    public class NormalTile
    {
        //random height
        public float minHeight;
        public float maxHeight;
        //Power
        public float weight;
    }
    [Serializable]
    public class BrokenTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class OneTimeOnlyTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class SpringTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }
    [Serializable]
    public class MovingHorizontalTile
    {
        public float minHeight;
        public float maxHeight;
        //moving distance and speed
        public float distance;
        public float speed;
        public float weight;
    }
    [Serializable]
    public class MovingVerticalTile
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
        public float weight;
    }
    public NormalTile normalTile;
    public BrokenTile brokenTile;
    public OneTimeOnlyTile oneTimeOnlyTile;
    public SpringTile springTile;
    public MovingHorizontalTile movingHorizontalTile;
    public MovingVerticalTile movingVerticalTile;

    public float itemProbability;
    //public float coinProbability;
    public float enemyProbability;
}
