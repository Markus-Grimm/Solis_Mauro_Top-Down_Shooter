using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int life = 2;

    //Movimento y direccion del mouse
    public float movSpd = 6f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    //Disparo
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    void Update()
    {
        //Movimento y direccion del mouse
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Disparo
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }


        if (life <= 0)
        {
            Dead();
        }

    }

    private void FixedUpdate()
    {
        //Movimento y direccion del mouse
        rb.MovePosition(rb.position + movement * movSpd * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }


    //Disparo
    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            life -= 1;
        }

        if (collision.transform.tag == "Enemy")
        {
            life -= 1;
        }

    }


    void Dead()
    {
        Destroy(gameObject);
    }

}
