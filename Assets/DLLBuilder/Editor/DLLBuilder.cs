using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR_WIN
/*
 * DLLを生成する為のUnityEditor拡張クラス
 * @author katsumasa kimura 
 *
 * □初めに
 * 必要となるのはDllBuilderWindow.csのみです。
 * 各自のプロジェクトのEditorフォルダーにこのファイルを放り込んで下さい。
 * Macを所有していない為、現在対応している環境はWindowsのみです。
 *
 * □使い方
 * ・Windows->DLLBuilderを選択してDLLBuilderWindowを開く
 * ・ProjectWindowからDLLを生成したいC#のコードを選択する
 * ・[Create]ボタンを押す
 * 以上です。
 * DLLはプロジェクト直下のPluginフォルダに生成されます。
 * 
 * 概ね上記の内容で問題ありませんが、下記の点に注意してください。
 * ・Unityのインストール先がunityEditorDirと異なる場合
 * →インストール先に合わせてパスを修正して下さい。
 * ・UnityEditorを必要とするDLLを作成する場合
 * →isUseUnityEditorDLLにチェックを入れて下さい。
 * 
 */
public class DLLBuilderWindow : ScriptableWizard
{
    /// <summary>
    /// UnityEngine.dllを必要とするか
    /// </summary>
    public bool m_isUseUnityEngineDLL = true;
    
    /// <summary>
    /// UnityEditor.dllを必要とするか
    /// </summary>
    public bool m_isUseUnityEditorDLL = false;

    /// <summary>
    /// UnityEditorのインストール先ディレクトリ
    /// </summary>
    public string m_unityEditorDir = "C:/Program Files/Unity/Editor";
    

    /// <summary>
    /// DLLBuilder用のWindowを開く
    /// </summary>
    [MenuItem("Window/DLLBuilder")]
    static void Open()
    {
        DisplayWizard<DLLBuilderWindow>("DLLBuilder");        
    }


    /// <summary>
    /// DLLを作成する
    /// </summary>
    private void OnWizardCreate()
    {
        var projectPath = System.IO.Directory.GetCurrentDirectory();
        var outputPath = projectPath + "/Plugins";
        if (!System.IO.Directory.Exists(outputPath))
        {
            System.IO.Directory.CreateDirectory(outputPath);
        }

        var process = new System.Diagnostics.Process();
        process.StartInfo.FileName = "\"" + m_unityEditorDir + "/Data/Mono/bin/smcs.bat\"";
        process.StartInfo.Arguments = "";

        if (m_isUseUnityEngineDLL)
        {
            process.StartInfo.Arguments += " -r:\"" + m_unityEditorDir + "/Data/Managed/UnityEngine.dll\"";
        }
        if (m_isUseUnityEditorDLL)
        {
            process.StartInfo.Arguments += " -r:\"" + m_unityEditorDir + "/Data/Managed/UnityEditor.dll\"";
        }
        string sources = "";
        string dllName = "";
        foreach (var obj in Selection.objects)
        {   
            var assetPath = AssetDatabase.GetAssetPath(obj);
            var fnameBase = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            dllName = fnameBase;
            sources += projectPath + "/" + assetPath + " ";
        }
        process.StartInfo.Arguments += " -target:library -out:" + outputPath + "/" + dllName + ".dll " + sources;
        //Debug.Log(process.StartInfo.FileName + " " + process.StartInfo.Arguments);
        process.Start();
        process.WaitForExit();
    }
}
#endif