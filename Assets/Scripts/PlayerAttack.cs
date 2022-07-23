using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Range(0f, 100f)]
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster" && other.gameObject.name == "Body")
        {
            GameObject monster = other.gameObject.transform.parent.gameObject;
            GameObject player = GameObject.FindWithTag("Player");

            if (player.name == "root")
            {
                player = player.transform.parent.gameObject;
            }

            MonsterController controller = monster.GetComponent<MonsterController>();

            int nowAni = player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash;

            if (nowAni == Animator.StringToHash("Base Layer.Attack1") || nowAni == Animator.StringToHash("Base Layer.Attack2"))
            {
                controller.getDamaged(damage);
            }
        }
    }
}
