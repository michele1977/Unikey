using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public enum StateEnum
    {
        Created = 1, Open , Started , Closed 
    }
    public class State
    {
        public StateEnum StateType { get; set; }

        public void SetCreated()
        {
            throw new NotImplementedException();
        }

        public void SetOpen()
        {
            throw new NotImplementedException();
        }

        public void SetStarted()
        {
            throw new NotImplementedException();
        }

        public void SetClosed()
        {
            throw new NotImplementedException();
        }

    }
}