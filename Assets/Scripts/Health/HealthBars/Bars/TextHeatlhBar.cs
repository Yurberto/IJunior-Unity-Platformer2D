using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHeatlhBar : HealthBar
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override void UpdateHealthData()
    {
        _text.text = ((int)Health.CurrentValue).ToString() + "/" + ((int)Health.MaxValue).ToString(); 
    }
}
