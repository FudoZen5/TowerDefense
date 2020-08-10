using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class MonsterLevel
{
    [HideInInspector]
    public string name = "MonsterLevel";
    public int cost;
    public GameObject visualization;
    public int damage;
}

public class MonsterData : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrehab;
    public Transform BulletConteiner;
    private float count;
    private float reload;

    public List<MonsterLevel> levels;
    [SerializeField] private EnemySearch enemySearch;
    private Transform currentTarget;
    public bool HaveUpgrade => levels[levels.Count - 1] != CurrentLevel;
    private MonsterLevel currentLevelValue;
    public MonsterLevel CurrentLevel
    {
        //2
        get
        {
            return currentLevelValue;
        }
        //3
        private set
        {
            currentLevelValue = value;
            levels.ForEach(monsterLevel => monsterLevel.visualization?.SetActive(monsterLevel == currentLevelValue));
        }
    }
    
    private void Start()
    { 
        reload = 2f;
        count = reload;

    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(CurrentLevel);
        int maxLevelIndex = levels.Count - 1;
        return levels[currentLevelIndex < maxLevelIndex ? ++currentLevelIndex : maxLevelIndex];
    }

    public void IncreaseLevel()
    {
        CurrentLevel = GetNextLevel();
    }

    private Transform SetNewEnemyTarget()
    {
        float minDistance = Mathf.Infinity;
        Transform target = null;

        if (enemySearch.EnemyiesInRange.Count < 1)
            return target;

        foreach (Transform enemy in enemySearch.EnemyiesInRange)
        {
            float distance = Vector2.Distance(enemy.position, transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                target = enemy;
            }
        }

        return target;
    }

    private void Update()
    {
        if (!enemySearch.EnemyiesInRange.Contains(currentTarget))
        {
            currentTarget = SetNewEnemyTarget();
        }

        if(currentTarget != null)
        {
            LookAtEnemy();
            if (count <= 0)
            {
                ShootOnEnemy();
                count = reload;
            }
            else
                count -= Time.deltaTime;

        }
    }

    private void LookAtEnemy()
    {
        Vector3 newEndPosition = currentTarget.position;
        Vector3 newDirection = (transform.position - newEndPosition);
        //2
        float rotationAngle = Mathf.Atan2(newDirection.y, newDirection.x) * 180 / Mathf.PI;
        //3
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }

    private void ShootOnEnemy()
    {
        Bullet Clone = Instantiate(BulletPrehab, BulletConteiner).GetComponent<Bullet>();
        Clone.transform.position = transform.position;
        Clone.damage = CurrentLevel.damage;
        Clone.StartMove(currentTarget);
    }
}