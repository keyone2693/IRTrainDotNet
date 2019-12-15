using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
   public class FadakResult<T>
    {
        public int ExceptionId { get; set; }
        public string ExceptionMessage { get; set; }
        public T Result { get; set; }
         
    }
}
