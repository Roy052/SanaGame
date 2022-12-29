# Sana's Long Long Journey
<div>
    <h2> 게임 정보 </h2>
    <img src = "https://img.itch.zone/aW1nLzEwODkzOTEwLnBuZw==/315x250%23c/AL4pCL.png"><br>
    <img src="https://img.shields.io/badge/Unity-yellow?style=flat-square&logo=Unity&logoColor=FFFFFF"/>
    <img src="https://img.shields.io/badge/Racing-pink"/>
    <h4> 개발 일자 : 2022.07 <br><br>
    플레이 : https://goodstarter.itch.io/sanas-long-long-journey
    
  </div>
  <div>
    <h2> 게임 설명 </h2>
    <h3> 스토리 </h3>
     이 게임은 츠쿠모 사나를 위한 홀로라이브 팬 게임이다. <br><br>
     카운슬의 일원이었던 사나가 먼 여행을 떠난다.
    <h3> 게임 플레이 </h3>
     WASD 키 혹은 방향키로 캐릭터를 움직인다.<br><br>
     ESC 키를 이용해 메뉴로 혹은 게임을 종료할 수 있다.<br><br>
     리미터를 제외한 다른 물체와 부딪히면 이동속도가 느려진다.<br><br>
     리미터와 닿으면 리미터 게이지가 오르고, 게이지가 최대가 되면 몸집이 커지고 이동속도가 내려가지 않는다.<br><br>
     해왕성 너머까지 이동하자.
  </div> 
  <div>
    <h2> 게임 스크린샷 </h2>
      <table>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTYzOTMxMC8xMDg5Mzg4NS5wbmc=/347x500/dnXke%2B.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTYzOTMxMC85Njc1NjMyLnBuZw==/347x500/TCN%2FW%2B.png"></td>
        <td><img src = "https://img.itch.zone/aW1hZ2UvMTYzOTMxMC8xMDg5Mzg4Ni5wbmc=/347x500/Jf7u6c.png"></td>
      </table>
  </div>
    <div>
    <h2> 게임 플레이 영상 </h2>
    https://youtu.be/hd9L6K3XbN8
  </div>
  <div>
    <h2> 배운 점 </h2>
      처음엔 간단한 게임을 개인적으로 만들 생각이었다.<br><br>
      그러나 아이디어가 확장되어 다른 사람들의 코멘트를 추가하면서 자연스레 다른 사람의 피드백을 받게 되었다.<br><br>
      그 피드백을 통해 버그 수정 및 내용 수정, 부족한 캐릭터 반응 등을 수정해 조금 더 할만한 게임으로 바꿀 수 있었다.<br><br>
      또한 마지막 장면에 다른 사람들의 댓글들을 집어넣어 같이 만드는 게임을 처음으로 만들어 보았다.
  </div>
  <div>
    <h2> 수정할 점 </h2>
      컨텐츠를 추가한다.
   <h2> Design Picture </h2>
   <table>
        <td><img src = "https://postfiles.pstatic.net/MjAyMjA4MDFfMjcy/MDAxNjU5MzMwODAxOTk1.-gbZMXUDyhOMz_i8yUj_aAh4hzSgm6293HBrNPNIvTAg.ZZtycSu828JGsjLCsBnqv03vtyKRWoA7w_eJ4Rt68qkg.JPEG.tdj04131/KakaoTalk_20220801_141114267_03.jpg?type=w773" height = 500></td>
     <td><img src = "https://postfiles.pstatic.net/MjAyMjA4MDFfMTI3/MDAxNjU5MzMwODAxOTI1.GWwJzBX5V1b-ubqEyGZDpCZPxJOMTC3ju36pHG82cYQg.lAN9ou64svdpNpIa3q-vCsura4jk8hso3nfKY1Vb6Xgg.JPEG.tdj04131/KakaoTalk_20220801_141114267_04.jpg?type=w773" height = 500></td>
      </table>
  </div>

   <div>
       <h2> 주요 코드 </h2>
       <h4> GameSM Update 함수 속 거리 및 장애물 생성 </h4>
    </div>
    
```csharp

//거리 계산
if (gameEnd == false)
    distance += 2 * speed * Time.deltaTime;
    else
        distance = endDistance;

//거리 출력
distanceText.text = ((int) distance).ToString();
distanceSlider.value = (float) (distance / endDistance);

//거리에 따라 행성 출현
if (planetCount < planetLocation.Length && distance > planetLocation[planetCount])
{
    GameObject temp = Instantiate(planets[planetCount],
    new Vector3(12, 0, 0), Quaternion.identity);
    planetCount++;
}

//배경 생성(배경 이미지를 이동 후 파괴, 생성)
beforeBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
currentBackground.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

if (beforeBackground.transform.position.x < -lengthBackground * 2 + 0.7f)
{
    Destroy(beforeBackground);
    beforeBackground = currentBackground;
    currentBackground = Instantiate(currentBackground, new Vector3(2 * lengthBackground, 0, 0), Quaternion.identity);
}

//장애물 생성
timeCheck += Time.deltaTime;
if (gameEnd == false && timeCheck >= 1 + nextTime - (speed / 15.0))
{
    int typetemp = Random.Range(0, obstaclePrefabArray.Length);
    GameObject temp =
    Instantiate(obstaclePrefabArray[typetemp],
    new Vector3(12, Random.Range(-border, border), 0), Quaternion.identity);
    if(typetemp == 0)
    {
        temp.GetComponent<Dice>().angle = Random.Range(0, 359);
    }
    obstacleList.Add(temp);
    timeCheck = 0;
    nextTime = Random.Range(0.5f, 1.7f);
    starCount++;
    //배경 별 생성
    if(starCount >= 3)
    {
        StartCoroutine(StarGenerate(2, 10));
        starCount = 0;
    }
}

//리미터 아이템 생성
timeCheck1 += Time.deltaTime;
if(gameEnd == false && bigSanaModeON == false && timeCheck1 >= 4 + limiterNextTime - (speed / 15.0))
{
    GameObject temp =
    Instantiate(limiterGet,
    new Vector3(12, Random.Range(-border, border), 0), Quaternion.identity);
    temp.GetComponent<Dice>().angle = Random.Range(0, 359);
    timeCheck1 = 0;
    limiterNextTime = Random.Range(1f, 3.4f);
}
```
