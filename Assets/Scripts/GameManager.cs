using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text goldLabel;
    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.text = "GOLD: " + gold;
        }
    }
    private void Start()
    {
        Gold = 1000;
    }
}
