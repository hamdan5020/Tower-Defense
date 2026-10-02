using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int balance = 30;
    [SerializeField] int health = 20;

    [SerializeField] TextMeshProUGUI balanceTMP;

    [SerializeField] TextMeshProUGUI scoreTMP;

    [SerializeField] TextMeshProUGUI healthTMP;

    [SerializeField] TextMeshProUGUI[] wavesTMP;

    [SerializeField] TextMeshProUGUI gameOverTMP;

    [SerializeField] TextMeshProUGUI finalScoreTMP;

    private int score = 0;
    
    private EnemySpawner enemySpawner;

    UnitDisplay[] unitDisplays;

    // Start is called before the first frame update
    void Start()
    {
        gameOverTMP.enabled = false;
        finalScoreTMP.enabled = false;

        unitDisplays = FindObjectsOfType<UnitDisplay>();
        balanceTMP.text = "X " + balance.ToString();
        scoreTMP.text = "score: " + 0;
        healthTMP.text = "X " + health.ToString();
        
        enemySpawner = FindObjectOfType<EnemySpawner>();

        UpdateWaveInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBalance(int bal)
    {
        balance += bal;
        balanceTMP.text = "X " + balance.ToString();

        foreach (var unitDisplay in unitDisplays) 
        {
            unitDisplay.UpdateAfordability(balance);
        }

    }

    public void RemoveBalance(int bal)
    {
        balance -= bal;

        balanceTMP.text = "X " + balance.ToString();

        foreach (var unitDisplay in unitDisplays)
        {
            unitDisplay.UpdateAfordability(balance);
            
        }

    }

    public void AddScore(int scr)
    {
        score += scr;
        scoreTMP.text = "score: " + score;
    }

    public void DamageTower(int dmg) {
        health -= dmg;
        healthTMP.text = "X " + health;

        if (health <= 0)
        {
            healthTMP.text = "X " + 0;
            enemySpawner.stopSpawns();

            gameOverTMP.enabled = true;
            finalScoreTMP.text = "Score: " + score;
            finalScoreTMP.enabled = true;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateWaveInfo()
    {
        wavesTMP[0].text = "Wave " + (enemySpawner.GetWaveCount()+1) + " : " + enemySpawner.IDtoName(enemySpawner.GetNextWaves().ElementAt(0));

        wavesTMP[1].text = "Wave " + (enemySpawner.GetWaveCount()+2) + " : " + enemySpawner.IDtoName(enemySpawner.GetNextWaves().ElementAt(1));

        wavesTMP[2].text = "Wave " + (enemySpawner.GetWaveCount()+3) + " : " + enemySpawner.IDtoName(enemySpawner.GetNextWaves().ElementAt(2));


    }

    


}
