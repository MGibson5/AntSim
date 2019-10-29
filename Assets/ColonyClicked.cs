using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ColonyClicked : OnClicked
{
    public GameObject ColonyHighlite;
    public GameObject ColonyUI;
    private Colony Colony;

    [SerializeField] private TextMeshProUGUI ColonyTitle;

    [SerializeField] private TextMeshProUGUI FoodUI;
    [SerializeField] private TextMeshProUGUI PopUI;

    [SerializeField] private TextMeshProUGUI SightText;
    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI SpawnRateText;



    public override void Clicked()
    {
        ColonyHighlite.SetActive(true);
        ColonyUI.SetActive(true);

        ColonyTitle.text = Colony.ColonyName;
        FoodUI.text = Colony.food.ToString();
        PopUI.text = Colony.Ants.Count.ToString();

        SightText.text = GameSettings.SightDist.ToString();
    }

    public override void ClickOff()
    {
        Debug.Log("DAWD");
        ColonyHighlite.SetActive(false);
        ColonyUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Colony = gameObject.GetComponent<Colony>();
    }

    // Update is called once per frame
    void Update()
    {
        FoodUI.text = Colony.food.ToString();
        PopUI.text = Colony.Ants.Count.ToString();
        SightText.text = GameSettings.SightDist.ToString();
        SpeedText.text = GameSettings.AntSpeed.ToString();
        SpawnRateText.text = GameSettings.AntSpawnRate.ToString();

    }
}
