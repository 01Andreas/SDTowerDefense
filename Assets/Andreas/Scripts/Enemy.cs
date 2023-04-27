using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    [SerializeField]
    private float enemyHealth;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private int killReward;
    [SerializeField]
    private int damage;

    private MoneyManager moneyManager;
    private PlayerHealth Health;
    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        initializeEnemy();
        moneyManager = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyManager>();
        Health = GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHealth>();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void takeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Enemies.enemies.Remove(gameObject);
        moneyManager.AddMoney(killReward);

        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;


            if (distance < 0.001f)
            {
                if (targetTile != MapGenerator.endTile)
                {

                    int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

                    targetTile = MapGenerator.pathTiles[currentIndex + 1];
                }
                else
                {
                    Enemies.enemies.Remove(gameObject);
                    Health.DamagePlayer(damage);

                    Destroy(transform.gameObject);
                    // DoEndStuff
                    Debug.Log("Yes");
                }
            }
        }
    }

    private void Update()
    {
        checkPosition();
        moveEnemy();

        takeDamage(0);
    }
}
