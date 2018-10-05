using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movePower = 1f;
    public float jumpPower = 1f;
    public int maxHealth = 3;
    float DashTime;
    public float speed = 3.0f;

    Rigidbody2D rigid;
    Animator animator;
    public SpriteRenderer spriteRenderer;
    

    Vector3 movement;
    bool isJumping = false;
    bool isDie = false;
    bool isUnBeatTime = false;
    bool isLeftDash = false;
    bool isRightDash = false;
    bool notMoreJump = false;
    bool dashReset = false;
    bool AdoubleTap = false;
    bool DdoubleTap = false;

    public static int health = 3;
    int jumpCount = 0;//2단 점프 체크용
    


    //overRide function
    //Initialization
    void Start() {
        
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        health = maxHealth;
        DashTime = 0f;
       // Time.timeScale = 1.0f;


    }//end start

    //grapic & Input Updates
    void Update() {
        if (health == 0) {
            if (!isDie)
            {
                Die();
            }

            return;
        }

        if(Input.GetAxisRaw("Horizontal")== 0) {
            animator.SetBool("isMoving", false);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0){
            animator.SetBool("isMoving", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            animator.SetBool("isMoving", true);
        }

        if (Input.GetButtonDown("Jump")&& (!animator.GetBool("isJumping")||(animator.GetBool("isJumping")&&jumpCount <=2)))//점프 중이 아니거나 점프중일때 점프카운트가2 이하이면 
        {
            jumpCount++;
            isJumping = true;
            notMoreJump = true;
            animator.SetBool("isJumping", true);//Flag
            animator.SetTrigger("doJumping");//Animation
            animator.SetFloat("JumpUpDown", rigid.velocity.y);

            if (jumpCount > 2) {
                isJumping = false;
                if(notMoreJump == false)
                jumpCount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.A)&&AdoubleTap) {//a가 짧은 시간내에 두번 되었는지
            Debug.Log("a 대시준비");
            if (Time.time - DashTime < 0.3f)
            {
                Debug.Log("a 대시");
                isLeftDash = true;
                DdoubleTap = false;
            }
            else Debug.Log("a 대시 늦음");

            dashReset = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && !AdoubleTap) {
            Debug.Log("a 입력");
            AdoubleTap = true;
            DashTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D) && DdoubleTap){//d가 짧은 시간내에 두번 되었는지
            Debug.Log("d 대시준비");
            if (Time.time - DashTime < 0.3f)
            {
                Debug.Log("d 대시");
                isRightDash = true;
                AdoubleTap = false;
            }
            else Debug.Log("d 대시 늦음");

            dashReset = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && !DdoubleTap)
        {
            Debug.Log("d 입력");
            DdoubleTap = true;
            DashTime = Time.time;
        }

        if (dashReset) {
            AdoubleTap = false;
            DdoubleTap = false;
            dashReset = false;
        }

    }//end update

    //Physics engine updates
    void FixedUpdate()
    {
        if (health > 0)
        {
            Move();
            Jump();
            Dash();
        }
    }

    //움직임 기능
    void Move()
    {

        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal")<0){
            moveVelocity = Vector3.left;
            
            transform.localScale = new Vector3(-1, 1, 1);
            
        }

        else if(Input.GetAxisRaw("Horizontal")> 0)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    

    }
    void Dash()
    {
        if (isLeftDash) {
            transform.Translate(-speed*2 * Time.deltaTime, 0.01f, 0.0f);
            DashTime = 0f;
            isLeftDash = false;
        }

        if (isRightDash)
        {
            transform.position += new Vector3(speed*2*Time.deltaTime, 0.01f, 0.0f);
            DashTime = 0f;
            isRightDash = false;
        }
    }

    void Die() {
        Debug.Log("죽음");
        isDie = true;

        rigid.velocity = Vector2.zero;

        animator.SetBool("IsDie", true);

        Vector2 dieVelocity = new Vector2(-1, 1f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("Restart", 2f);//해당 메소드를 몇초 후에 사용
}

    void Restart() {
        GameManager.Restart();
        isDie = false;
        health = maxHealth;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        

        isJumping = false;
    }

 void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 0 && rigid.velocity.y < 0) {
            rigid.velocity = Vector2.zero;
            animator.SetBool("isJumping", false);
            jumpCount = 0;//한번만 점프하면 점프카운트가 1이 된 문제해결
            notMoreJump = false;
        }
        if (other.gameObject.tag == "End") {
            other.enabled = false;
            GameManager.EndGame();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && !(rigid.velocity.y < -0.1f) && !isUnBeatTime)//&& !other.isTrigger 
        {
            Debug.Log("피격");
            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                attackedVelocity = new Vector2(-0.5f, 2f);
                Debug.Log("피격2");
                animator.SetBool("isDamaged", true);
            }
            else
            {
                Debug.Log("피격3");
                attackedVelocity = new Vector2(0.5f, 2f);
                animator.SetBool("isDamaged", true);
            }
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

            health--;

            if (health > 1)
            {
                Debug.Log("피격4");
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
                
            }
        }
    }
    IEnumerator UnBeatTime() {
        Debug.Log("무적시간");
        int countTime = 0;

        while(countTime < 10){
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        isUnBeatTime = false;

        animator.SetBool("isDamaged", false);

        yield return null;
    }
   
}
