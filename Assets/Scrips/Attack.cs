using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    CircleCollider2D weapon1Range;
    bool isAttack = false;
    Animator animator;
    
    SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        weapon1Range = gameObject.GetComponentInChildren<CircleCollider2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)&&(isAttack == false)) {
            Debug.Log("공격 시작");
            weapon1Range.enabled = true;
            isAttack = true;
            animator.SetBool("isAttack",true);
         }
        if (Input.GetMouseButtonUp(0)) {
            Debug.Log("공격 끝");
            weapon1Range.enabled = false;
            isAttack = false;
            animator.SetBool("isAttack", false);
        }
		
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !collider.isTrigger && isAttack == true)
            {
            Debug.Log("1데미지");
            EnemyMovement.HealthPoint--;
            isAttack = true;
            if (EnemyMovement.HealthPoint == 0)
            {
                EnemyMovement enemy = collider.gameObject.GetComponent<EnemyMovement>();
                enemy.Die();
                EnemyMovement.HealthPoint = EnemyMovement.MaxHealthPoint;
            }
        }
    }
}
