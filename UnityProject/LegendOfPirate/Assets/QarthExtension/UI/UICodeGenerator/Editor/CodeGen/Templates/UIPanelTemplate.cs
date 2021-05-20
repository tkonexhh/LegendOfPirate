
namespace Qarth.Extension
{
    using System.IO;

    public class UIPanelTemplate
    {
        public static void Write(string name, string srcFilePath, string scriptNamespace,
            UIKitSettingData uiKitSettingData)
        {
            var scriptFile = srcFilePath;

            if (File.Exists(scriptFile))
            {
                return;
            }

            var writer = File.CreateText(scriptFile);

            var codeWriter = new FileCodeWriter(writer);


            var rootCode = new RootCode()
                .Using("UnityEngine")
                .Using("UnityEngine.UI")
                .Using("Qarth")
                .Using("UniRx")
                .EmptyLine()
                .Namespace(scriptNamespace, nsScope =>
                {
                    nsScope.Class(name + "Data", "UIPanelData", false, false, classScope => { });

                    nsScope.Class(name, "AbstractAnimPanel", true, false, classScope =>
                    {
                        classScope.CustomScope("protected override void OnUIInit()", false,
                            (function) => { });

                        classScope.EmptyLine();

                        classScope.CustomScope("protected override void OnPanelOpen(params object[] args)", false,
                            function =>
                            {
                                function.Custom("base.OnPanelOpen(args);");
                            });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnPanelHideComplete()", false,
                            function => 
                            {
                                function.Custom("base.OnPanelHideComplete();");
                                function.Custom("CloseSelfPanel();");
                            });

                        //classScope.EmptyLine();
                        //classScope.CustomScope("protected override void OnShow()", false,
                        //    function => { });
                        //classScope.EmptyLine();
                        //classScope.CustomScope("protected override void OnHide()", false,
                        //    function => { });

                        //classScope.EmptyLine();
                        //classScope.CustomScope("protected override void OnClose()", false,
                        //    function => { });
                    });
                });

            rootCode.Gen(codeWriter);
            codeWriter.Dispose();
        }
    }
}