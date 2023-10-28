using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count_Stars : MonoBehaviour
{
    public static Count_Stars count_stars;

    [SerializeField] private TMP_Text starText;
    private int countStars = 0;

    private void Awake()
    {
        count_stars = this;
    }

    private void Update()
    {
        starText.text = countStars.ToString();
    }

    public int GetStarCount()
    {
        return countStars;
    }

    public void SetStarCount(int newCountStars)
    {
        countStars = newCountStars;
    }
}
