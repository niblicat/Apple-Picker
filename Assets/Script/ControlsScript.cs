using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class ControlsScript : MonoBehaviour
{
    public enum ParticleType { Skull, Star };
    private TMP_Text scoreGT;
    private TMP_Text roundCounter;
    public static int round = 0;
    public static bool isPaused = false;
    public static bool debugEnabled = false;
    public static bool isProtected = false;
    public static bool gameOverFlag = false;
    public static bool basketsAreSlow = false;
    public static float greenAppleTimer = 0f;
    public static float yellowAppleTimer = 0f;
    public static float purpleAppleTimer = 0f;
    [SerializeField] private GameObject instantiator;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject debugMenu;
    [SerializeField] private GameObject gameOverScreen;
    private AppleInstantiator myInstantiator;
    void Start() {
        gameOverFlag = false;
        
        GameObject roundGO = GameObject.Find("Round");
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        roundCounter = roundGO.GetComponent<TMP_Text>();
        scoreGT = scoreGO.GetComponent<TMP_Text>();
        roundCounter.text = "Round 1";
        scoreGT.text = "0";

        Time.timeScale = 1.0f;
        HighScore.score = 0;
        round = 1;

        myInstantiator = instantiator.GetComponent<AppleInstantiator>();
        UpdateVariablesByRound(round);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverFlag) {
            SoundHandler mySoundHandler = gameObject.GetComponent<SoundHandler>();
            mySoundHandler.PlayDefaultAudio();
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ToggleDebugMenu();
        }
        if (greenAppleTimer > 0) {
            greenAppleTimer -= Time.deltaTime;
            if (greenAppleTimer <= 0) {
                myInstantiator.instantiateSpeedLock = false;
                myInstantiator.UpdateInstantiateIntervalByRound(round);
                greenAppleTimer = 0f;
                ToggleInstantiatorParticles(false);
            }
        }
        if (yellowAppleTimer > 0) {
            yellowAppleTimer -= Time.deltaTime;
            if (yellowAppleTimer <= 0) {
                isProtected = false;
                yellowAppleTimer = 0f;
                ToggleBasketParticles(false);
            }
            
        }
        if (purpleAppleTimer > 0) {
            purpleAppleTimer -= Time.deltaTime;
            if (purpleAppleTimer <= 0) {
                basketsAreSlow = false;
                purpleAppleTimer = 0f;
                ToggleBasketParticles(false);
            }
        }

    }

    public void TogglePause() {
        if (!gameOverFlag) {
            Time.timeScale = isPaused ? 1.0f : 0.0f;
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            ToggleVisualBlur();
        }
    }

    public static void ToggleVisualBlur() {
        PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        DepthOfField depth;
        if (ppVolume.profile.TryGetSettings(out depth)) {
            depth.active = !depth.active;
        }
        ColorGrading cg;
        if (ppVolume.profile.TryGetSettings(out cg)) {
            cg.active = !cg.active;
        }
    }
    public static void ToggleVisualBlur(bool newState) {
        PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        DepthOfField depth;
        if (ppVolume.profile.TryGetSettings(out depth)) {
            depth.active = newState;
        }
        ColorGrading cg;
        if (ppVolume.profile.TryGetSettings(out cg)) {
            cg.active = newState;
        }
    }

    public void IncreaseScore(int scoreIncrease) {
        int score = int.Parse(scoreGT.text);
        score += scoreIncrease;
        scoreGT.text = score.ToString();
        
        if (score > HighScore.score) HighScore.score = score;
        if (score / 2000 >= round) {
            UpdateVariablesByRound(++round);
        }
    }

    public void UpdateVariablesByRound(int roundNum) {
        if (roundNum > 0) {
            myInstantiator.UpdateSpeedByRound(roundNum);
            myInstantiator.UpdateDirectionChangeByRound(roundNum);
            myInstantiator.UpdateInstantiateIntervalByRound(roundNum);
            myInstantiator.UpdateAppleChancesByRound(roundNum);
            myInstantiator.UpdateFallSpeedByRound(roundNum);
            roundCounter.text = "Round " + roundNum;
        }
    }

    public void StartGameOver() {
        gameOverFlag = true;
        ToggleVisualBlur(true);
        Time.timeScale = 0.0f;
        gameOverScreen.SetActive(true);
        ResetEffects();
    }

    public void Retry() {
        SceneLoader.LoadScene("AppleGame");
    }
    public void MainMenu() {
        SceneLoader.LoadScene("Title");
    }

    public void GreenAppleFrenzy() {
        greenAppleTimer += 3f;
        if (!myInstantiator.instantiateSpeedLock) {
            myInstantiator.UpdateInstantiateIntervalByRound(round + 20);
            myInstantiator.instantiateSpeedLock = true;

            ToggleInstantiatorParticles(true);
        }
    }

    public void YellowAppleProtection() {
        if (basketsAreSlow) {
            basketsAreSlow = false;
            purpleAppleTimer = 0f;
        }
        else {
            ToggleBasketParticles(true);
        }
        ChangeParticleEffect(ParticleType.Star);
        isProtected = true;
        yellowAppleTimer += 5f;
    }

    public void PurpleAppleBurden() {
        purpleAppleTimer += 3f;
        basketsAreSlow = true;
        ToggleBasketParticles(true);
        ChangeParticleEffect(ParticleType.Skull);
    }

    public void ToggleBasketParticles(bool newParticleState) {
        GameObject[] basketArray = GameObject.FindGameObjectsWithTag("BasketParticleHolder");
        if (basketArray.Length > 0) {
            foreach (GameObject basket in basketArray) {
                ParticleSystem particles = basket.GetComponent<ParticleSystem>();
                var emission = particles.emission;
                emission.enabled = newParticleState;
            }
        }
    }
    public void ToggleInstantiatorParticles(bool newParticleState) {
        ParticleSystem particles = instantiator.GetComponent<ParticleSystem>();
        var emission = particles.emission;
        emission.enabled = newParticleState;
    }
    public void ChangeParticleEffect(ParticleType pt) {
        GameObject[] basketArray = GameObject.FindGameObjectsWithTag("BasketParticleHolder");
        if (basketArray.Length > 0) {
            Material newMaterial = null;
            switch (pt) {
                case ParticleType.Skull:
                    newMaterial = Resources.Load<Material>("Materials/Particles/Skulls");
                    break;
                case ParticleType.Star:
                    newMaterial = Resources.Load<Material>("Materials/Particles/Stars");
                    break;
            }
            foreach (GameObject basket in basketArray) {
                ParticleSystemRenderer psr = basket.GetComponent<ParticleSystemRenderer>();
                psr.material = newMaterial;
            }
        }
    }
    public void ToggleDebugMenu() {
        debugMenu.SetActive(!debugEnabled);
        debugEnabled = !debugEnabled;
    }

    public void ResetEffects() {
        purpleAppleTimer = yellowAppleTimer = greenAppleTimer = 0f;
        basketsAreSlow = isProtected = myInstantiator.instantiateSpeedLock = false;
        ToggleInstantiatorParticles(false);
        ToggleBasketParticles(false);
    }
}
