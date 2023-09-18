using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public float startTime = 20f;
    private float timer;
    public AudioClip soundMoeda;
    private AudioSource audioSource;
    

    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = startTime;
        rb = GetComponent<Rigidbody>();  
        count = 0;
        SetCountText();
        SetTimerText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            SceneManager.LoadScene("DeadEnd");
        }
    }

    void SetTimerText()
    {
        timerText.text = "Time: " + timer.ToString("F2");
        if(timer <= 0)
        {
            SceneManager.LoadScene("DeadEnd");
        }
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        timer -= Time.deltaTime;
        SetTimerText();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            audioSource.PlayOneShot(soundMoeda);
        }
    }
}
