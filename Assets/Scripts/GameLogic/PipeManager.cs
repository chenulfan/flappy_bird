using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour {

    private const float DELAY_TIME_BEFORE_DESTROY = 9.2f;
    private const float DISTANCE_BETWEEN_PIPES_LOWER_BOUND = 9.5f;
    private const float DISTANCE_BETWEEN_PIPES_HIGHER_BOUND = 12f;
    private const float X_INSTANTIATE_DISTANCE = 10.4f;
    private const float RANDOM_POSITION_Y_LOWER_BOUND = -8.5f;
    private const float MIN_WAIT_TIME_FOR_SPAWNING = 1.5f;
    private const float RANDOM_POSITION_Y_HIGHER_BOUND = -2f;
    private const float HIGHER_PIPE_POSITION_Y = 7f;
    private const float PASSAGE_Y_SCALE = 200f;
    private const float SPAWNING_LEVEL_UP = 0.25f;
    private const int Z_POSITION = 0;
    private float m_waitTimeForSpawning = 5f;
    private float m_Time;
    public GameObject m_LowerPipe;
    public GameObject m_HigherPipe;
    public GameObject m_Passage;
    public GameObject m_Bird;

    float GetPipeSystemSpawnXPosition(){
        float spawnXPosition = m_Bird.transform.position.x + X_INSTANTIATE_DISTANCE;
        return spawnXPosition;
    }
    Vector3 GetLowerPipeVector(float i_SpawnXPosition) {
        float yPosition = Random.Range(RANDOM_POSITION_Y_LOWER_BOUND, RANDOM_POSITION_Y_HIGHER_BOUND);
        return new Vector3(i_SpawnXPosition, yPosition, Z_POSITION);
    }
    Vector3 GetHigherPipeVector(Vector3 LowerPipeVector){
        float distanceBetweenPipes = Random.Range(DISTANCE_BETWEEN_PIPES_LOWER_BOUND, DISTANCE_BETWEEN_PIPES_HIGHER_BOUND);
        float yPositionRespectiveToLower = LowerPipeVector.y + distanceBetweenPipes;
        return new Vector3(LowerPipeVector.x, yPositionRespectiveToLower, LowerPipeVector.z);
    }
    void UpdateSpawningTimeDelta()
    {
        if(m_waitTimeForSpawning > MIN_WAIT_TIME_FOR_SPAWNING) 
        {
            m_waitTimeForSpawning -= SPAWNING_LEVEL_UP;
        }
    }
    void Update()
    {
        m_Time += UnityEngine.Time.deltaTime;

        if(m_Time > m_waitTimeForSpawning) 
        {
            m_Time = m_Time - m_waitTimeForSpawning;    

            float SpawnXPosition = GetPipeSystemSpawnXPosition();
            Vector3 lowerPipeVector = GetLowerPipeVector(SpawnXPosition);
            Vector3 higherPipeVector = GetHigherPipeVector(lowerPipeVector);
            Vector3 passageVector = new Vector3(SpawnXPosition, 0, Z_POSITION);
            
            GameObject newLowerPipe =  Instantiate(m_LowerPipe, lowerPipeVector, Quaternion.identity, transform);
            GameObject newPassage =  Instantiate(m_Passage, passageVector, Quaternion.identity, transform);
            GameObject newHigherPipe =  Instantiate(m_HigherPipe, higherPipeVector, Quaternion.identity, transform);

            UpdateSpawningTimeDelta();
            PipeSystemSelfDestruct(newLowerPipe, newPassage, newHigherPipe);
        }
    }

    void PipeSystemSelfDestruct(GameObject i_lowerPart, GameObject i_passage, GameObject i_upperPipe) {
        Destroy( i_lowerPart, DELAY_TIME_BEFORE_DESTROY);
        Destroy( i_passage, DELAY_TIME_BEFORE_DESTROY);
        Destroy( i_upperPipe, DELAY_TIME_BEFORE_DESTROY);
    }
}
