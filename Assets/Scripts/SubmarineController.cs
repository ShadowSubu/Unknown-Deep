using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    bool canUseTool = true;
    public float cameraShakeIntensity;
    public float cameraShakeTime;
    public int fragmentsCollected = 0;
    public bool drillUnlocked = false;
    public bool bladeUnlocked = false;

    [SerializeField] Transform turbineRing;
    [SerializeField] ParticleSystem bubbles;

    [Header("Arms")]
    public GameObject grabArm;
    public GameObject drillArm;
    public GameObject bladeArm;
    [SerializeField] GameObject grabArmArmature;
    [SerializeField] GameObject drillArmArmature;
    [SerializeField] GameObject bladeArmArmature;

    [Header("Icons")]
    [SerializeField] Image grabArmIcon;
    [SerializeField] Image drillArmIcon;
    [SerializeField] Image bladeArmIcon;

    #region Fixed Variables
    private static Vector3 forward = new Vector3(0f,0f,0f);
    private static Vector3 backward = new Vector3(0f,179f,0f);
    private static Vector3 upward = new Vector3(-40f, 0f, 0f);
    private static Vector3 downward = new Vector3(40f, 0f, 0f);
    private static Quaternion f = Quaternion.Euler(forward.x, forward.y, forward.z);
    private static Quaternion b = Quaternion.Euler(backward.x, backward.y, backward.z);
    private static Quaternion u = Quaternion.Euler(upward.x, upward.y, upward.z);
    private static Quaternion d = Quaternion.Euler(downward.x, downward.y, downward.z);

    private Color unselected = new Color(1f, 1f, 1f, 0.02f);
    private Color selected = new Color(1f, 1f, 1f, 1f);

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        WorldManager.instance.submarine = this;
        RetractAllArms();
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseTool)
        {
            if (grabArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                canUseTool = false;
                ArmOpenAnimation(grabArm, grabArmArmature);
                grabArmIcon.color = selected;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseTool && drillUnlocked)
        {
            if (drillArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                canUseTool = false;
                ArmOpenAnimation(drillArm, drillArmArmature);
                drillArmIcon.color = selected;

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseTool && bladeUnlocked)
        {
            if (bladeArm.activeSelf)
            {
                RetractAllArms();
            }
            else
            {
                canUseTool = false;
                ArmOpenAnimation(bladeArm, bladeArmArmature);
                bladeArmIcon.color = selected;

            }
        }
    }

    void RetractAllArms()
    {
        grabArm.SetActive(false);
        drillArm.SetActive(false);
        bladeArm.SetActive(false);
        grabArmIcon.color = unselected;
        drillArmIcon.color = unselected;
        bladeArmIcon.color = unselected;
    }

    void ArmOpenAnimation(GameObject arm, GameObject armature)
    {
        RetractAllArms();
        Sequence grabArmOpen = DOTween.Sequence();
        grabArmOpen.Append(armature.transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f));
        grabArmOpen.OnComplete(() =>
        {
            arm.SetActive(true);
            canUseTool = true;
            grabArmOpen.Append(armature.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
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
            CameraShake.instance.ShakeCamera(cameraShakeIntensity, cameraShakeTime);
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
            TakeDamage(WorldManager.instance.wallDamage);
        }
        else if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(WorldManager.instance.spikeDamage);
        }
        else if (collision.gameObject.CompareTag("Mine"))
        {
            TakeDamage(currentHealth + 1);
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
