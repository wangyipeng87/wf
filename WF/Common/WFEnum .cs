using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common
{
   public enum InstanceState
    {
        Enable = 1,
        Disable=0,
        Hang=2,
    }
    public enum ValType
    {
        INT = 1,
        STRING = 0,
    }
    public enum Operation
    {
        Start = 1,
        Apply = 2,
        Reject = 3,
        GoTo = 4,
        Transfer = 5,
        Add = 6,
        Read = 7,
        CallBack = 8,
    }
    public enum State
    {
        Disable = 0,
        Enable = 1
    }
    public enum IsDelete
    {
        UnDelete = 0,
        Deleteed = 1
    }

    public enum WFNodeType
    {
        BeginNode = 1,
        EndNode = 2,
        Normal=3,
    }
    public enum WFProcessType
    {
        User = 1,
        Role = 2,
        Custom = 3,
    }
    public enum TodoState
    {
        UnDo = 1,
        Done = 2
    }
    public enum TodoType
    {
        Normal = 1,
        Read = 2,
        Add = 3,
        Redirect = 4,
    }

    public enum TodoIsShow
    {
        Show = 1,
        Hide = 2,
    }
}
