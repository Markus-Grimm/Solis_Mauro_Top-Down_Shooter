using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    public int life = 2;
    public GameController a;


    //Movimento y direccion del jugador
    public float movSpd = 1f;

    // IA Rango de vision
    public float visionRadius = 4f;

    public Rigidbody2D rb;
    public GameObject Player;


    Vector2 movement;
    Vector2 pyrPos;

    // IA Rango de vision
    Vector2 iniPos;
    Vector2 target;

    //IA NavMesh
    private NavMeshAgent agent;

    private void Start()
    {
        a = GameController.FindObjectOfType<GameController>();

        // IA Rango de vision
        iniPos = transform.position;

        //IA NavMesh
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    void Update()
    {
        pyrPos = Player.transform.position;
        
        // IA Rango de vision        
                float dist = Vector2.Distance(Player.transform.position, transform.position);
        if (dist < visionRadius)
        {
            //target = Player.transform.position;
            //IA NavMesh
            agent.SetDestination(Player.transform.position);
        }
        else
        {
            //target = iniPos;
            //IA NavMesh
            agent.SetDestination(iniPos);
        }


        float fixedspd = movSpd * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, /*pyrPos*/target, fixedspd);


        if (life <= 0)
        {
            a.AumentoScore();
            Dead();
        }

    }


    private void FixedUpdate()
    {
        //Movimento y direccion del mouse
        rb.MovePosition(rb.position + movement * movSpd * Time.fixedDeltaTime);

        Vector2 lookDir = pyrPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            life -= 1;
        }

    }

    void Dead()
    {
        
        Destroy(gameObject);
    }

}
