using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AttackTester : MonoBehaviour
{
    private Animator ani;

    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField] float angle;

    private void Start() => ani = GetComponent<Animator>();

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ani.Play("Punching");
        }
    }

    public void Attack()
    {
        // 전방 앞에 위치한 몬스터들을 확인하고, 피격을 진행한다.

        // 1. 범위 안의 몬스터들을 확인한다.
        Collider[] cols = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in cols)
        {
            // 2. 각도 내에 몬스터가 있는지 확인한다.
            // 위 아래 높낮이로 통해 범위 밖으로 나간 것으로 판단되지 않도록, y값을 계산하지 않도록 하기 위해 임의로 0으로 설정한다.
            Vector3 source = transform.position; source.y = 0;
            Vector3 destination = col.transform.position; destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir);

            if (targetAngle > angle * 0.5f) continue;

            IDamagable damagable = col.GetComponent<IDamagable>();
            if (damagable != null) damagable.TakeHit(damage);
        }
    }

    private void OnDrawGizmos()
    {
        // 거리 시각적으로 그리기
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        // 각도 시각적으로 그리기
        Vector3 rightDir = Quaternion.Euler(0, angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * range);
    }
}
