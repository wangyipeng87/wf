using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common
{
    /// <summary>
    /// 流程实例状态
    /// </summary>
   public enum InstanceState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable=0,
        /// <summary>
        /// 挂起
        /// </summary>
        Hang=2,
        /// <summary>
        /// 结束
        /// </summary>
        End = 3,
    }
    /// <summary>
    /// 流程变量类型
    /// </summary>
    public enum ValType
    {
        /// <summary>
        /// int 类型
        /// </summary>
        INT = 1,
        /// <summary>
        /// string 类型
        /// </summary>
        STRING = 0,
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// 启动
        /// </summary>
        Start = 1,
        /// <summary>
        /// 同意
        /// </summary>
        Apply = 2,
        /// <summary>
        /// 驳回
        /// </summary>
        Reject = 3,
        /// <summary>
        /// 跳转
        /// </summary>
        GoTo = 4,
        /// <summary>
        /// 转签
        /// </summary>
        Redirect = 5,
        /// <summary>
        /// 加签
        /// </summary>
        Add = 6,
        /// <summary>
        /// 已阅
        /// </summary>
        Read = 7,
        /// <summary>
        /// 回撤
        /// </summary>
        CallBack = 8,
        /// <summary>
        /// 取消待办
        /// </summary>
        Cancel = 9,
        /// <summary>
        /// 传阅
        /// </summary>
        Transmit=10,
        /// <summary>
        /// 挂起
        /// </summary>
        Hang = 11,
    }
    /// <summary>
    /// 是否启用状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        Enable = 1
    }
    /// <summary>
    /// 是否删除状态
    /// </summary>
    public enum IsDelete
    {
        /// <summary>
        /// 未删除
        /// </summary>
        UnDelete = 0,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleteed = 1
    }
    /// <summary>
    /// 流程节点类型
    /// </summary>
    public enum WFNodeType
    {
        /// <summary>
        /// 开始节点
        /// </summary>
        BeginNode = 1,
        /// <summary>
        /// 结束节点
        /// </summary>
        EndNode = 2,
        /// <summary>
        /// 一般类型节点
        /// </summary>
        Normal=3,
    }
    /// <summary>
    /// 节点操作人获取方式
    /// </summary>
    public enum WFProcessType
    {
        /// <summary>
        /// 根据具体用户获取操作人
        /// </summary>
        User = 1,
        /// <summary>
        /// 根据具体的角色获取操作人
        /// </summary>
        Role = 2,
        /// <summary>
        /// 自定义获取操作人
        /// </summary>
        Custom = 3,
    }
    /// <summary>
    /// 流程待办的状态
    /// </summary>
    public enum TodoState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        UnDo = 1,
        /// <summary>
        /// 已处理
        /// </summary>
        Done = 2,
    }
    /// <summary>
    /// 流程待办类型
    /// </summary>
    public enum TodoType
    {
        /// <summary>
        /// 一般类型
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 传阅类型待办
        /// </summary>
        Read = 2,
        /// <summary>
        /// 加签类型待办
        /// </summary>
        Add = 3,
        /// <summary>
        /// 转签类型待办
        /// </summary>
        Redirect = 4,
    }
    /// <summary>
    /// 待办是否显示
    /// </summary>
    public enum TodoIsShow
    {
        /// <summary>
        /// 显示待办
        /// </summary>
        Show = 1,
        /// <summary>
        /// 不显示待办
        /// </summary>
        Hide = 2,
    }
}
