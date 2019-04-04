﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public float fireBallCooldown, shieldCooldown, lightningCooldown;
    GameObject player;
    LevelManager levelManager;
    BossManager boss;
    UIManager uIManager;
    DialogueManager dialogueManager;
    bool paused = false;
    bool doubleJump = false, wallJump = false, dash = false, fireBall = true, shield = true, lightning = false, invulnerable=false;

    //Los checkpoints son structs en los que se guardan dos datos: El transform, para la posición, y la escena, para cargar la escena necesaria al reaparecer.
    [System.Serializable]
    struct Checkpoint
    {
        public Vector2 playerPosition, cameraRoom;
        public string scene;
    }

    //Guardamos la información del checkpoint actual para poder acceder a ella desde otros scripts del juego. Como el GameManager no se elimina al cambiar de escena podemos almacenar aqui este tipo de datos.
    [SerializeField]
    Checkpoint currentCheckpoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {

    }

    //obtiene el level manager
    public void GetLevelManager(LevelManager level)
    {
        levelManager = level;
    }

    //devuelve cual es el level manager en cada escena
    public LevelManager SetLevelManager()
    {
        return levelManager;
    }

    //devuelve el valor de la vida actual
    public int GetLife()
    {
        return player.GetComponent<Life>().GetActualLife();
    }

    public void GetPlayer(GameObject playerI)
    {
        player = playerI;
    }

    public GameObject ReturnPlayer()
    {
        return player;
    }

    //Avisamos a GameManager de qué objeto es el UIManager
    public void ThisUIManager(UIManager uI)
    {
        uIManager = uI;
    }

    public UIManager ReturnUIManager()
    {
        return uIManager;
    }

    public void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }

    public void Pausa()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        paused = !paused;
    }

    public bool IsPaused()
    {
        return paused;
    }

    //Este metodo carga la escena en la que se encuentra el checkpoint actual. Para determinar la posición del jugador el LevelManager accederá a el transform del Checkpoint actual en su metodo Start.
    public void Respawn()
    {
        SceneManager.LoadScene(currentCheckpoint.scene);
    }

    //Cuando el jugador llegue a un checkpoint este avisará al GameManager para que cambie la información del checkpoint actual. Como es lógico la escena de este checkpoint será aquella en la que nos encontremos en este momento.
    public void ChangeCurrentCheckpoint(Transform checkpointTransform, Transform roomTransform)
    {
        currentCheckpoint.playerPosition = new Vector2(checkpointTransform.position.x, checkpointTransform.position.y);
        currentCheckpoint.cameraRoom = new Vector2(roomTransform.position.x, roomTransform.position.y);
        currentCheckpoint.scene = SceneManager.GetActiveScene().name;
    }

    //Este metodo devuelve el transform del checkpoint actual. Esta información es util para que el LevelManager situe al jugador en la posición correcta al spawnear.
    public Vector2 ReturnCurrentCheckpointPosition()
    {
        return currentCheckpoint.playerPosition;
    }

    public Vector2 ReturnCurrentCheckpointRoomPosition()
    {
        return currentCheckpoint.cameraRoom;
    }

    public void SetAbilityTrue(string ability)
    {
        switch (ability)
        {
            case "Dash":
                dash = true;
                break;
            case "Wall Jump":
                wallJump = true;
                break;
            case "Double Jump":
                //Si el player tiene PlayerMovement entonces setea los saltos a dos
                PlayerMovement playerM=player.GetComponent<PlayerMovement>();
                if (playerM != null) playerM.DoubleJumpActive();
                doubleJump = true;
                break;
            case "Fireball":
                fireBall = true;
                break;
            case "Shield":
                shield = true;
                break;
            case "Lightning":
                lightning = true;
                break;
        }
    }

    public bool ReturnAbilityValue(string ability)
    {
        switch (ability)
        {
            case "Dash":
                return dash;
            case "WallJump":
                return wallJump;
            case "DoubleJump":
                return doubleJump;
            case "Fireball":
                return fireBall;
            case "Shield":
                return shield;
            case "Lightning":
                return lightning;
            default: return false;
        }
    }
    //Metodo para comprobar si se puede hacer daño al jugador (lo utiliza el MakeDamage).
    public bool GetInvulnerablePlayer()
    {
        return invulnerable;
    }

    //Metodo para activar/desactivar la invulnerabilidad (llamado por el escudo).
    public void InvulnerablePlayer()
    {
        invulnerable = !invulnerable;
    }

    //Devuelve el cooldown de una habilidad, la cual se determina a partir del nombre que se le da al método
    public float ReturnCooldown(string name)
    {
        if (name.Contains("Fireball")) return fireBallCooldown;

        else if (name.Contains("Shield")) return shieldCooldown;

        else if (name.Contains("Lightning")) return lightningCooldown;

        else return 0;
    }
    public void GetBossManager(BossManager bossM)
    {
        boss = bossM;
    }
    public BossManager ReturnBossManager()
    {
        return boss;
    }

    public void GetDialogueManager(DialogueManager dialogueM)
    {
        dialogueManager = dialogueM;
    }

    public DialogueManager ReturnDialogueManager()
    {
        return dialogueManager;
    }
}
