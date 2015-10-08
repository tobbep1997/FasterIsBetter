using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    /// <summary>
    /// CharacterController makes sure that the player can move and jump
    /// </summary>
    public float moveSpeed, jumpHight;
    public float StartTime, StopTime;        //This is the times that it take for the character is at max speed or has stoped

    public float TimeMoved;                 //This is the time since character has moved
    private float currentDir, ChangeDir;     //This are the current direction and if the character changes direction
                                             /*
                                              * These 2 offsets are for checking if the character is running in to a wall
                                              * so after the timeoffset it check if the character has moved DistanceOffset distance
                                              */
    public float TimeOffset, DistanceOffset;
    private bool canMove = true;            //can the character move
    private Vector2 LastPos;                //This is the characters position from the previous game tick

    CharacterSoundController soundController;

    private bool isGrounded;

    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
        }
    }

    public bool canJump = false;
    public bool rayJump = false;
    public bool isJumping;
    private Rigidbody2D body2D;

    private float dir;                      //The players current direction

    public Animator anim;
    private float fPreviousYPos;            //its the character position on the Y-axis the previous game tick


    private bool IsTouch = true;


    void Start()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
            IsTouch = true;
        else
            IsTouch = false;
            body2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fPreviousYPos = transform.position.y;

        soundController = GetComponent<CharacterSoundController>();
    }
    void Update()                           //The reson i use Update instead of fixedupdate is that FixedUpdate is effected by TimeScale
    {
        if (TimeScale.playing == false || TimeScale.timeTicking == false)
        {
            return;
        }
        //Here i call all the functions that are needed for the character to move and jump
        Jump();
        if (IsTouch)
            TouchMoveHorizontal();
        else
            MoveHorizontal();
    }

    void MoveLeft()
    {
        dir = -1;
    }
    void MoveRight()
    {
        dir = 1;
    }
    void ResetDirection()
    {
        dir = 0;
    }
    void MoveHorizontal()                   //This makes sure that the player can move the character in the horizontal directions
    {
        dir = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump();
        if (Input.GetKeyDown(KeyCode.Z))
            EventManager.CallPlayerInteract();

        //This check if the player can and is trying to move then it check what direction its going
        if (canMove)
            if (dir != 0)
            {
                TimeMoved += Time.deltaTime * Time.timeScale;
                currentDir = dir;
            }
            else
                TimeMoved -= Time.deltaTime * Time.timeScale;

        //Check if the player trys to walk but the character cant move and makes sure that the character aint picking up speed
        if (TimeMoved >= TimeOffset)
        {
            if (dir != 0)
            {
                if (CharacterMoved(LastPos, body2D.position, DistanceOffset))
                {
                    canMove = false;
                    TimeMoved = 0;
                }
                else
                    canMove = true;
            }
            else
                canMove = true;
        }
        else
            canMove = true;

        //Checks if the player turns
        if (dir != ChangeDir && dir != 0)
            TimeMoved = 0;
        //Bonderis
        if (TimeMoved >= StartTime)
            TimeMoved = StartTime;
        if (TimeMoved <= 0)
            TimeMoved = 0;
        if (TimeMoved == 0)
            currentDir = 0;
        //Check if the player stops moving and slows the character down
        if (dir == 0 && TimeMoved > StopTime)
            TimeMoved = StopTime;
        //This gives a % value of the speed
        float AccMoveMult = TimeMoved / StartTime;
        //this makes sure that the character is moving
        TurnCharacter();
        //This updates the animations so the character is playing the right animation at the right time
        UpdateAnimation(dir, fPreviousYPos);

        if (currentDir > 0)
            body2D.position += Vector2.right * moveSpeed * AccMoveMult * TimeScale.DeltaTime;
        else if (currentDir < 0)
            body2D.position -= Vector2.right * moveSpeed * AccMoveMult * TimeScale.DeltaTime;



        //Some previousFram Values beeing set
        fPreviousYPos = transform.position.y;
        ChangeDir = dir;
        LastPos = body2D.position;
    }
    void TouchMoveHorizontal()
    {
        if (Input.touchCount > 0)
        {
            bool resetDir = true;
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x >= (Screen.width / 3) * 2 && Input.GetTouch(i).position.y < (Screen.height / 3) * 2)
                {
                    MoveRight();
                    resetDir = false;
                }
                else if (Input.GetTouch(i).position.x <= (Screen.width / 3) && Input.GetTouch(i).position.y < (Screen.height / 3) * 2)
                {
                    MoveLeft();
                    resetDir = false;
                }


                if (Input.GetTouch(i).position.x > (Screen.width / 3) &&
                         Input.GetTouch(i).position.x < (Screen.width / 3) * 2 &&
                         Input.GetTouch(i).position.y < (Screen.height / 3))
                {
                    PlayerJump();
                }
                if (Input.GetTouch(i).position.x > (Screen.width / 3) &&
                         Input.GetTouch(i).position.x < (Screen.width / 3) * 2 &&
                         Input.GetTouch(i).position.y > (Screen.height / 3))
                {
                    EventManager.CallPlayerInteract();

                }
            }
            if (resetDir)
            {
                ResetDirection();
            }
        }
        else
            ResetDirection();

        //This check if the player can and is trying to move then it check what direction its going
        if (canMove)
            if (dir != 0)
            {
                TimeMoved += Time.deltaTime * Time.timeScale;
                currentDir = dir;
            }
            else
                TimeMoved -= Time.deltaTime * Time.timeScale;

        //Check if the player trys to walk but the character cant move and makes sure that the character aint picking up speed
        if (TimeMoved >= TimeOffset)
        {
            if (dir != 0)
            {
                if (CharacterMoved(LastPos, body2D.position, DistanceOffset))
                {
                    canMove = false;
                    TimeMoved = 0;
                }
                else
                    canMove = true;
            }
            else
                canMove = true;
        }
        else
            canMove = true;

        //Checks if the player turns
        if (dir != ChangeDir && dir != 0)
            TimeMoved = 0;
        //Bonderis
        if (TimeMoved >= StartTime)
            TimeMoved = StartTime;
        if (TimeMoved <= 0)
            TimeMoved = 0;
        if (TimeMoved == 0)
            currentDir = 0;
        //Check if the player stops moving and slows the character down
        if (dir == 0 && TimeMoved > StopTime)
            TimeMoved = StopTime;
        //This gives a % value of the speed
        float AccMoveMult = TimeMoved / StartTime;
        //this makes sure that the character is moving
        TurnCharacter();
        //This updates the animations so the character is playing the right animation at the right time
        UpdateAnimation(dir, fPreviousYPos);

        if (currentDir > 0)
            body2D.position += Vector2.right * moveSpeed * AccMoveMult * TimeScale.DeltaTime;
        else if (currentDir < 0)
            body2D.position -= Vector2.right * moveSpeed * AccMoveMult * TimeScale.DeltaTime;

        //Some previousFram Values beeing set
        fPreviousYPos = transform.position.y;
        ChangeDir = dir;
        LastPos = body2D.position;
    }
    void TurnCharacter()//This turns the character so the character cant run backwards
    {
        if (currentDir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (currentDir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Jump()//Allows the character to jump
    {
        //Checks if the character is curently jumping
        if (isJumping)
        {
            soundController.JumpSound();
            bool pressingJump = false;
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.x < (Screen.width / 3) * 2 && Input.GetTouch(i).position.x > (Screen.width / 3))
                {
                    pressingJump = true;
                }
                if (pressingJump)
                {
                    isJumping = true;
                }
                else
                    isJumping = false;
            }
            if (Input.touchCount == 0)
            {
                isJumping = false;
            }
        }
    }
    public void PlayerJump()
    {
        if (canJump && isJumping == false && rayJump == false)
        {
            body2D.AddForce(Vector2.up * jumpHight * TimeScale.fTimeScale);
            isJumping = true;
            canJump = false;
        }
    }
    void UpdateAnimation(float dir, float PreviousYPos)//This update all the varibles and parameters to the animator of the character
    {
        //All this values are sent to the animator
        //Check if the player is running 		
        if (dir != 0)
        {
            anim.SetBool("Running", true);
        }
        else
            anim.SetBool("Running", false);

        anim.SetBool("Grounded", isGrounded);
        //checks if the character is grounded
        if (isGrounded == false)
        {
            //if the character is in the air this checks if the character is falling or jumping
            //This checks if the character is jumping
            if (transform.position.y > PreviousYPos)
            {
                anim.SetBool("Jumping", true);
            }
            else
                anim.SetBool("Jumping", false);
            //This checks if the character is falling
            if (transform.position.y < PreviousYPos)
            {
                anim.SetBool("Falling", true);
            }
            else
                anim.SetBool("Falling", false);
        }
        else
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }
    }
    private bool CharacterMoved(Vector2 Start, Vector2 End, float Offset)//The return a true if the character has moved
    {
        //this checks if the player is on the at the same possition with in a small offset
        if (CheckIfWithin(Start.x, End.x, Offset))
        {
            if (CheckIfWithin(Start.y, End.y, Offset))
            {
                return false;
            }
            return false;
        }
        return true;
    }
    private bool CheckIfWithin(float inputFloat, float CheckFloat, float OffsetFloat)//This retunrs true if the inputFloat in within the bounduris of checkFloat +-Offsetfloat
    {
        if (inputFloat > CheckFloat - OffsetFloat)
        {
            if (inputFloat < CheckFloat + OffsetFloat)
            {
                return true;
            }
            return false;
        }
        return false;
    }


}
