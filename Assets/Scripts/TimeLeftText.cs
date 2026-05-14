using TMPro;
using UnityEngine;

public class TimeLeftText : BaseText
{
    [SerializeField] protected Time timer;

    private void OnEnable()
    {
        timer.OnTimeChanged += UpdateUI;
    }

    private void OnDisable()
    {
        timer.OnTimeChanged -= UpdateUI;
    }

    private void UpdateUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        text.text = $"{minutes:00}:{seconds:00}";
    }
}