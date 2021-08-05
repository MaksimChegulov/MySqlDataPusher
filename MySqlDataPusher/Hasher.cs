using System;
using System.Text;
using System.Security.Cryptography;

namespace MySqlDataPusher
{
    public class Hasher
    {
        private const string MACHINE_KEY = "your_core_machinekey";
        private const string PASSWORD_HASH = "6fa58141339cd91c3e37619a08f4d6e97862e0263c89b200d68c8bdc2d1e87dd";

        public string ComputeHash(string userId)
        {
            var stage1 = PASSWORD_HASH + userId + MACHINE_KEY;

            var stage2 = Encoding.UTF8.GetBytes(stage1);

            var stage3 = SHA512.Create().ComputeHash(stage2);

            return Convert.ToBase64String(stage3);
        }
    }
}
