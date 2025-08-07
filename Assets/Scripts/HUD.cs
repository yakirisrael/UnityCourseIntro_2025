using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public float health = 0.5f;
    public int score = 0;
    public GameObject ScoreTxt;
    public GameObject Filler;
    
    TextMeshProUGUI ScoreTxtComponent;
    Image FillerComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScoreTxtComponent = ScoreTxt.GetComponent<TextMeshProUGUI>();
        FillerComponent = Filler.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTxtComponent.text = "Score: " + score.ToString();
        FillerComponent.fillAmount = health;
    }
}
