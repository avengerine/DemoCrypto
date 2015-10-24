using System;

namespace DemoCrypto
{
        class Program
        {
            static void Main(string[] args)
            {
                var plainText =
                "En un lugar de la mancha de cuyo nombre no quiero acordarme, no ha mucho que había un hidalgo...";

                var password = "$P45sW0rD%";
                var crypto = new DemoCryptoClass();
                
                var cipherText = crypto.EncryptText(plainText, password);

                Console.WriteLine("Clear text: " + plainText);
                Console.WriteLine("Encrypted text: " + cipherText);
                Console.ReadKey();

                var decipheredText = crypto.DecryptText(cipherText, password);

                Console.WriteLine("Encrypted text: " + cipherText);
                Console.WriteLine("Decrypted text: " + decipheredText);
                if (decipheredText.CompareTo(plainText) == 0)
                    Console.WriteLine("Texts are identical...");

                Console.ReadKey();
            }
        }
}
