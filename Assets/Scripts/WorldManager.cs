using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public CheckPoints checkPoint;
    public GameObject submarinePrefab;
    public SubmarineController submarine;
    public Image health;
    public RectTransform healthBar;
    public int wallDamage;
    public int spikeDamage;
    public int volcanoDamage;
    public LevelLoader loader;
    public AudioSource fragmentCollectionSound;
    [SerializeField] TextMeshProUGUI timer;
    float time;
    bool startTimer;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        health.fillAmount = 1f;
        healthBar = health.GetComponent<RectTransform>();
        startTimer = true;
    }

    private void Update()
    {
        if (startTimer)
        {
            time += Time.deltaTime;
        }
    }

    public void ShowTimer()
    {
        time = Mathf.Clamp(time, 0f, Mathf.Infinity);
        int minute = (int)(time / 60f);
        int seconds = (int)(time % 60);
        timer.text = "Expedition Time : " + minute.ToString() + ":" + seconds.ToString();
    }
}
