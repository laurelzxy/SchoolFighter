using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    public float playerSpeed = 0.6f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;
    private Animator playerAnimator;

    // Player olhando para a direita
    private bool playerFacingRight = true;

    //Variavel contadora
    private int punchCount;

    //tempo de ataque
    private float timeCross= 0.75f;

    private bool comboControl;

    //INdicar se o player esta morto
    private bool isDead;


    void Start()
    {
        //Obtem e inicializa as propriedades do RigiBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Obtem e inicializa as propriedades do RigiBody2D
        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;


    }

    private void Update()
    {
        PlayerMove();
        UpdateAnimator();
        
        if (Input.GetKeyDown(KeyCode.X))
        {
           

                if (punchCount < 2)
                {

                    PlayerJab();
                    punchCount++;
                    if (!comboControl)
                    {
                        //Iniciar o temporizador
                         StartCoroutine(CrossController());
                    }
                }
                else if (punchCount >= 2)
                {
                    PlayerCross();
                     punchCount=0;
                }
                
            }
        //Parando o temporizador
        StopCoroutine(CrossController());


    }

    //Geralmente é ultilizado para implementação de física no jogo, por ter
    //uma uma execução padronizada em diferentes dispositivos
    private void FixedUpdate()
    {
        //Verificar se o player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, cria um Vector2 para usar no playDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se o player vai para a esquerda e esta olhando para a direita
        if (playerDirection.x < 0 && playerFacingRight == true)
        {
            Flip();
        }

        // o player vai para a direita e esta olhando para a esquerda
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Definir o valor da parametro do animaro, igual á propriedade isWalking
        playerAnimator.SetBool("isWalking", isWalking); 
    }

    void Flip ()
    {
        //Vai girar o sprite do player em 180 no eixo y

        //Inverter o valor da variavel playerfacing right
        playerFacingRight = !playerFacingRight;

        //Girar o sprite em 180 graus no eixo Y
        // X , Y, Z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessar a animação do JAB, ativa o gatilho de ataque JAB
        playerAnimator.SetTrigger("isJab");
    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");
    }

    void ZeroSpeed ()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;

    }
    
}
