using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSettings : MonoBehaviour
{
    public float antSpeed = 2f;

    public float sightDist = 2f;

    public int sightRayAmount = 10;

    public float collectDist = .5f;

    public float antSpawnRate = 2f;

    public void ChangeSightDist(float _change)
    {
        sightDist = _change + 1;
    }

    public void ChangeSpeed(float _change)
    {
        antSpeed = _change;
    }

    public void ChangeSpawnRate(float _change)
    {
        Debug.Log(_change);
        antSpawnRate = _change + 1;
    }
}