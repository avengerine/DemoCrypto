using System.Security.Cryptography;
using NUnit.Framework;
using Should;
using SpecsFor;
using DemoCrypto;

namespace DemoCryptoTest
{
    public class EncryptionSuccess : SpecsFor<DemoCryptoClass>
    {
        private string password = "password";
        private string plainText = "This is the plain text..";
        private string cipherText = "";

        protected override void Given()
        {
            base.Given();
        }

        protected override void When()
        {
            cipherText = SUT.EncryptText(plainText, password);
        }

        [Test]
        public void ReturnIsNotNull()
        {
            cipherText.ShouldNotBeNull();
        }

        [Test]
        public void CipherTextIsNotEmpty()
        {
            cipherText.ShouldNotBeEmpty();
        }

        [Test]
        public void SecondEncryptionIsDifferent()
        {
            var d = new DemoCryptoClass();
            var ct = d.EncryptText(plainText, password);
            cipherText.ShouldNotEqual(ct);
        }

        [Test]
        public void PlainTextIsRecoverable()
        {
            var d = new DemoCryptoClass();
            var pt = d.DecryptText(cipherText, password);
            plainText.ShouldEqual(pt);
        }
    }
}
