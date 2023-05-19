using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float rotationSpeed = 30;
    private Vector3 inputVec;
    private Vector3 targetDirection;

    private float health;
    private static float damageDealt;

    public float PlayerHealth { get { return health; } set { health = value; } }
    public static float PlayerDamage { get { return damageDealt; } set { damageDealt = value; } }

    public Slider slider;

    private void Start()
    {
        health = 500f;
        damageDealt = 25f;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("PlayerHorizontal");
        float vertical = Input.GetAxisRaw("PlayerVertical");
        inputVec = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("Input X", horizontal);
        animator.SetFloat("Input Z", vertical);

        if (vertical != 0 || horizontal != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
            animator.SetBool("Moving", false);

        if (Input.GetButtonDown("PlayerAttack"))
        {
            animator.SetTrigger("Attack1Trigger");
            StartCoroutine(AnimPause(1.2f));
        }
        UpdateMovement();
    }

    IEnumerator AnimPause(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
    }

    private void UpdateMovement()
    {
        RotateTowardMovementDirection();
        GetCameraRelativeMovement();
    }

    private void RotateTowardMovementDirection()
    {
        if (inputVec != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void GetCameraRelativeMovement()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float horizontal = Input.GetAxisRaw("PlayerHorizontal");
        float vertical = Input.GetAxisRaw("PlayerVertical");

        targetDirection = horizontal * right + vertical * forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player2Weapon")
        {
            //PlayerHealth -= PlayerTwoController.PlayeTwoDamage;
            slider.value = PlayerHealth;
        }
        if (PlayerHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "healthBoost")
        {
            Destroy(other.gameObject);
            if (PlayerHealth < 500)
            {
                PlayerHealth += 100;
                slider.value = PlayerHealth;
                if (PlayerHealth > 500)
                    PlayerHealth = 500;
            }
        }
        if (other.gameObject.tag == "damageBoost")
        {
            Destroy(other.gameObject);
            StartCoroutine(BoostTime(10f));
        }
    }

    IEnumerator BoostTime(float time)
    {
        float playerPrevDamage = PlayerDamage;
        PlayerDamage = playerPrevDamage * 2f;
        yield return new WaitForSeconds(time);
        PlayerDamage = playerPrevDamage;
    }

    void FootR() { }
    void FootL() { }
    void Hit() { }
}
