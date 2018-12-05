using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Todo
    {
        ///<summary>
        ///Описание дела
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        ///Дата создания дела
        ///</summary>
        public DateTime CreatedDate { get; }

        ///<summary>
        ///Состояние дела
        ///</summary>
        public bool IsFinished { get; set; }

        public Todo()
        {
            IsFinished = false;
            CreatedDate = DateTime.Now;
        }
    }
}
