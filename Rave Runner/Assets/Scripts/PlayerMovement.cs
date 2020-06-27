using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public static int score;
	public static int beerCounter;
	public static int cigarettesCounter;
	public static int speed;
	int jumpForce;                
	public static int minJumpForce;
	public static int maxJumpForce;
	public Animator animator;

	public Rigidbody2D rb2d;
	public bool isOnFloor;
	public bool isSquating;

    [SerializeField]
    private BoxCollider2D playerCollider;
    public Vector2 coliderStandingSize;
    public Vector2 coliderSquatingSize;

    // Use this for initialization
    void Start () {
		jumpForce = minJumpForce;
		animator.SetInteger("Speed", speed);
		rb2d.freezeRotation = true;
        score = 0;
        speed = 10;
        minJumpForce = 750;
        maxJumpForce = 1350;

    }
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

		//if (beerCounter > 20)
		//{
		//	SceneManager.LoadScene("Alcohol");
		//}
		//if (cigarettesCounter > 20)
		//{
		//	SceneManager.LoadScene("Cancer");
		//}

		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isOnFloor)
		{
			if (jumpForce < maxJumpForce)
			{
				jumpForce +=50;
			}
			
		}
		if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && isOnFloor)
		{
			rb2d.AddForce(Vector2.up * jumpForce);
			jumpForce = minJumpForce;
		}

		if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
		{
			rb2d.AddForce(Vector2.down * jumpForce/1.5f);
            playerCollider.size = coliderSquatingSize;
            isSquating = true;
			
		} 
		else if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
		{
            playerCollider.size = coliderStandingSize;
            isSquating = false;	
		}

		if ((Input.GetKey("right") || Input.GetKey("d")) && isOnFloor)
		{
			rb2d.velocity = new Vector3(2f,rb2d.transform.localPosition.y,rb2d.transform.localPosition.z);	
		} 
		if ((Input.GetKey("left") || Input.GetKey("a")) && isOnFloor)
		{
			rb2d.velocity = new Vector3(-2f,rb2d.transform.localPosition.y,rb2d.transform.localPosition.z);	
		} 
		
		animator.SetBool("IsOnStage", isOnFloor);
		animator.SetBool("IsSquating", isSquating);
	}

}
