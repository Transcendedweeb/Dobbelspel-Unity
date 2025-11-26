using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyHandler : MonoBehaviour
{
    public GameObject parentHolder;
    public GameObject startCamera;
    public GameObject canvas;
    public GameObject shooterArrow;
    public GameObject football;
    public GameObject keeper;
    public GameObject shooter;

    public GameObject backgroundChallenger;
    public GameObject backgroundOpponent;
    public GameObject textChallengerTurn;
    public GameObject textOpponentTurn;
    public GameObject textChallengerWin;
    public GameObject textOpponentWin;


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

    string goalDirection;
    string keeperDirection;
    string shotPosition;

    public AudioClip[] announcerGoal;
    public AudioClip[] announcerSave;
    public AudioClip[] announcerMiss;
    AudioSource audioSource;
    ChangeCamera changeCamera;

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
            GetAnnouncerClip();
            yield return new WaitForSeconds(4f);
            canvas.SetActive(false);

            if (challengerScore == 3 || opponentScore == 3)
            {
                isGameOver = true;
                if (challengerScore == 3) textChallengerWin.SetActive(true);
                else textOpponentWin.SetActive(true);
                ArduinoDataManager.Instance.ResetButtonStates();
                StartCoroutine(ReturnToMenu());
                yield break;
            }
            else if (isChallengerTurn)
            {
                backgroundOpponent.SetActive(true);
                SetTransitionText();
                SetArrowSpeed(opponentScore);
            }
            else
            {
                backgroundChallenger.SetActive(true);
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
    }

    void DetermineShotDirection()
    {
        // Determine the goal direction based on joystick input
        if (ArduinoDataManager.Instance.JoystickDirection == "Left" || ArduinoDataManager.Instance.JoystickDirection == "LeftUp" || ArduinoDataManager.Instance.JoystickDirection == "LeftDown")
        {
            goalDirection = "Left";
        }
        else if (ArduinoDataManager.Instance.JoystickDirection == "Right" || ArduinoDataManager.Instance.JoystickDirection == "RightUp" || ArduinoDataManager.Instance.JoystickDirection == "RightDown")
        {
            goalDirection = "Right";
        }
        else
        {
            goalDirection = "Center";
        }

        int randomDirection = Random.Range(0, 2);
        Debug.Log(randomDirection);
        if (randomDirection == 0)
        {
            keeperDirection = goalDirection;
        }
        else
        {
            if (goalDirection == "Left")
            {
                keeperDirection = Random.value < 0.5f ? "Right" : "Center";
            }
            else if (goalDirection == "Right")
            {
                keeperDirection = Random.value < 0.5f ? "Left" : "Center";
            }
            else
            {
                keeperDirection = Random.value < 0.5f ? "Left" : "Right";
            }
        }
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
                ScoreGoal();
            }
        }
    }

    void ScoreGoal()
    {
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

    void ResetGameObjects()
    {
        startCamera.SetActive(true);
        backgroundOpponent.SetActive(true);
        textOpponentTurn.SetActive(true);
        canvas.SetActive(true);
        textChallengerWin.SetActive(false);
        textOpponentWin.SetActive(false);
    }

    void ResetVariables()
    {
        challengerScore = 0;
        opponentScore = 0;
        isGameOver = false;

        arrowPosition = arrowLowerLimit;
        shotTaken = false;
        isChallengerTurn = false;

        arrowSpeed = 1;
    }


    void ResetFootballPosition()
    {
        Vector3 originalFootballPosition = new Vector3(14.5f, 18.5f, 199f);
        football.transform.position = originalFootballPosition;

        Rigidbody ballRigidbody = football.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector3.zero;
            ballRigidbody.angularVelocity = Vector3.zero;
        }
    }

    void SetTransitionText()
    {
        if (isChallengerTurn) textOpponentTurn.SetActive(true);
        else textChallengerTurn.SetActive(true);
    }

    void SetArrowSpeed(int nextPlayerScore)
    {
        switch (nextPlayerScore)
        {
            case 0:
                arrowSpeed = 2;
                break;
            case 1:
                arrowSpeed = 4;
                break;
            default:
                arrowSpeed = 9;
                break;
        }
    }

    void GetAnnouncerClip()
    {
        switch(shotPosition)
        {
            case "green":
                PlayAnnouncerClip(announcerGoal);
                break;
            case "orange":
                if (goalDirection != keeperDirection) PlayAnnouncerClip(announcerGoal);
                else PlayAnnouncerClip(announcerSave);
                break;
            default:
                PlayAnnouncerClip(announcerMiss);
                break;
        }
    }

    void PlayAnnouncerClip(AudioClip[] clipArray)
    {
        int randomIndex = Random.Range(0, clipArray.Length);
        AudioClip randomClip = clipArray[randomIndex];

        audioSource.clip = randomClip;
        audioSource.Play();
    }

    IEnumerator ReturnToMenu()
    {
        while (!ArduinoDataManager.Instance.ButtonAPressed && !ArduinoDataManager.Instance.ButtonBPressed)
        {
            yield return null;
        }

        ArduinoDataManager.Instance.ResetButtonStates();

        parentHolder.SetActive(false);
        ResetForNextShot();
        ResetGameObjects();
        ResetVariables();
        changeCamera.ChangeToNextCamera();
    }

    void OnEnable()
    {
        audioSource = this.GetComponent<AudioSource>();
        changeCamera = this.GetComponent<ChangeCamera>();
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
