using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFitness
{
    class Member_class
    {
        private string _name, _ID, _pass, _gen, _age, _num,_email;
        private int _weight, _height;
        public string memName
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        public string memID
        {
            set
            {
                _ID = value;
            }
            get
            {
                return _ID;
            }
        }
        public string mempassword
        {
            set
            {
                _pass = value;
            }
            get
            {
                return _pass;
            }
        }
        public string memgender
        {
            set
            {
                _gen = value;
            }
            get
            {
                return _gen;
            }
        }
        public string memage
        {
            set
            {
                _age = value;
            }
            get
            {
                return _age;
            }
        }
        public string memnum
        {
            set
            {
                _num = value;
            }
            get
            {
                return _num;
            }
        }
        public int memweight
        {
            set
            {
                _weight = value;
            }
            get
            {
                return _weight;
            }
        }
        public int memheight
        {
            set
            {
                _height = value;
            }
            get
            {
                return _height;
            }
        }
        public string mememail
        {
            set
            {
                _email = value;
            }
            get
            {
                return _email;
            }
        }
    }
}
