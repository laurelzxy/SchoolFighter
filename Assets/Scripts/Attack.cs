using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ao colidir, salva na variavel enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        //ao colidir, salva na variavel player, o player que foi atingido
        PlayerController player = collision.GetComponent<PlayerController>();

        //Se a colis�o foi com um inimigo
        if (enemy != null)
        {
            //Inimigo recebe dano
            enemy.TakeDamage(damage);


        }

        //Se a colis�o foi com um player
        if (player != null)
        {
            //O player recebe dano
            player.TakeDamage(damage);
        }

    }
}
