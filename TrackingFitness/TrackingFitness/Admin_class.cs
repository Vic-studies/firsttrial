using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFitness
{
    class Admin_class
    {
        private string _ID, _name, _pass, _gen, _age, _num, _email;
        public string adID
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
        public string adname
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
        public string adpassword
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
        public string adgen
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
        public string adage
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
        public string adnum
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
        public string ademail
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
