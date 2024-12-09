using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTweenTester : MonoBehaviour
{
    private void Update()
    {
        // 왼쪽 마우스 버튼을 클릭했을 때,
        if (!Input.GetMouseButtonDown(0)) return;

        // 화면상의 마우스 위치를 기준으로 레이캐스트를 발사
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 레이저 끝이 닿은 위치로 이동한다.
        if (Physics.Raycast(ray, out RaycastHit hitInfo)) MoveTo(hitInfo.point);
    }


    public void MoveTo(Vector3 des)
    {
        transform.DOMove(des, 1f)
            .SetEase(Ease.InOutCirc)
            .SetDelay(0.5f);

        Material material = GetComponent<Material>();
        material.DOColor(Random.ColorHSV(), 0.5f);
    }
}
