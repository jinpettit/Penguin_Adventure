using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishCounter : MonoBehaviour
{
    public static FishCounter instance;

    public TMP_Text counterText;
    public int currentFish = 0;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        counterText.text = "Fish:" + currentFish.ToString();
    }

    // Update is called once per frame
   public void IncreaseFishCount(int v)
    {
        currentFish += v;
        counterText.text = "Fish: " + currentFish.ToString();
    }
}