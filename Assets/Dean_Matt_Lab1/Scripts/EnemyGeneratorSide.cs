using UnityEngine;
using System.Collections;

public class EnemyGeneratorSide : MonoBehaviour
{

    #region Private Fields
    [SerializeField]
    GameObject[] EnemyTypes;

    [SerializeField]
    private float TimeMin = 1.0f;

    [SerializeField]
    private float TimeMax = 4.0f;

    [SerializeField]
    private float EnemyMoveSpeed = 0;

    //[SerializeField]
    //[Range(0, 10)]
    //private float EnemyMoveRange = 0;

    [SerializeField]
    [Range(-1, -4.75f )]
    private float EnemyMoveRangeMin;

    [SerializeField]
    [Range(1, 4.75f)]
    private float EnemyMoveRangeMax;

    //[SerializeField]
    //private Camera

    private float TimeLimit = 0;

    private float Timer = 0;

    private Vector3 enemyPosition;

    private float enemyPositionX;

    #endregion
    // Use this for initialization
    void Start()
    {
        enemyPositionX = 12.45f;
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        updateEnemyMoveRange(EnemyMoveRangeMin, EnemyMoveRangeMax);
    }

    void updateEnemyMoveRange(float EnemyMoveRangeMin = -4.75f, float EnemyMoveRangeMax = 4.75f)
    {
        enemyPosition.x = enemyPositionX;
        enemyPosition.y = Random.Range(EnemyMoveRangeMin, EnemyMoveRangeMax);
        gameObject.transform.position = enemyPosition;
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
