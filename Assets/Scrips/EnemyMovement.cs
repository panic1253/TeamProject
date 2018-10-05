using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    public float movePower = 1f;
    public int enemyType;
    public static int MaxHealthPoint= 3;

    Animator animator;
    GameObject traceTarget;

    Vector3 movement;
    bool isTracing = false;
    bool isDie = false;

    int movementFlag = 0;//0:기본, 1:왼쪽, 2:오른쪽
    public static int HealthPoint = 3;
    

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");

        HealthPoint = MaxHealthPoint;
	}

    void Update()
    {
        
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMovement");
    }

    
	void FixedUpdate () {
        if (HealthPoint > 0)
        {
            Move();
        }
	}

   void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";

        if (isTracing)
        {
            Vector3 playerpos = traceTarget.transform.position;

            if (playerpos.x < transform.position.x)
                dist = "Left";
            else if (playerpos.x > transform.position.x)
                dist = "Right";

        }
        else 
        {
            if(movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }
        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dist == "Right") {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    IEnumerator DefendTime() {
        Debug.Log("무적시간");

        yield return new WaitForSeconds(0.01f);

        yield return null;
    }

    public void Die() {
        StopCoroutine("ChangeMovement");
        isDie = true;

        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        renderer.flipY = true;

        BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
        coll.enabled = false;

        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0, 1f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        Destroy(gameObject, 0.1f);
    }

   void OnTriggerEnter2D(Collider2D other) //추적 시작
    {
        if (enemyType == 0)
            return;

        if (other.gameObject.tag == "Player") {
            traceTarget = other.gameObject;

            StopCoroutine("ChangeMovement");
        }
    }
    void OnTriggerStay2D(Collider2D other)//추적 유지중
    {
        if (enemyType == 0)
            return;

        if (other.gameObject.tag == "Player") {
            isTracing = true;
            animator.SetBool("isMoving", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)//추적 끝
    {
        if (enemyType == 0)
            return;

        if (other.gameObject.tag == "Player")
        {
            isTracing = false;
            animator.SetBool("isMoving", true);
        }
    }

}
