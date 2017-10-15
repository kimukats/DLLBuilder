# DLLBuilder
UnityEditor上からDLLを生成する為のUnityEditor拡張クラスです。
 
□初めに

必要となるのはDllBuilderWindow.csのみです。
各自のプロジェクトのEditorフォルダーにこのファイルを放り込んで使用して下さい。
Macを所有していない為、現在対応している環境はWindowsのみです。


□使い方

Windows->DLLBuilderを選択してDLLBuilderWindowを開く
ProjectWindowからDLLを生成したいC#のコードを選択する
[Create]ボタンを押す

以上です。

DLLはプロジェクト直下のPluginフォルダに生成されます。
DLLとC#のファイルを差し替えてお使い下さい。

□注意事項

・Unityのインストール先がunityEditorDirと異なる場合

→インストール先に合わせてパスを修正して下さい。

・UnityEditorを必要とするDLLを作成する場合

→isUseUnityEditorDLLにチェックを入れて下さい。 
