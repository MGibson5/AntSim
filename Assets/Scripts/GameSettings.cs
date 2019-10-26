using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float antSpeed = 2f;
    public static float AntSpeed => Instance.antSpeed;

    [SerializeField] private float sightDist = 2f;
    public static float SightDist => Instance.sightDist;

    [SerializeField] private int sightRayAmount = 10;
    public static float SightRayAmount => Instance.sightRayAmount;

    [SerializeField] private float collectDist = .5f;
    public static float CollectDist => Instance.collectDist;

    public static GameSettings Instance{get; private set;}

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
