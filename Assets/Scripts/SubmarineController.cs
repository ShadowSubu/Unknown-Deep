using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubmarineController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Submarine Variables")]
    public float subSpeed;
    public float turnSpeed;
    public float rotationSpeed;
    public float stabilizationSpeed;
    public int maxHealth;
    public int currentHealth;
    float turbineSpeed;
    private float currentSpeed;
    bool canMove = true;

    [SerializeField] Transform turbineRing;
    [SerializeField] ParticleSystem bubbles;

    [Header("Arms")]
    [SerializeField] GameObject grabArm;
    [SerializeField] GameObject drillArm;
    [SerializeField] GameObject bladeArm;
    [SerializeField] GameObject grabArmArmature;
    [SerializeField] GameObject drillArmArmature;
    [SerializeField] GameObject bladeArmArmature;

    #region Fixed Variables
    private static Vector3 forward = new Vector3(0f,0f,0f);
    private static Vector3 backward = new Vector3(0f,179f,0f);
    private static Vector3 upward = new Vector3(-40f, 0f, 0f);
    private static Vector3 downward = new Vector3(40f, 0f, 0f);
    private static Quaternion f = Quaternion.Euler(forward.x, forward.y, forward.z);
    private static Quaternion b = Quaternion.Euler(backward.x, backward.y, backward.z);
    private static Quaternion u = Quaternion.Euler(upward.x, upward.y, upward.z);
    private static Quaternion d = Quaternion.Euler(downward.x, downward.y, downward.z);
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        WorldManager.instance.submarine = gameObject;
    }

    void FixedUpdate()
    {
        SubmarineMovement();
    }

    private void Update()
    {
        HandleArm();
    }

    private void SubmarineMovement()
    {
        if (canMove)
        {
            float horizontolInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 movement = new Vector3(0f, verticalInput, horizontolInput).normalized;

            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, f, rotationSpeed * Time.deltaTime);
                currentSpeed = subSpeed;
                bubbles.Play();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, b, rotationSpeed * Time.deltaTime);
                currentSpeed = subSpeed;
                bubbles.Play();
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
                bubbles.Stop();
            }

            if (Input.GetKey(KeyCode.W))
            {
                Quaternion temp = Quaternion.Euler(upward.x, transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
                currentSpeed = subSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Quaternion temp = Quaternion.Euler(downward.x, transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, temp, rotationSpeed * Time.deltaTime);
                currentSpeed = subSpeed;
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.01f);
            }

            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, subSpeed);
            rb.velocity = movement * currentSpeed;

            turbineSpeed = Mathf.Clamp(turbineSpeed, 2f, currentSpeed);
            turbineRing.Rotate(Vector3.forward, currentSpeed / 4);

            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.Euler(new Vector3(0, rb.rotation.eulerAngles.y, 0)), stabilizationSpeed));
        }
    }

    void HandleArm()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (grabArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                ArmOpenAnimation(grabArm, grabArmArmature);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (drillArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                ArmOpenAnimation(drillArm, drillArmArmature);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (bladeArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                ArmOpenAnimation(bladeArm, bladeArmArmature);
            }
        }
    }

    void RetractAllArms()
    {
        grabArm.SetActive(false);
        drillArm.SetActive(false);
        bladeArm.SetActive(false);
    }

    void ArmOpenAnimation(GameObject arm, GameObject armature)
    {
        RetractAllArms();
        Sequence grabArmOpen = DOTween.Sequence();
        grabArmOpen.Append(armature.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f));
        grabArmOpen.OnComplete(() =>
        {
            arm.SetActive(true);
            grabArmOpen.Append(armature.transform.DOScale(new Vector3(1f, 1f, 1f), 3f));
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BiomeTwoCheckpoint"))
        {
            WorldManager.instance.checkPoint.ReachedCheckPointOne();
        }
        else if (other.CompareTag("BiomeThreeCheckpoint"))
        {
            WorldManager.instance.checkPoint.ReachedCheckPointTwo();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth >= damageAmount)
        {
            currentHealth -= damageAmount;
            WorldManager.instance.health.fillAmount = (float)currentHealth/(float)maxHealth;
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        GameManager.instance.ChangeState(GameState.respawn);
        currentHealth = maxHealth;
        WorldManager.instance.health.fillAmount = (float)currentHealth / (float)maxHealth;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Level"))
        {
            Debug.Log(1);
            TakeDamage(WorldManager.instance.wallDamage);
        }
        else if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(WorldManager.instance.spikeDamage);
        }

        if (collision.gameObject.CompareTag("BiomeTwoCheckpoint"))
        {
            WorldManager.instance.checkPoint.ReachedCheckPointOne();
        }

        if (collision.gameObject.CompareTag("BiomeThreeCheckpoint"))
        {
            WorldManager.instance.checkPoint.ReachedCheckPointTwo();
        }
    }

    
}
