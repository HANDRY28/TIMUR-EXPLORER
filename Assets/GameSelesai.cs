using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSelesai : MonoBehaviour
{
    public TextMeshProUGUI Teks_Score, Teks_TotalScore;

    public void Start()
    {
        if (Data.DataScore >= PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", Data.DataScore);
        }

        Teks_Score.text = Data.DataScore.ToString();
        Teks_TotalScore.text = PlayerPrefs.GetInt("score").ToString(); 

    }
}
