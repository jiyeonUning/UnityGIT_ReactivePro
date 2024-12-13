using UnityEngine;

public class _ZenjectTester : MonoBehaviour
{
    // 해당 컴포넌트가 존재하지 않으면 동작할 수 없다 = 종속성
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
