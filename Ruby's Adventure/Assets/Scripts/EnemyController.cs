using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public bool broken = true;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    Animator animator;

    public ParticleSystem smokeEffect;
    public ParticleSystem projectileHit;

    AudioSource audioSource;
    public AudioClip fixClip;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (!broken)
            {
                return;
            }

            direction = -direction;
            timer = changeTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * Time.deltaTime * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        audioSource.clip = fixClip;
        audioSource.Play();
        audioSource.loop = false;
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        Instantiate(projectileHit, rigidbody2D.position, Quaternion.identity);
        smokeEffect.Stop();
        //audioSource.Pause();
    }
}
