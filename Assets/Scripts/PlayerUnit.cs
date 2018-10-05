using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour {

    [Header("이동 관련 변수")]
    public int playerUnitSpeed = 1;                    //플레이어 유닛 이동속도
    public Rigidbody2D playerUnitRb;                   //플레이어 유닛의 리지드바디
    
    [Header("스텟 관련 변수")]
    public int playerUnitHp = 10;                      //플레이어 유닛 체력
    public int playerUnitAttack = 1;                   //플레이어 유닛 공격력

    [Header("Enemy 관련 변수")]
    public GameObject enemyUnit;                       //적 유닛
    public int enemyUnitHp;                            //적 유닛 체력
    public int enemyUnitAttack;                        //적 유닛 공격력
    public GameObject enemyNexus;                      //적 넥서스
    public int enemyNexusHp;                           //적 넥서스 체력

    [Header("Enemy 관련 변수")]
    public bool playerisDie = false;                   //플레이어 유닛 죽음체크

    private void Start()
    {
        playerUnitRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        StartCoroutine("Move");
    }

    //이동
    IEnumerator Move()
    {
        transform.Translate(Vector3.right * playerUnitSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyUnit")        //적 유닛 체크
        {
            enemyUnit = other.gameObject;
            //enemyUnitHp = other.GetComponent<EnemyUnit>().enemyUnitHp;
            //enemyUnitAttack = other.GetComponent<EnemyUnit>().enemyUnitAttack;
        }
        else                                           //적 넥서스 체크
        {
            enemyNexus = other.gameObject;
            //enemyNexusHp = other.GetComponent<EnemyNexus>().enemyNexusHp;
        }
    }

    IEnumerator Damage()
    {

        yield return new WaitForSeconds(1f);
    }

}
