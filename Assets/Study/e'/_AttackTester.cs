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
        // ���� �տ� ��ġ�� ���͵��� Ȯ���ϰ�, �ǰ��� �����Ѵ�.

        // 1. ���� ���� ���͵��� Ȯ���Ѵ�.
        Collider[] cols = Physics.OverlapSphere(transform.position, range);

        foreach (Collider col in cols)
        {
            // 2. ���� ���� ���Ͱ� �ִ��� Ȯ���Ѵ�.
            // �� �Ʒ� �����̷� ���� ���� ������ ���� ������ �Ǵܵ��� �ʵ���, y���� ������� �ʵ��� �ϱ� ���� ���Ƿ� 0���� �����Ѵ�.
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
        // �Ÿ� �ð������� �׸���
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        // ���� �ð������� �׸���
        Vector3 rightDir = Quaternion.Euler(0, angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * range);
    }
}
