using Assets.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    public int numberOffEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSection;


    void Update()
    {
        //Caso atinja o n�mero m�ximo de inimigos spawnados
        if (currentEnemies >= numberOffEnemies)
        {
            //Contar a quantidade de inimigos ativos na cena
            int enemies = FindObjectsByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                LevelManeger.ChangeSection(nextSection);

                this.gameObject.SetActive(false);
            }
        }
    }

    void SpawnEnemy()
    {
        //Posic�a� do Spawn do inimgo 
        Vector2 spawnPosition;

        //Limites de Y
        //-0.36
        //-0.95

        spawnPosition.y = Random.Range(-0.95f, -0.36f);

        //Inimigo Posi��o X m�ximo (direita) do confiner da camera + 1 de distancia
        //Pegar o RightBound (limite direito ) da Section (confiner) como base

        float rightSectionBound = LevelManeger.currentConfiner.BoundingShape2D.bounds.max.x;

        //Define o x do spawnposition igual ao ponto da DIREITA do confiner
        spawnPosition.x = rightSectionBound;


        // Instancia(spawna) os inimigos
        // Pega um inimigo aleatorio da lista de inimigos
        // Spawna na posi��o spawnposition
        // quaterninon � uma classe ultilizada para trabalhar com rota��o
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        // Incrementa o contador de inimigos do Spawner
        currentEnemies++;

        //SE o numero de inimigos atualmente na cena for menor que o numeri m�ximo de inimigos,
        // Invoca novamente a fun��o de spawn
        if (currentEnemies < numberOffEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            //Desativa o colisor para iniciar o spawn apenas uma vez
            // ATEN��O: Desabilita o collider, mas o objeto Spawner continua ativo
            this.GetComponent<BoxCollider2D>().enabled = false;

            //Invoca pela  primeira vez a fun��o SpawnEnemy
            SpawnEnemy();
        }
    }
}
