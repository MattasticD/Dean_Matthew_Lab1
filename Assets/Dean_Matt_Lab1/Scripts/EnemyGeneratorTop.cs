using UnityEngine;
using System.Collections;

public class EnemyGeneratorTop : MonoBehaviour {

    #region Private Fields
    [SerializeField]
    GameObject[] EnemyTypes;

    [SerializeField]
    private float TimeMin = 1.0f;

    [SerializeField]
    private float TimeMax = 4.0f;

    [SerializeField]
    private float EnemyMoveSpeed = 0;

    [SerializeField]
    [Range(-1, -10)]
    private float EnemyMoveRangeMin;

    [SerializeField]
    [Range(1, 10)]
    private float EnemyMoveRangeMax;

    private float TimeLimit = 0;

    private float Timer = 0;

    private Vector3 enemyPosition;

    private float enemyPositionY;
    #endregion
    // Use this for initialization
    void Start () {
        enemyPositionY = 5.5f;
	}
	
	// Update is called once per frame
	void Update () {
        updateTimer();
        updateEnemyMoveRange(EnemyMoveRangeMin, EnemyMoveRangeMax);

    }

    void updateEnemyMoveRange(float EnemyMoveRangeMin = -10, float EnemyMoveRangeMax = 10)
    {
        enemyPosition.y = enemyPositionY;
        enemyPosition.x = Random.Range(EnemyMoveRangeMin, EnemyMoveRangeMax);
        gameObject.transform.position = enemyPosition;
    }

    void updateEnemyPosition()
    {
        Vector3 enemyPosition = gameObject.transform.position;
        enemyPosition.x = Random.Range(-8, 8);
        gameObject.transform.position = enemyPosition;
    }

    void updateEnemyMoveRange(float EnemyMoveRange)
    {
        
        

        
    }

    void updateTimer()
    {
        Timer += Time.deltaTime;

        if (Timer >= TimeLimit)
        {
            TimeLimit = Random.Range(TimeMin, TimeMax);

            Timer = 0;

            doSpawn();
        }
    }

    void doSpawn()
    {
        int randIndex = Random.Range(0, EnemyTypes.Length);

        Instantiate(EnemyTypes[randIndex],
                    transform.position, transform.rotation);
    }
}
