using System;

namespace Cdis.Brisk.Domain.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultRequestDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public object Return { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsException { get; private set; }

        /// <summary>
        ///         
        /// </summary>        
        public ResultRequestDomain(object result, bool isSuccess, string message)
        {
            Return = result;
            IsSuccess = isSuccess;
            Message = message;
            IsException = false;
        }

        /// <summary>
        /// 
        /// </summary>        
        public ResultRequestDomain(Exception exception)
        {
            string msgErro = exception.InnerException != null
                                ? exception.InnerException.InnerException != null
                                  ? exception.InnerException.InnerException.Message
                                  : exception.InnerException.Message
                                : exception.Message;

            Return = null;
            IsSuccess = false;
            Message = msgErro;
            IsException = true;
        }
    }
}
