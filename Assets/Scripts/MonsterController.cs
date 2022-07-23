using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// [System.Serializable]  : 직렬화  -> 인스펙터에 표시 가능
public class MonsterController : MonoBehaviour
{
    GameObject target;

    Animator ani;
    Rigidbody rig;
    NavMeshAgent nav;

    [HideInInspector]  // 인스펙터에서 해당 변수를 숨긴다.
    public int test = 5;

    [SerializeField]  // 해당 변수를 인스펙터에 표시한다.
    float maxHealth = 50f;
    float nowHealth = 50f;

    [SerializeField]  
    float maxCooldown = 2f;
    float nowCooldown = 0f;

    private void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (nowHealth > 0)
        {
            if (target != null)
            {
                nav.isStopped = false;
                nav.SetDestination(target.transform.position);
                ani.SetBool("Walk", true);

                PlayerController player = target.GetComponent<PlayerController>();

                if (player.getNowHealth() > 0)
                {
                    float distance = Vector3.Distance(target.transform.position, transform.position);

                    if (distance <= 2f)
                    {
                        if (nowCooldown >= maxCooldown)
                        {
                            nowCooldown = 0f;
                            ani.SetTrigger("Attack");
                            StartCoroutine("onPlayerDamaged");
                        }
                        else
                        {
                            nowCooldown += Time.deltaTime;
                        }
                    }
                    else
                    {
                        // 진행 중이던 코루틴을 강제로 종료함
                        StopCoroutine("onPlayerDamaged");
                    }
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                nav.isStopped = true;
                ani.SetBool("Walk", false);
            }
        }
        else
        {
            rig.isKinematic = true;
            nav.isStopped = true;
            ani.SetBool("Die", true);

            StartCoroutine("onMonsterDead");

            // 0.5초 기다린 후 아래 내용을 실행
            // Destroy(gameObject);
        }

        rig.angularVelocity = Vector3.zero;
        rig.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && other.gameObject.name.Equals("Player"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && other.gameObject.name.Equals("Player"))
        {
            target = null;
        }
    }

    // 코루틴 메소드
    IEnumerator onMonsterDead()
    {
        // 실행이 일시정지됨 -> 유니티에게 제어권을 넘김 -> 지정한 시간이 되면 제어권을 다시 가져옴
        // WaitForSeconds() : 프레임 당 시간에 영향을 받음 (컴퓨터 사양에 따라 조금씩 차이가 날 수 있음) = Update, LateUpdate
        // WaitForSecondsRealtime() : 프레임 당 시간에 영향을 받지 않음 (컴퓨터 사양과 상관 없이 동일한 시간을 체크) = FixedUpdate
        yield return new WaitForSeconds(2.5f); 
        
        // 제어권을 다시 가져왔을 때 여기부터 실행됨
        Destroy(gameObject);
    }

    IEnumerator onPlayerDamaged()
    {
        yield return new WaitForSeconds(0.3f);
        target.GetComponent<PlayerController>().getDamaged(5f);
    }

    public void getDamaged(float damage)
    {
        if(nowHealth > 0)
        {
            nowCooldown = 0f;
            ani.SetTrigger("Damaged");
            nowHealth -= damage;
        }
    }

    public float getNowHealth()
    {
        return nowHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
