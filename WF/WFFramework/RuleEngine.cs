using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WF.Common;

namespace WF.WFFramework
{
    public class Val
    {
        public string name;
        public int type;
        public string value;
    }
    public class RuleEngine
    {
        /// <summary>
        /// 判断是否符合表达式（变量只支持Int和bool类型）
        /// </summary>
        /// <returns>全大写无连接线的Guid串</returns>
        public static bool IsConformExpress(List<Val> map, string express)
        {
            bool res = true;
            if (string.IsNullOrEmpty(express) || express.Trim() != "" || express.Trim().Length != 0)
            {
                return true;
            }
            // 1.CSharpCodePrivoder
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.ICodeComplier
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            // 3.CompilerParameters
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;

            // 4.CompilerResults
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GenerateCode(map, express));

            if (cr.Errors.HasErrors)
            {
                Console.WriteLine("编译错误：");
                foreach (CompilerError err in cr.Errors)
                {
                    Console.WriteLine(err.ErrorText);
                }
            }
            else
            {
                // 通过反射，调用HelloWorld的实例
                Assembly objAssembly = cr.CompiledAssembly;
                object DynamicFund = objAssembly.CreateInstance("DynamicCodeGenerate.DynamicFund");
                MethodInfo objMI = DynamicFund.GetType().GetMethod("OutPut");
                res = Utility.ConvertToBool(objMI.Invoke(DynamicFund, null));
            }
            return res;
        }

        static string GenerateCode(List<Val> map, string express)
        {
            StringBuilder varDefinition = new StringBuilder();
            if (map != null && map.Count > 0)
            {
                foreach (Val item in map)
                {
                    if (item.type == (int)ValType.STRING)
                    {
                        varDefinition.Append( " string " + item.name + "=\"" + item.value + "\";");
                    }
                    else
                    {
                        varDefinition.Append(" int " + item.name + "=" + item.value + ";");
                    }
                    varDefinition.Append(Environment.NewLine);
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace DynamicCodeGenerate");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    public class DynamicFund");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("        public bool OutPut()");
            sb.Append(Environment.NewLine);
            sb.Append("        {");
            sb.Append(Environment.NewLine);
            sb.Append(varDefinition.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("             return " + express + ";");
            sb.Append(Environment.NewLine);
            sb.Append("        }");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            string code = sb.ToString();
            return code;
        }
    }
}
