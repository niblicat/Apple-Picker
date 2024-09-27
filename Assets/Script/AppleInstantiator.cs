using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class AppleInstantiator : MonoBehaviour
{
    [Header("Primary Generated object")][SerializeField] private GameObject apple;
    [Header("Generated object 2")][SerializeField] private GameObject apple1;
    [Header("Generated object 3")][SerializeField] private GameObject apple2;
    [Header("Generated object 4")][SerializeField] private GameObject apple3;
    [Header("Generated object 5")][SerializeField] private GameObject branch;
    public float apple1Chance = 0f;
    public float apple2Chance = 0f;
    public float apple3Chance = 0f;
    public float branchChance = 0f;
    public Vector3 spawnOffset = new Vector3(0, -1.5f, 0);
    public float instantiateInterval = 1f;
    public float speed = 2f;
    public float leftAndRightLimits = 8f;
    public bool doDirectionChange = true;
    public float directionChangeChance = 0.01f;
    public bool instantiateSpeedLock = false;
    [SerializeField] private bool doStartInvoke = false;
    void Start() {
        if (doStartInvoke)
            InvokeRepeating(nameof(InstantiateApple), instantiateInterval, instantiateInterval);
        // use nameof() to auto complete fn name for safety :)

    }

    // Update is called once per frame
    void Update() {
        Vector3 newPos = transform.position;
        newPos.x += speed * Time.deltaTime;
        transform.position = newPos;
        if (newPos.x > leftAndRightLimits)
            speed = -1 * Mathf.Abs(speed);
        else if (newPos.x < -1 * leftAndRightLimits)
            speed = Mathf.Abs(speed);
    }

    void FixedUpdate() {
        if (doDirectionChange && Random.value < directionChangeChance) {
            speed *= -1;
        }
    }

    public void PrintLog(){ 
        Debug.Log("I am tree");
    }

    private void InstantiateApple() {
        SoundHandler mySoundHandler = gameObject.GetComponent<SoundHandler>();
        mySoundHandler.PlayPseudoRandomAudio();

        Vector3 resultPosition = transform.position + spawnOffset;
        float chance2 = apple1Chance + apple2Chance;
        float chance3 = chance2 + apple3Chance;
        float chance4 = chance3 + branchChance;
        float randomVal = Random.value;
        if (randomVal < apple1Chance)
            Instantiate(apple1, resultPosition, transform.rotation);
        else if (randomVal < chance2)
            Instantiate(apple2, resultPosition, transform.rotation);
        else if (randomVal < chance3)
            Instantiate(apple3, resultPosition, transform.rotation);
        else if (randomVal < chance4)
            Instantiate(branch, resultPosition, transform.rotation);
        else
            Instantiate(apple, resultPosition, transform.rotation);
        
    }
    public void UpdateSpeedByRound(int roundNum) {
        speed = Mathf.Abs(speed) / speed * (4f * Mathf.Log(((float)roundNum + 5f) / 5f) + 2f);
    }
    public void UpdateDirectionChangeByRound(int roundNum) {
        float newDirectionChangeChance = Mathf.Log(roundNum) / 300f + 0.01f;
        directionChangeChance = newDirectionChangeChance < 0.5f ? newDirectionChangeChance : 0.5f;
    }
    public void UpdateInstantiateIntervalByRound(int roundNum) {
        if (!instantiateSpeedLock) {
            instantiateInterval = 1f - 1.7f * Mathf.Atan((float)roundNum / 20f) / Mathf.PI;
            CancelInvoke();
            InvokeRepeating(nameof(InstantiateApple), instantiateInterval, instantiateInterval);
        }
    }

    public void UpdateAppleChancesByRound(int roundNum) {
        apple1Chance = (0.12f * Mathf.Atan((float)roundNum / 12f ) - 0.07f * Mathf.Atan(((float)roundNum - 20f) / 20f )) / Mathf.PI;
        apple2Chance = (0.09f * Mathf.Atan((float)roundNum / 24f ) - 0.03f * Mathf.Atan(((float)roundNum - 40f) / 40f )) / Mathf.PI;
        apple3Chance = 0.05f * Mathf.Atan((float)roundNum / 64f) / Mathf.PI + Mathf.Abs(0.05f * Mathf.Sin(Mathf.PI * (float)roundNum / 16f));
        branchChance = 0.05f * Mathf.Atan((float)roundNum / 80f) / Mathf.PI + Mathf.Abs(0.05f * Mathf.Cos(Mathf.PI * (float)roundNum / 16f));
    }
    public void UpdateFallSpeedByRound(int roundNum) {
        Rigidbody appleRigidBody = apple.GetComponent<Rigidbody>();
        appleRigidBody.angularDrag = 0.05f - 0.0999f * Mathf.Atan(roundNum / 12f) / Mathf.PI;
    }

}
