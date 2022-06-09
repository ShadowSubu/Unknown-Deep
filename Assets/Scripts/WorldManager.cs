using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public CheckPoints checkPoint { get; private set; }
    public GameObject submarinePrefab;
    public GameObject submarine;

    private void Awake()
    {
        instance = this;
    }
}
