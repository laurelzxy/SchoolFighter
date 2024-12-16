using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider playerHealthBar;
    public Image playerImage;

    public GameObject enemyUI;
    public Slider enemyHealthBar;
    public Image enemyImage;

    // Objeto para armzazenar os dados do player
    private PlayerController player;

    //Timers e controles do enemyUI
   [ SerializeField ] public float enemyUITime = 4f;
    private float enemyTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtem os dados do Player
        player = FindAnyObjectByType<PlayerController>();

        // DEfinir o valor maximo da barra de vida ih=gual ao maximo da vida do player
        playerHealthBar.maxValue = player.maxHealth;

        //Iniciar o HealtBar cheia(barra de vida)
        playerHealthBar.value = playerHealthBar.maxValue;

        //Definir a imagem do player
        playerImage.sprite = player.playerImage;
    }

    // Update is called once per frame
    void Update()
    {
        //Inicia o contador para controlar o tempo de exibição da enemyUI
        enemyTimer += Time.deltaTime;

        //SE  o tempo limite for atingido, oculta a UI e reseta o timer
        if (enemyTimer >= enemyUITime)
        {
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealthBar.value = amount;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, Sprite image)
    {
        //Atualiza os dados do inimigo de acordo com o inimigo atacado
        enemyHealthBar.maxValue = maxHealth;
        enemyHealthBar.value = currentHealth;
        enemyImage.sprite = image;

        //Zera o timer para começar a contar 4 seg
        enemyTimer = 0;

        //Habilita a enemyUI, deixando-a visível
        enemyUI.SetActive(true);
    }

}
