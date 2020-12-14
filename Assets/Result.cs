using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt_Score;
    private void OnEnable()
    {
        txt_Score.text = GameManager.Instance.Score.ToString();
    }

}
