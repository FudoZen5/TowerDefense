using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMonster : MonoBehaviour
{
    public GameObject monsterPrefab;
    private MonsterData monster;
    private GameManager gameManager;
    private Transform bulletConteiner;

    private bool CanPlaceMonster()
    {
        int cost = monsterPrefab.GetComponent<MonsterData>().levels[0].cost;
        return monster == null && gameManager.Gold >= cost;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bulletConteiner = GameObject.Find("BulletConteiner").transform;
    }

    //1
    void OnMouseUp()
    {
        //2
        if (CanPlaceMonster())
        {
            //3
            monster =
              Instantiate(monsterPrefab, transform.position, Quaternion.identity,transform).GetComponent<MonsterData>();
            //4
            monster.BulletConteiner = bulletConteiner;
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.CurrentLevel.cost;
            return;
        }

        if (monster.HaveUpgrade && gameManager.Gold >= monster.GetNextLevel().cost)
        {
            monster.IncreaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= monster.CurrentLevel.cost;
        }
    }

}