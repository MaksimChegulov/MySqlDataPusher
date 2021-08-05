using System;
using System.Text;
using System.Security.Cryptography;

namespace MySqlDataPusher
{
    public class Hasher
    {
        private const string MACHINE_KEY = "your_core_machinekey";
        private const string PASSWORD_HASH = "fde9949f7b36de982464e77129945a6635885b7fba8725f02245577a579ff948";

        public string ComputeHash(string userId)
        {
            var stage1 = PASSWORD_HASH + userId + MACHINE_KEY;

            var stage2 = Encoding.UTF8.GetBytes(stage1);

            var stage3 = SHA512.Create().ComputeHash(stage2);

            return Convert.ToBase64String(stage3);
        }
    }
}
