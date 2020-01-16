using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Context
{
    public enum State
    {
        Open = 1,
        Started = 2,
        Closed = 3
    }
    public partial class Test
    {
        public int NumQuestions { get; set; }
    }

    public partial class AdministratedTest
    {
        public State StateEnum { get; set; } = State.Open;
    }
}
