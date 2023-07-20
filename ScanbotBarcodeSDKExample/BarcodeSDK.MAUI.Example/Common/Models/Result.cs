using System;
using BarcodeSDK.MAUI.Example.Common.Constants;

namespace BarcodeSDK.MAUI.Example.Common.Models
{
	public class Result
	{
        /// <summary>
        /// Status of the operation
        /// </summary>
        public OperationResult Status { get; set; }

        /// <summary>
        /// Error message descripting the reason of failure
        /// </summary>
        public string Error { get; set; }


        public static Result Succeed()
        {
            return new Result { Status = OperationResult.Ok };
        }

        public static Result Fail(string message)
        {
            return new Result
            {
                Status = OperationResult.Failed,
                Error = message
            };
        }
    }
}

