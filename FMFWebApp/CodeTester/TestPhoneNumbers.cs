
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTester
{
    class TestPhoneNumbers
    {
        public void Test1()
        {
            var region = "FO";
            String number = Console.ReadLine();
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber phoneNumber = phoneUtil.Parse(number, region);
                Console.WriteLine("Extension: " + phoneNumber.Extension);
                Console.WriteLine("Country code: " + phoneNumber.CountryCode);
                Console.WriteLine("NationalNumber: " + phoneNumber.NationalNumber);
            }
            catch (NumberParseException e)
            {
                Console.WriteLine("NumberParseException was thrown: " + e.ToString());
            }
            Console.ReadLine();
        }
    }
}
