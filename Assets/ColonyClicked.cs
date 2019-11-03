using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ColonyClicked : OnClicked
{
    public GameObject ColonyHighlite;
    public GameObject ColonyUI;
    public Colony Colony;
    [SerializeField]private AntSettings antSettings;
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

        SightText.text = antSettings.sightDist.ToString();
    }

    public override void ClickOff()
    {
        ColonyHighlite.SetActive(false);
        ColonyUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Colony = gameObject.GetComponent<Colony>();
        antSettings = gameObject.GetComponent<AntSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        FoodUI.text = Colony.food.ToString();
        PopUI.text = Colony.Ants.Count.ToString();
        SightText.text = antSettings.sightDist.ToString();
        SpeedText.text = antSettings.antSpeed.ToString();
        SpawnRateText.text = antSettings.antSpawnRate.ToString();

    }
}
