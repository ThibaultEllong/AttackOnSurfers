using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



[System.Serializable]

public enum SIDE { Left, Mid, Right }
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, None }
public enum HitUp { Up, Mid, Down, None }

public enum HitZ { Forward, Mid, Backward, None }
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float SpeedDodge;
    public float fwdSpeed = 7f;
    public float XValue = 5;
    float NewXPos = 0f;
    
    public Rigidbody rb;

    public int hp;
    public TextMeshProUGUI lifeText;

    public float JumpPower = 7f;
    private bool InJump;
    private bool InRoll;

    internal float RollCounter;

    private float ColHeight;
    private float ColCenterY;

    private float y;
    private float x;

    public SIDE m_Side = SIDE.Mid;
    public HitX m_HitX = HitX.None;
    public HitY m_HitY = HitY.None;
    public HitZ m_HitZ = HitZ.None;
    public HitUp m_HitUp = HitUp.None;

    private bool stopAllStates = false;

    private bool CanInput = true;

    public Collider colCollider;

    public GameObject LoseText;
    public GameObject redDeath;
    public GameObject ReplayButton;
    public GameObject MenuButton;
    public GameObject ExitButton;




    private bool swipeLeft, swipeRight, swipeUp, swipeDown;



    private Animator _animator;
    private CharacterController m_char;


    // Update is called once per frame

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_char = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
        transform.position = Vector3.up;
        ColCenterY = m_char.center.y;
        ColHeight = m_char.height;
        LoseText.SetActive(false);
        redDeath.SetActive(false);
        hp = 10;
        setLifeText();

        ReplayButton.SetActive(false);
        MenuButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    private void Update()
    {
        colCollider.isTrigger = !CanInput;
        if (!CanInput)
        {
            m_char.Move(Vector3.down * 10f * Time.deltaTime);
            return;
        }
            swipeLeft = Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow) && CanInput;
            swipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && CanInput;
            swipeUp = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && CanInput;
            swipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && CanInput;
        
       

        if(hp == 0)
        {
            DeathPlayer("Death");
        }
      
        if (swipeLeft && !InRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
                _animator.Play("dodgeLeft");
            }
            else if (m_Side == SIDE.Right && m_char.isGrounded)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
                _animator.Play("dodgeLeft");
            }
            else if (m_Side == SIDE.Right && m_char.isGrounded == false)
            {
                if (m_char.velocity.y < 0.1f)
                {
                    NewXPos = 0;
                    m_Side = SIDE.Mid;

                } else
                {
                    NewXPos = -XValue;
                    m_Side = SIDE.Left;

                }
            }
        }
        else if (swipeRight && !InRoll)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;

                _animator.Play("dodgeRight");
            }
            else if (m_Side == SIDE.Left && m_char.isGrounded)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;

                _animator.Play("dodgeRight");
            }
            else if (m_Side == SIDE.Left && m_char.isGrounded == false)
            {
                if (m_char.velocity.y < 0.1f)
                {

                    NewXPos = 0;
                    m_Side = SIDE.Mid;

                }
                else
                {
                    NewXPos = XValue;
                    m_Side = SIDE.Right;
                }

            }
        }
        if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1){
            stopAllStates = false;
        }
        Vector3 vectorMove = new Vector3(x - transform.position.x, y * Time.deltaTime, fwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * 10f);
        Jump();
        Slide();
        m_char.Move(vectorMove);
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            Debug.Log("End of the Game !");
            _animator.Play("RunStop");
            fwdSpeed = 0 ;
            
        }
    }

    public void Jump()
    {
        if (m_char.isGrounded)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                InJump = false;
            }
            if(swipeUp)
            {
                if(m_Side == SIDE.Left || m_Side == SIDE.Right)
                {
                    JumpPower = 20f;
                }
                else
                {
                    JumpPower = 30f;
                }
                y = JumpPower;
                InJump = true;
                _animator.Play("Jumping");
            }

        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime;
            if (m_char.velocity.y < -0.1f)
            {
                ///_animator.Play("Falling");
            }
        }
    }

    public void Slide()
    {
        RollCounter -= Time.deltaTime;
        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0, ColCenterY, 0);
            m_char.height = ColHeight;
            InRoll = false;
            
        }
        if (swipeDown)
        {
            RollCounter = .8f;
            y = 0f;
            _animator.CrossFadeInFixedTime("Slide", 0.1f);
            m_char.center = new Vector3(0, ColCenterY / 2f, 0);
            m_char.height = 1;
            InRoll = true;
            InJump = false;

        }
    }

    public void playAnimation(string anim)
    {
        if(stopAllStates)
        {
            return;
        }
        _animator.Play(anim);
    }

    public IEnumerator DeathPlayer(string anim)
    {
        LoseText.SetActive(true);
        redDeath.SetActive(true);
        stopAllStates = true;
        _animator.Play(anim);
        yield return new WaitForSeconds(0.2f);
        CanInput = false;
        ///ResetLevel();
        ReplayButton.SetActive(true);
        MenuButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    public void Stumble(string anim)
    {
        _animator.ForceStateNormalizedTime(0.0f);
        stopAllStates = true;
        _animator.Play(anim);
        if(hp == 0)
        {
            StartCoroutine(DeathPlayer(anim));
            return;
        }
        lifeMinusOne();
        
    }
   

    public void OnCharacterColliderHit(Collider col)
    {
        m_HitX = GetHitX(col);
        m_HitY = GetHitY(col);
        m_HitZ = GetHitZ(col);
  
        if(m_HitZ == HitZ.Forward && m_HitX == HitX.Mid)
        {
            if(m_HitY == HitY.Down)
            {
                
                if (col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Building"))
                {
                    if(InRoll == false)
                    {
                        Debug.Log("Down Dead");

                        StartCoroutine(DeathPlayer("Death"));
                    }
                    else
                    {
                        Debug.Log("Down Alive");
                    }
                   
                }


            } 

            else if(m_HitY == HitY.Mid)

            {
                
                if (col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Building"))
                {
                    StartCoroutine(DeathPlayer("Death"));
                    Debug.Log("Mid");

                }

            } 

            else if(m_HitY == HitY.Up)

            {
                if(InRoll == false)
                {
                    if (col.gameObject.CompareTag("Obstacle")) //Slide Obstacle not avoided
                    {
                        StartCoroutine(DeathPlayer("Death"));
                        Debug.Log("Up");
                    }
                }
                else //Slide Obstacle avoided
                {
                    Debug.Log("Alive");
                    Debug.Log("Up");
                }
                

            }
        } else if(m_HitZ == HitZ.Backward || m_HitZ == HitZ.Mid)
        {
            if (m_HitX == HitX.Right && col.gameObject.CompareTag("Obstacle"))
            {
                Stumble("Stumble Right");
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
            else if(m_HitX == HitX.Left && col.tag == "Obstacle")
            {
                Stumble("Stumble Left");
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
            
        }
    }

    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x + max_x) / 2f - col_bounds.min.x;
        HitX hit;
        if (average > col_bounds.size.x - 0.33f)
        {
            hit = HitX.Left;
        }
        else if (average < 0.33f)
        {
            hit = HitX.Right;
        }
        else
        {
            hit = HitX.Mid;
        }
        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - col_bounds.min.y)/char_bounds.size.y;
        ///Debug.Log("Hit:"+ average + " Collider:" + col.tag);
        HitY hit;
        if (average < 0.33f)
        {
            hit = HitY.Up;
        }
        else if (average < 0.66f)
        {
            hit = HitY.Mid;
        }
        else
        {
            hit = HitY.Down;
        }
        return hit;
    }

    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - col_bounds.min.z) / char_bounds.size.z;
        HitZ hit;
        if (average < 0.33f)
        {
            hit = HitZ.Forward;
        }
        else if (average < 0.66f)
        {
            hit = HitZ.Mid;
        }
        else
        {
            hit = HitZ.Backward;
        }
        return hit;
    }


    void lifeMinusOne()
    {
        hp -= 1;
        setLifeText();
    }

    void setLifeText()
    {
        lifeText.text = "HP: " + hp.ToString();
    }

    public void ResetLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}



