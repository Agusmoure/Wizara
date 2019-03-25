using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    GameObject player;
    LevelManager levelManager;
    UIManager uIManager;
    string currentScene, lastScene;
    bool paused = false;
    bool fireBall = true;
    bool doubleJump = false, wallJump = false, dash = false;

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
        if (levelManager != null) Invoke("MakeLevelManagerRespawn", 0.5f * Time.deltaTime);
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
        Invoke("MakeLevelManagerRespawn", 0.5f * Time.deltaTime);
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

    void MakeLevelManagerRespawn()
    {
        levelManager.SpawnPlayer();
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
                doubleJump = true;
                break;
            case "Fireball":
                fireBall = true;
                break;
        }
    }
    public bool GetAbility(string nombreHabilidad)
    {
        switch (nombreHabilidad)
        {
            case "Fireball":
                return fireBall;
            case "WallJump":
                return wallJump;
            default:
                return false;

        }
    }
}
