using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CollisionDetection : MonoBehaviour
{

    private int count;
    public TextMeshProUGUI countText;

    public GameObject winText;
    public GameObject winImage;

    public GameObject ReplayButton;
    public GameObject MenuButton;
    public GameObject ExitButton;

    public Animator m_animator;
    void Start()
    {
        count = 0;
        setCountText();
        winText.SetActive(false);
        winImage.SetActive(false);
        ReplayButton.SetActive(false);
        MenuButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count = +count + 1;
            setCountText() ;
        }
        
        if (other.gameObject.CompareTag("End"))
        {
            winText.SetActive(true);
            winImage.SetActive(true);
            ReplayButton.SetActive(true);
            MenuButton.SetActive(true);
            ExitButton.SetActive(true);
        }
        
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    public void ResetLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
