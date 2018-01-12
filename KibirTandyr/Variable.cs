using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KibirTandyr
{
    public class Variable
    {
        private const string NULL = "NULL";
        public int HandleCount = 0;
        private static Dictionary<int, Variable> Variables;

        public VariableType VarType { get; private set; }
        public object Value { get; private set; }
        public bool IsConst { get; private set; }
        public int Handle { get; private set; }

        private void RegisterSelf()
        {
            Handle = ++HandleCount;
            Variables.Add(Handle, this);
        }

        public Variable()
        {
            RegisterSelf();
            VarType = VariableType.Unknown;
            Value = NULL;
            IsConst = false;
        }

        public Variable(string source)
        {
            RegisterSelf();
            var vartype = ParseType(source);
            VarType = vartype;
            Value = DefaultValue(vartype);
            IsConst = false;
        }

        public Variable(string source, bool isconst)
        {
            RegisterSelf();
            var vartype = ParseType(source);
            VarType = vartype;
            Value = DefaultValue(vartype);
            IsConst = isconst;
        }

        public void SetValue(string newval)
        {
            RegisterSelf();
            if (IsConst) return;
            VarType = ParseType(newval);
            Value = newval;
        }

        public static VariableType ParseType(string source)
        {
            int strlen = source.Length;
            if (source == "true" || source == "false")
            {
                return VariableType.Bool;
            }
            else
            {
                int quotes = source.CountOfEx('\"', out int[] pos);
                if (quotes == 2 && pos[0] == 0 && pos[1] == strlen - 1)
                {
                    return VariableType.String;
                }
                int pts = source.CountOfEx('.', out pos);
                if (quotes == 0 && pts == 1 && pos[0] != 0)
                {
                    return VariableType.Float;
                }
                if (quotes == 0 && pts == 0)
                {
                    return VariableType.Int;
                }
            }
            return VariableType.Unknown;
        }

        public static object DefaultValue(VariableType vtype)
        {
            switch (vtype)
            {
                case VariableType.String:
                    return string.Empty;
                case VariableType.Int:
                    return 0;
                case VariableType.Float:
                    return 0;
                case VariableType.Bool:
                    return false;
                default: // Unknown
                    return NULL;
            }
        }

        public static Variable GetByHandle(int handle)
        {
            return Variables[handle];
        }
    }

    public enum VariableType
    {
        Unknown, // NULL
        String, // "hello world"
        Int, // 12345
        Float, // 3.1415
        Bool // true
    }
}
