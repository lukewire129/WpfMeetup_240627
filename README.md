## 개요
- ASP.NET CORE API를 통한 로컬 API서버와 SIGNAL을 통해 스케줄 알림봇을 운용할 수 있음.
- 윈도우서비스템플릿으로 시작해도 되고 AspnetCoreAPI로 시작해도 됨.
API Swagger가 기본적으로 세팅되어있는것을 확인하기 위해 AspnetCoreAPI로 만듬

## 목차
- [ ] API 서버 세팅 및 테스트
- [ ] 서비스 세팅
- [ ] SignalR 설정  
- [ ] API만들기

```
  다음달
- [ ] NCronJOB [추가 예정 기능](https://github.com/NCronJob-Dev/NCronJob/issues/83)
- [ ] 관리자 프로그램
```

## 1. API 서버 세팅 및 테스트
1. 윈도우 서비스확용 설치
    ```
    Microsoft.Extensions.Hosting.WindowsServices
    ```
2. 서비스 네임 설정
    ```CSharp
    builder.Services.AddWindowsService (options =>
    {
        options.ServiceName = "todobotapi";
    });
    ```
3. Kestrel을 이용한 포트설정
    ```CSharp
    builder.WebHost.ConfigureKestrel ((context, serverOptions) =>
    {
        serverOptions.Listen (IPAddress.Loopback, 555);
    });
    ```


## 2. 서비스 세팅
1. 서비스 생성하기
    ```
    sc create [서비스명] binPath=[api프로그램의절대경로] start=boot
    ```
2. 서비스 시작하기
   ```
   sc start [서비스명]
   ```
3. 서비스 중지하기
   ```
   sc stop [서비스명]
   ```
4. 서비스 삭제하기
   ```
   sc delete [서비스명]
   ```
## 3. SignalR 설정
1. SignalR 사용
   ```
   builder.Services.AddSignalR ();
   ```
2. 메세지허브 & MapHub연결
   클래스생성
   ```
   public class ChatHub : Hub
   {
       public async Task SendMessage(string user, string message)
       {
           await Clients.All.SendAsync ("ReceiveMessage", user, message);
       }
   }
   ```
   Maphub사용
   ```
   app.MapHub<ChatHub> ("/chatHub");
   ```
4. 알림클라이언트 MVVM 기본세팅
5. SignalR연결

## 4. API만들기
1. POST, GET API만 만들 계획
2. DTO 모델
