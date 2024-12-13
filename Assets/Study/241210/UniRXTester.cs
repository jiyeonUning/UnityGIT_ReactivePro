using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class UniRXTester : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;

    [SerializeField] int hp;
    [SerializeField] bool isGround;
    

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        // ���� ������Ʈ�� �޾����� ���� ä�� ��� : ���� ������Ʈ�� ������� �Բ� �������.
        this.UpdateAsObservable()                        // ������Ʈ���� ������ �������� �����. 
            .Where(x => Input.GetKeyDown(KeyCode.Space)) // ���������� ��ȣ ���� ������ �����Ͽ��� �� ������ �� �ֵ��� �����Ѵ�.
            .Where(x => isGround == true)
            .Select(x => transform.position)             // ���� �̷���� �۾��� �ʿ��� �����͸� �Ѱ��ش�.
            .Subscribe(x => Jump() );                // �������� �˷��� �� ����, ������ �Լ��� �ൿ�� �����Ѵ�.


        // ���� �΋H���� ���� ��� Ȯ���� ��Ʈ���� ���� �΋H���� ��Ȳ���� isGround�� true�� ������ش�.
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // ���� �΋H���� ���� ��� Ȯ���� ��Ʈ���� ���� �������� ��Ȳ���� isGround�� false�� ������ش�.
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = false);


        // �̵� ���� stream
        this.UpdateAsObservable()
            .Subscribe(param =>
            {
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");

                Vector3 velocity = rigid.velocity;
                velocity.x = x; velocity.z = z;

                rigid.velocity = velocity;
            });
    }

    void Jump()
    {
        Debug.Log("����Ű Ȯ�ε�");
         rigid.velocity = Vector3.up * 5;
    }
}
