using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreater : UnitChoose
{
    [Header("Prefab 관련 변수")]
    public static GameObject playerUnit;                      //플레이어가 선택한 유닛

    [Header("Line 변수")]
    public Transform line;                                    //라인 위치

    [Header("Raycast 관련 변수")]
    public Vector2 mouseUpPos;                                //클릭 뗀 순간의 마우스 위치
    public RaycastHit2D mouseUpHit;                           //클릭 뗀 위치 체크용

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseUpHit = Physics2D.Raycast(mouseUpPos, Vector2.zero);

            switch (mouseUpHit.transform.name)
            {
                case "Line1":
                    UnitCreate();
                    break;
                case "Line2":
                    UnitCreate();
                    break;
            }
        }
    }

    //유닛 생성 함수
    public void UnitCreate()
    {
        switch (line.name)
        {
            case "Line1":
                line.name = "Line1";
                Instantiate(playerUnit, line);
                break;
            case "Line2":
                line.name = "Line2";
                Instantiate(playerUnit, line);
                break;
        }


    }
}
