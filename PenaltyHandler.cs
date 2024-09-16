using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyHandler : MonoBehaviour
{
    public GameObject canvas;
    public GameObject shooterArrow;
    public GameObject football;
    public GameObject keeper;
    public GameObject shooter;

    public GameObject backgroundOpponent;
    public GameObject backgroundChallenger;
    public GameObject textMiss;
    public GameObject textGoal;


    public int arrowSpeed = 1;
    public float kickForce = 500f;
    public int arrowUpperLimit = 337;
    public int arrowLowerLimit = -335;

    public float greenZoneMin = -50f;
    public float greenZoneMax = 50f;
    public float orangeZoneMin = -150f;
    public float orangeZoneMax = 150;
    float redZoneMin;
    float redZoneMax;


    public GameObject[] shotMarkers;
    Dictionary<string, Vector3> shotMarkersPositions = new Dictionary<string, Vector3>();

    bool isChallengerTurn = false;
    int challengerScore = 0;
    int opponentScore = 0;
    bool isGameOver = false;

    float arrowPosition;
    bool isArrowMovingUp = true;
    bool shotTaken = false;
    bool isScored = false;


    string goalDirection;
    string keeperDirection;
    string shotPosition;

    IEnumerator ShootoutLoop()
    {
        while (!isGameOver)
        {
            canvas.SetActive(true);
            StartCoroutine(ShotHandler());
            while (!shotTaken)
            {
                yield return null;
            }

            CheckIfScored();
            yield return new WaitForSeconds(4f);
            canvas.SetActive(false);

            // Check for game over
            if (challengerScore == 3 || opponentScore == 3)
            {
                isGameOver = true;
                Debug.Log((challengerScore == 3 ? "Challenger" : "Opponent") + " wins the shootout!");
                yield break;
            }
            else if (isChallengerTurn)
            {
                backgroundChallenger.SetActive(true);
                SetTransitionText();
                SetArrowSpeed(opponentScore);
            }
            else
            {
                backgroundOpponent.SetActive(true);
                SetTransitionText();
                SetArrowSpeed(challengerScore);
            }
            yield return new WaitForSeconds(2f);

            ResetForNextShot();
            isChallengerTurn = !isChallengerTurn;
            shotTaken = false;
            ArduinoDataManager.Instance.ResetButtonStates();
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator ShotHandler()
    {
        Debug.Log("Arrow movement started.");
        while (!ArduinoDataManager.Instance.ButtonAPressed && !ArduinoDataManager.Instance.ButtonBPressed)
        {
            if (isArrowMovingUp)
            {
                arrowPosition += arrowSpeed;
                if (arrowPosition >= arrowUpperLimit)
                    isArrowMovingUp = false;
            }
            else
            {
                arrowPosition -= arrowSpeed;
                if (arrowPosition <= arrowLowerLimit)
                    isArrowMovingUp = true;
            }

            shooterArrow.transform.localPosition = new Vector3(shooterArrow.transform.localPosition.x, arrowPosition, shooterArrow.transform.localPosition.z);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        DetermineShotDirection();
        ProcessShotPosition();

        shooter.GetComponent<Animator>().SetTrigger("Kick");
        yield return new WaitForSeconds(.8f);
        
        KickBall();
        TriggerKeeperAnimation();

        shotTaken = true;
        Debug.Log("Arrow stopped at position: " + arrowPosition);
    }

    void DetermineShotDirection()
    {
        if (ArduinoDataManager.Instance.JoystickDirection == "Left")
        {
            goalDirection = "Left";
        }
        else if (ArduinoDataManager.Instance.JoystickDirection == "Right")
        {
            goalDirection = "Right";
        }
        else
        {
            goalDirection = "Center";
        }
        Debug.Log(goalDirection);

        // Simulate goalkeeper choice
        int randomDirection = Random.Range(0, 3);
        if (randomDirection == 0)
            keeperDirection = "Left";
        else if (randomDirection == 1)
            keeperDirection = "Right";
        else
            keeperDirection = "Center";
    }

    void TriggerKeeperAnimation()
    {
        switch (keeperDirection)
        {
            case "Left":
                keeper.GetComponent<Animator>().SetTrigger("DiveLeft");
                break;
            case "Right":
                keeper.GetComponent<Animator>().SetTrigger("DiveRight");
                break;
            default:
                keeper.GetComponent<Animator>().SetTrigger("CenterBlock");
                break;
        }
    }

    void ProcessShotPosition()
    {
        if (arrowPosition >= greenZoneMin && arrowPosition <= greenZoneMax)
        {
            shotPosition = "green";
        }
        else if (arrowPosition >= orangeZoneMin && arrowPosition <= orangeZoneMax)
        {
            shotPosition = "orange";
        }
        else
        {
            shotPosition = "red";
        }
    }

    void KickBall()
    {
        Vector3 targetPosition = Vector3.zero;
        string targetKey = goalDirection.ToLower() + " " + shotPosition;

        if (shotMarkersPositions.TryGetValue(targetKey, out targetPosition))
        {
            Vector3 direction = (targetPosition - football.transform.position).normalized;
            Rigidbody ballRigidbody = football.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(direction * kickForce);
        }
    }

    void CheckIfScored()
    {
        isScored = false;
        if (shotPosition == "green")
        {
            BoxCollider keeperCollider = keeper.GetComponent<BoxCollider>();
            keeperCollider.enabled = false;
            ScoreGoal();
        }
        else if (shotPosition == "orange")
        {
            if (goalDirection != keeperDirection)
            {
                BoxCollider keeperCollider = keeper.GetComponent<BoxCollider>();
                keeperCollider.enabled = false;
                isScored = true;
                ScoreGoal();
            }
        }
    }

    void ScoreGoal()
    {
        isScored = true;
        if (isChallengerTurn)
        {
            challengerScore++;
        }
        else
        {
            opponentScore++;
        }
    }

    void ResetForNextShot()
    {
        keeper.GetComponent<Animator>().SetTrigger("Reset");
        shooter.GetComponent<Animator>().SetTrigger("Reset");
        ResetFootballPosition();
        BoxCollider keeperCollider = keeper.GetComponent<BoxCollider>();
        keeperCollider.enabled = true;

        arrowPosition = arrowLowerLimit+1;
        isArrowMovingUp = true;
        shooterArrow.transform.localPosition = new Vector3(shooterArrow.transform.localPosition.x, arrowPosition, shooterArrow.transform.localPosition.z);
    }

    void ResetFootballPosition()
    {
        Vector3 originalFootballPosition = new Vector3(14.5f, 18.5f, 199f);  // Replace with the actual starting position
        football.transform.position = originalFootballPosition;

        Rigidbody ballRigidbody = football.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
            Debug.Log("Football position reset and motion stopped.");
        }
        else
        {
            Debug.LogError("The football does not have a Rigidbody component!");
        }
    }

    void SetTransitionText()
    {
        if (isScored) textGoal.SetActive(true);
        else textMiss.SetActive(true);
    }

    void SetArrowSpeed(int nextPlayerScore)
    {
        switch (nextPlayerScore)
        {
            case 0:
                arrowSpeed = 1;
                break;
            case 1:
                arrowSpeed = 4;
                break;
            default:
                arrowSpeed = 6;
                break;
        }
    }


    void Start()
    {
        string[] keys = {
            "left green", "left orange", "left red",
            "center green", "center orange", "center red",
            "right green", "right orange", "right red"
        };

        for (int i = 0; i < shotMarkers.Length; i++)
        {
            if (shotMarkers[i] != null)
            {
                shotMarkersPositions[keys[i]] = shotMarkers[i].transform.position;
            }
        }

        redZoneMin = arrowLowerLimit;
        redZoneMax = arrowUpperLimit;
        arrowPosition = arrowLowerLimit;

        StartCoroutine(ShootoutLoop());
    }
}
