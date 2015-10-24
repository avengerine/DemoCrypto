using NUnit.Framework;
using Should;
using SpecsFor;
using DemoCrypto;

namespace DemoCryptoTest
{
    public class WhenEncryptionSuccess : SpecsFor<DemoCryptoClass>
    {
        private const string Password = "password";
        private const string PlainText = "This is the plain text..";
        private string _cipherText;

        protected override void When()
        {
            _cipherText = SUT.EncryptText(PlainText, Password);
        }

        [Test]
        public void ReturnIsNotNull()
        {
            _cipherText.ShouldNotBeNull();
        }

        [Test]
        public void CipherTextIsNotEmpty()
        {
            _cipherText.ShouldNotBeEmpty();
        }

        [Test]
        public void SecondEncryptionIsDifferent()
        {
            var d = new DemoCryptoClass();
            var ct = d.EncryptText(PlainText, Password);
            _cipherText.ShouldNotEqual(ct);
        }

        [Test]
        public void PlainTextIsRecoverable()
        {
            var d = new DemoCryptoClass();
            var pt = d.DecryptText(_cipherText, Password);
            PlainText.ShouldEqual(pt);
        }
    }
}
