using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdPlayerScript : MonoBehaviour
{
    private const float JUMP_HEIGHT = 0.5f;

    private const float SECONDS_TO_WAIT_AFTER_DEATH = 0.9f;
    private const string JUMP_KEY = "space";
    private const float GRAVITY = -0.4f;
    private const float THRUST = 5.1f;

    private const float LOWER_HEIGHT_LIMIT = -7.5f;
    private const float UPPER_HEIGHT_LIMIT = 7.5f;
    private const string COLLISION_OBJECT_TAG = "Pipe";
    private const string PASSAGE_OBJECT_TAG = "Passage";
    private const string MENU_SCENE_NAME = "MenuScene";
    private const float MOVE_RIGHT_INCREASE_SPEED = 0.2f; 
    private float m_MoveRightSpeed = 4.5f;

    public Rigidbody m_Rigidbody;
    public GameObject m_PipeSystem;
    public ScoreManager m_ScoreManager;
    
    void EndGame()
    {
        m_ScoreManager.SaveScore();
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }
    void MoveRight() 
    {
        transform.position += Vector3.right * Time.deltaTime * m_MoveRightSpeed;
    }
    void CheckFail()
    {
        if(transform.position.y < LOWER_HEIGHT_LIMIT || transform.position.y > UPPER_HEIGHT_LIMIT)
        {
            EndGame();
        }
    }
    void Update()
    {
        CheckFail();
        MoveRight();
        if (Input.GetKeyDown(JUMP_KEY))
        {
            m_Rigidbody.AddForce(Vector3.up * THRUST, ForceMode.Impulse);
        }
        
    }
    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == COLLISION_OBJECT_TAG)
        {
            yield return new WaitForSeconds(SECONDS_TO_WAIT_AFTER_DEATH);
            m_PipeSystem.SetActive(false);
            EndGame();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PASSAGE_OBJECT_TAG)
        {
            m_ScoreManager.IncreaseScore();
            m_MoveRightSpeed += MOVE_RIGHT_INCREASE_SPEED;
        }
    }
}
