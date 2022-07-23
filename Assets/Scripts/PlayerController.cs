using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float moveSpeed = 5f;

    [Range(0f, 10f)]
    public float rotationSpeed = 5f;

    [SerializeField]
    float maxHealth = 100f;
    float nowHealth = 100f;

    Animator ani;
    Rigidbody rig;
    Slider slider;

    private void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        slider = GameObject.Find("Health Slider").GetComponent<Slider>();

        slider.maxValue = maxHealth;
    }

    private void Update()
    {
        slider.value = nowHealth;

        if (nowHealth > 0)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float mouseX = Input.GetAxisRaw("Mouse X");

            transform.Rotate(new Vector3(0f, mouseX, 0f) * rotationSpeed);

            int nowAni = ani.GetCurrentAnimatorStateInfo(0).fullPathHash;

            // 왼쪽 클릭을 했을 때 = Attack1
            if (Input.GetMouseButtonDown(0))
            {
                ani.SetBool("Sprint", false);
                ani.SetTrigger("Attack1");
            }

            // 오른쪽 클릭을 했을 때 = Attack2
            if (Input.GetMouseButtonDown(1))
            {
                ani.SetBool("Sprint", false);
                ani.SetTrigger("Attack2");
            }

            if (h != 0 || v != 0)
            {
                if (nowAni == Animator.StringToHash("Base Layer.Idle") || nowAni == Animator.StringToHash("Base Layer.Sprint"))
                {
                    ani.SetBool("Sprint", true);
                    transform.Translate(new Vector3(h * moveSpeed * Time.deltaTime, 0f, v * moveSpeed * Time.deltaTime));
                }
                else
                {
                    ani.SetBool("Sprint", false);
                }
            }
            else
            {
                ani.SetBool("Sprint", false);
            }
        }
        else
        {
            ani.SetBool("Die", true);
        }

        rig.angularVelocity = Vector3.zero;
    }

    public void getDamaged(float damage)
    {
        if (nowHealth > 0)
        {
            ani.SetTrigger("Damaged");
            nowHealth -= damage;
        }
    }

    public float getNowHealth()
    {
        return nowHealth;
    }
}
