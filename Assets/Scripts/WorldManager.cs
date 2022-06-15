using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public CheckPoints checkPoint;
    public GameObject submarinePrefab;
    public SubmarineController submarine;
    public Image health;
    public int wallDamage;
    public int spikeDamage;
    public int volcanoDamage;
    public LevelLoader loader;
    public AudioSource fragmentCollectionSound;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        health.fillAmount = 1f;
    }
}
