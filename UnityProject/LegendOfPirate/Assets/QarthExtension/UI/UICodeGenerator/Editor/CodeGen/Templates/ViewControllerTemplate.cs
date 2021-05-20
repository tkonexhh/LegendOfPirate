using System;
using System.IO;

namespace Qarth.Extension
{
    public class ViewControllerDesignerTemplate
    {
        public static void Write(string name, string scriptsFolder, string scriptNamespace, PanelCodeInfo panelCodeInfo,
            UIKitSettingData uiKitSettingData)
        {
            var scriptFile = scriptsFolder + "/{0}.Designer.cs".FillFormat(name);
            var writer = File.CreateText(scriptFile);

            writer.WriteLine("// Generate Id:{0}".FillFormat(Guid.NewGuid().ToString()));
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine();

            if (uiKitSettingData.IsDefaultNamespace)
            {
                writer.WriteLine("// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间");
                writer.WriteLine("// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改");
            }

            writer.WriteLine("namespace {0}".FillFormat(scriptNamespace.IsTrimNullOrEmpty()
                ? uiKitSettingData.Namespace
                : scriptNamespace));
            writer.WriteLine("{");
            writer.WriteLine("\tpublic partial class {0}".FillFormat(name));
            writer.WriteLine("\t{");

            foreach (var bindInfo in panelCodeInfo.BindInfos)
            {
                writer.WriteLine("\t\tpublic {0} {1};".FillFormat(bindInfo.BindScript.ComponentName, bindInfo.Name));
            }

            writer.WriteLine();
            writer.WriteLine("\t}");
            writer.WriteLine("}");

            writer.Close();
        }
    }

    public class ViewControllerTemplate
    {
        public static void Write(string name, string scriptsFolder, string scriptNamespace,
            UIKitSettingData uiKitSettingData)
        {
            var scriptFile = scriptsFolder + "/{0}.cs".FillFormat(name);


            if (File.Exists(scriptFile))
            {
                return;
            }

            var writer = File.CreateText(scriptFile);

            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using Qarth;");
            writer.WriteLine();

            if (uiKitSettingData.IsDefaultNamespace)
            {
                writer.WriteLine("// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间");
                writer.WriteLine("// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改");
            }

            writer.WriteLine("namespace {0}".FillFormat(scriptNamespace.IsTrimNullOrEmpty()
                ? uiKitSettingData.Namespace
                : scriptNamespace));
            writer.WriteLine("{");
            writer.WriteLine("\tpublic partial class {0} : ViewController".FillFormat(name));
            writer.WriteLine("\t{");
            writer.WriteLine("\t\tvoid Start()");
            writer.WriteLine("\t\t{");
            writer.WriteLine("\t\t\t// Code Here");
            writer.WriteLine("\t\t}");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            writer.Close();
        }
    }
}