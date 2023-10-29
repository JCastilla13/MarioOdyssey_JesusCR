using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Moons : MonoBehaviour
{
    public static Count_Moons count_moons;

    [SerializeField] private TMP_Text starText;
    private int countMoons = 0;

    private void Awake()
    {
        count_moons = this;
    }

    private void Update()
    {
        starText.text = countMoons.ToString();
    }

    public int GetStarCount()
    {
        return countMoons;
    }

    public void SetStarCount(int newCountMoons)
    {
        countMoons = newCountMoons;
    }
}
