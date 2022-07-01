using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private  Rigidbody2D rigidbody;
    public float jumpForce;
    private bool levelStart;
    public GameObject gameController;
    private int score;
    public Text scoreText;
    public GameObject message;

    private void Awake()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        levelStart = false;
        rigidbody.gravityScale = 0;
        score = 0;
        scoreText.text = score.ToString();
        message.GetComponent<SpriteRenderer>().enabled = true;
    }
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SoundController.instance.PlaySound("wing", 0.5f);
            if(levelStart == false)
            {
                levelStart = true;
                rigidbody.gravityScale = 5;
                gameController.GetComponent<PipeGenerator>().enableGenratePipe = true;
                message.GetComponent<SpriteRenderer>().enabled = false;
            }
            BirdMoveUp();
        }
    }
    private void BirdMoveUp()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundController.instance.PlaySound("hit", 0.5f);
        ReloadScene();
        score = 0;
        scoreText.text = score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundController.instance.PlaySound("point", 0.5f);
        score += 1;
        scoreText.text = score.ToString();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("_gameplay");
    }
}
