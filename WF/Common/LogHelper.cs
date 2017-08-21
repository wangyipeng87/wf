using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.Common
{
    /// <summary>
    /// 使用LOG4NET记录日志的功能，在WEB.CONFIG里要配置相应的节点
    /// </summary>
    public class LogHelper
    {
        ////log4net日志专用
        public static readonly ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        public static readonly ILog logerror = log4net.LogManager.GetLogger("logerror");
        /// <summary>
        /// 普通的文件记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(string info, Exception se)
        {
            loginfo.Error(info, se);
        }
    }
}
