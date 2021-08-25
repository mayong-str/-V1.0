using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.pojo
{
    class orders
    {
        private int ID;
        private string order_no;
        private string product_no;
        private string target;
        private string complete;
        private string completion;
        private string number;
        private string type;
        private string Date;

        public int ID1 
        { 
            get => ID; 
            set => ID = value; 
        }
        public string Order_no 
        { 
            get => order_no; 
            set => order_no = value; 
        }
        public string Product_no 
        {
            get => product_no; 
            set => product_no = value; 
        }
        public string Target 
        { 
            get => target;
            set => target = value; 
        }
        public string Complete 
        { 
            get => complete;
            set => complete = value; 
        }
        public string Completion 
        {
            get => completion; 
            set => completion = value;
        }
        public string Number 
        { 
            get => number; 
            set => number = value; 
        }
        public string Type
        { 
            get => type; 
            set => type = value; 
        }
        public string Date1 
        { 
            get => Date; 
            set => Date = value; 
        }
    }
}
