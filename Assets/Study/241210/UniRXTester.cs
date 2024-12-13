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

        // 게임 오브젝트에 달아주지 않은 채로 사용 : 게임 오브젝트가 사라지면 함께 사라진다.
        this.UpdateAsObservable()                        // 업데이트마다 반응할 옵저버를 만든다. 
            .Where(x => Input.GetKeyDown(KeyCode.Space)) // 옵저버에게 괄호 안의 조건이 성립하였을 때 반을할 수 있도록 설정한다.
            .Where(x => isGround == true)
            .Select(x => transform.position)             // 다음 이루어질 작업에 필요한 데이터를 넘겨준다.
            .Subscribe(x => Jump() );                // 옵저버가 알려줄 때 마다, 실행할 함수나 행동을 연결한다.


        // 땅과 부딫히는 것을 계속 확인할 스트림이 땅과 부딫히는 상황에서 isGround를 true로 만들어준다.
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = true);

        // 땅과 부딫히는 것을 계속 확인할 스트림이 땅과 떨어지는 상황에서 isGround를 false로 만들어준다.
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Ground"))
            .Subscribe(x => isGround = false);


        // 이동 전용 stream
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
        Debug.Log("점프키 확인됨");
         rigid.velocity = Vector3.up * 5;
    }
}
