using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ExceptionModeel<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the output is executed 
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        public T Data { get; set; }
    }
}
