using System;
using WCFService.Business_Service;

namespace WCFService
{
    public class PhoneService : IPhoneService
    {

        private int _randomCode;

        public bool CodeIdentic(string phone)
        {
            _randomCode = new Random().Next();
            Console.WriteLine("Secret code checker for phone ({0}): {1}", phone, _randomCode);
            int enteredCodeInt = _randomCode;
            bool f = enteredCodeInt == _randomCode;
            Console.WriteLine(f ? "Passed successfully" : "Some error, try again");
            return f;
        }
    }
}
