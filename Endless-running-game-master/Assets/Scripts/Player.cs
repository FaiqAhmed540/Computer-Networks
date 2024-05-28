using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public CharacterController controller;

    public Camera c1;

    public static Camera c1s;

    public Text Timer;
    public Text Counter;

    public Slider invisibleMoodBar;
    public Canvas canvas;
    public GameObject menu;




    private float invisibleMoodTimer;
    private bool isInvisableMoodActive;
    private float invisableMoodCounter;

    private static bool isAlive;
    public static bool isPaused;

    private int desiredLane;
    private float distanceBetweenLanes;


    private float distance;


    private static float speed;
    private float gravity;
    private float jumpForce;
    private float verticalVelocity;

    private float animationDuration;

    private float timer;




    public static bool IsPlayerAlive()
    {
        return isAlive;
    }

    


    void Start()
    {

        c1s = c1;


        distance = 0;

     invisibleMoodTimer = 0.0f;
    isInvisableMoodActive = false;
    invisableMoodCounter = 0.0f;

    isAlive = true;
    isPaused = false;

    desiredLane = 1;
    distanceBetweenLanes = 7.5f;


    speed = 25.0f;
    gravity = 35.0f;
    jumpForce = 15f;
    verticalVelocity = 0;

    animationDuration = 3.0f;

    timer = 60;

    controller = GetComponent<CharacterController>();

        invisibleMoodBar.value = invisableMoodCounter / 50f;

        Timer.text = timer.ToString();


        Transform newTrans = SpawnManager.instance.getspawnpoint();
            transform.position=newTrans.position;
    }


    void Update()
    {

        if (Time.time < animationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }




        if (isPaused || !isAlive)
        {
            return;
        }




        timer -= Time.deltaTime;
            Timer.text = Mathf.Round(timer).ToString();
            CheckIfPlayerIsAlive();

        distance += speed * Time.deltaTime*0.1f;
        Counter.text = Mathf.Round(distance).ToString();


        if (isInvisableMoodActive)
        {
            if(invisibleMoodTimer > 0)
            {
                invisibleMoodTimer -= Time.deltaTime;
                invisibleMoodBar.value = invisibleMoodTimer / 5;
            }
            else
            {
                invisibleMoodTimer = 0;
                invisableMoodCounter = 0;
                isInvisableMoodActive = false;
                speed /= 2;
            }
        }


        if ( (Input.GetKeyDown(KeyCode.LeftArrow)) || (SwipeInput.Instance.SwipeLeft))
            MoveLane("Left");

        if ((Input.GetKeyDown(KeyCode.RightArrow)) || (SwipeInput.Instance.SwipeRight))
            MoveLane("Right");
    
         Vector3 targetPosition = transform.position.z * Vector3.forward;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * distanceBetweenLanes;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * distanceBetweenLanes;
        }

        Vector3 moveVector = Vector3.zero;

        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        if(controller.isGrounded)
        {
            verticalVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space) || (SwipeInput.Instance.SwipeUp))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.DownArrow) || (SwipeInput.Instance.SwipeDown) )
            {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move((moveVector) * Time.deltaTime);

    }

    private void CheckIfPlayerIsAlive()
    {
        if (timer <= 0)
        {
            isAlive = false;
            speed = 0;
            StartCoroutine(ThePlayerIsDied());
        }
    }


    private void MoveLane(string lane)
    {
        if(lane == "Right")
        {
            if(desiredLane != 2)
            desiredLane++;
        }
        else if(lane == "Left")
        {
            if (desiredLane != 0)
                desiredLane--;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider collision = hit.collider;

            if (collision.gameObject.tag == "Coin")
            {
                timer += 2;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "RedSphere")
            {
            if (!isInvisableMoodActive)
            {
                isAlive = false;
                speed = 0;
                StartCoroutine(ThePlayerIsDied());
            }
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "BlueSphere")
            {

             Destroy(collision.gameObject);

            if (!isInvisableMoodActive)
            {

                invisableMoodCounter += 10;
                invisibleMoodBar.value = invisableMoodCounter / 50f;

                if (invisableMoodCounter >= 50)
                {
                    invisableMoodCounter = 0;
                    invisibleMoodTimer = 5.0f;
                    isInvisableMoodActive = true;
                    speed *= 2;
                }
            }
            

            }
            else if (collision.gameObject.tag == "GreySphere")
            {
                Destroy(collision.gameObject);
            if (!isInvisableMoodActive)
            {
                timer -= 10;
                CheckIfPlayerIsAlive();
            }

            }
       

    }
   
    IEnumerator ThePlayerIsDied()
    {
        canvas.gameObject.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

}

