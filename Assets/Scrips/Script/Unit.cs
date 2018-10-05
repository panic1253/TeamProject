using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float speed = 1f; // 이동속도
	public int health = 5;   // 체력
	public int atK = 1;      // 공격력

    public float atkSpeed = 1.0f; // 공격속도

    public bool isDie;// 사망하였는가 체크

    private GameObject enemy; // 가져올 적
    private Rigidbody2D unitRigid; // 현재 개체의 리지드바디(이동 관련에 사용)

    public int enemyHealth;

    void Start()
    {
        unitRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }


    public void Move()// 유닛이 생성되고 이동하는 것
	{
        Vector2 moveVelocity = unitRigid.velocity;

        if (gameObject.tag == "Enemy")
        {
            moveVelocity = new Vector2(-speed, 0);// 적일 시 반대 방향(아군쪽)
        }
        else
        {
            moveVelocity = new Vector2(speed, 0);
        }
        unitRigid.velocity = moveVelocity;
	}

	public void Damage()
	{
		health = health - atK;

        if(health <=0)
        {
            isDie = true;
            speed = 0f;
            StopCoroutine("UnitAttack");
            //죽음 애니메이션 실행
            Destroy(this.gameObject, 1f);
        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
         if (other.gameObject.tag == "Enemy")// 적 감지
         {
            enemy = other.gameObject;
            speed = 0;// speed를 0으로 만들어 움직이지 않게한다.

            if (this.isDie != true)//나는 죽지 않았는가
            {
                StartCoroutine("UnitAttack");// 공격 코루틴 시작
                if (enemy.GetComponent<Unit>().isDie == true)//죽었는가?
                {
                    StopCoroutine("UnitAttack");
                    speed = 1f;// 코루틴이 끝나고, speed가 1이 되어 다시 움직인다.
                }
                
            }
         }
        
    }

    IEnumerator UnitAttack()
    {
        while(true)
        {
            enemy.gameObject.GetComponent<Unit>().Damage();//해당 함수의 Damage를 이용함.
            yield return new WaitForSeconds(atkSpeed);// 공격속도
        }
    }
}
