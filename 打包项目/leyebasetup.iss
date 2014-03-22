; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "乐业吧"
#define MyAppVersion "1.0.0.10"
#define MyAppPublisher "常州智业网络科技有限公司"
#define MyAppURL "http://leyeba.net/"
#define MyAppExeName "leyeba.exe"
#define MyAppID "{EB7A49A0-A8E4-4A64-8C80-C5268E6361C2}"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{#MyAppID}
AppName={#MyAppName}
AppVersion={#MyAppVersion}AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\leyeba
DefaultGroupName=乐业吧
AllowNoIcons=yes
OutputBaseFilename=leyebaSetup
SetupIconFile=logo.ico
Compression=lzma
SolidCompression=yes
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}
PrivilegesRequired=admin 

[Languages]
Name: "chs"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; OnlyBelowVersion: 0,6.1

[Files]
Source: "Aliyun.OpenServices.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "AutoUpdate.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "AutoUpdate.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "ControlEx.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Interop.shell32.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "leyeba.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "leyeba.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "logConfig.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "OssModule.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "Util.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "http://leyeba.net/"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon
Name: "{userdesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}";

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent

[CustomMessages]
chs.dotnet=此程序运行需要Microsoft .NET Framework 4.0，这个组件可能没有安装在您的系统中。%n是否现在进入下载页并退出安装还是继续安装程序（应用程序可能会无法成功运行）？
chs.link=http://www.microsoft.com/zh-cn/download/details.aspx?id=17718


[Code]
//source --> http://www.kynosarges.org/DotNetVersion.html
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//    'v4.5'          .NET Framework 4.5
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, serviceCount: cardinal;
    success: boolean;
begin
    // installation key group for all .NET versions
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;
    success := RegQueryDWordValue(HKLM, key, 'Install', install);
    success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);

    result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;   
var
  err: integer;
  ResultStr: String;
  ResultCode: Integer;
begin
    if (not IsDotNetDetected('v4\Full', 0)) then begin
        if MsgBox(ExpandConstant('{cm:dotnet}'), mbConfirmation, MB_YESNO) = IDYES then begin
            ShellExecAsOriginalUser('open', ExpandConstant('{cm:link}'),'', '', SW_SHOW, ewNoWait, err);
            result := false;
        end else
            result := false;
    end else
        result := true;
    if RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1', 'UninstallString', ResultStr) then
    begin
    if MsgBox('安装程序检测到您已安装乐业吧的旧版本，点击是卸载旧版本后继续。', mbConfirmation, MB_YESNO) = IDYES then
        begin           
             ResultStr := RemoveQuotes(ResultStr);
             Exec(ResultStr, '/silent', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
             result := true;
        end else
            result := false;
    end else
        result := true;
end;
var
ErrorCode: Integer;
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep=ssDone then
    ShellExec('taskbarpin', ExpandConstant('{userdesktop}\乐业吧.lnk'), '', '', SW_SHOWNORMAL, ewNoWait,ErrorCode);
end;

procedure DeinitializeUninstall();
begin
    ShellExec('taskbarunpin', ExpandConstant('{userdesktop}\乐业吧.lnk'), '', '', SW_SHOWNORMAL, ewNoWait,ErrorCode);
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
if CurUninstallStep = usUninstall then
  if MsgBox('您是否要删除用户配置信息？', mbConfirmation, MB_YESNO) = IDYES then
    //删除 {app} 文件夹及其中所有文件
    DelTree(ExpandConstant('{app}'), True, True, True);
end;