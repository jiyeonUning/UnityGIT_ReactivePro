using UnityEngine;

public class _ZenjectTester : MonoBehaviour
{
    // �ش� ������Ʈ�� �������� ������ ������ �� ���� = ���Ӽ�
    private Rigidbody rigid;

    [SerializeField] float moveSpeed;


    private void Start() => rigid = GetComponent<Rigidbody>();

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rigid.velocity = moveDir * moveSpeed;
    }
}
