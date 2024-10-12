using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BattleManager : MonoBehaviour
{
    public enum Phase
    {
        PlayerThinking,
        PlayerActing,
        EnemyActing,
        BattleOver
    }

    [SerializeField] public float maxPlayerHealth = 100;
    [SerializeField] public float maxEnemyHealth = 1000;

    public float playerHealth;
    public float enemyHealth;
    public List<StatusEffect> playerStatuses = new();
    public List<StatusEffect> enemyStatuses = new();

    public UnityEvent<Phase> phaseUpdated;
    public UnityEvent<float> playerHealed;
    public UnityEvent<float> playerHurt;
    public UnityEvent<float> enemyHealed;
    public UnityEvent<float> enemyHurt;
    public UnityEvent playerStatusesUpdated;
    public UnityEvent enemyStatusesUpdated;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
        enemyHealth = maxEnemyHealth;
    }

    public void HealPlayer(float amount)
    {
        playerHealth += amount;
        playerHealed.Invoke(amount);
    }

    public void HurtPlayer(float amount)
    {
        playerHealth -= amount;
        playerHurt.Invoke(amount);
    }

    public void HealEnemy(float amount)
    {
        enemyHealth += amount;
        enemyHealed.Invoke(amount);
    }

    public void HurtEnemy(float amount)
    {
        enemyHealth -= amount;
    }

    public void InflictPlayerStatus(StatusEffect status)
    {
        foreach(StatusEffect existingStatus in playerStatuses)
        {
            if (existingStatus.effect == status.effect)
            {
                existingStatus.turnsRemaining += status.length;
                playerStatusesUpdated.Invoke();
                return;
            }
        }
        playerStatuses.Add(status);
        playerStatusesUpdated.Invoke();
    }

    public void ClearPlayerStatus(StatusEffect status)
    {
        if (playerStatuses.Contains(status)){
            playerStatuses.Remove(status);
            playerStatusesUpdated.Invoke();
        }
    }

    public void InflictEnemyStatus(StatusEffect status)
    {
        foreach (StatusEffect existingStatus in enemyStatuses)
        {
            if (existingStatus.effect == status.effect)
            {
                existingStatus.turnsRemaining += status.length;
                enemyStatusesUpdated.Invoke();
                return;
            }
        }
        enemyStatuses.Add(status);
        enemyStatusesUpdated.Invoke();
    }

    public void ClearEnemyStatus(StatusEffect status)
    {
        if (enemyStatuses.Contains(status))
        {
            enemyStatuses.Remove(status);
            enemyStatusesUpdated.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
