using DemoCrypto;
using Should;
using NUnit.Framework;
using SpecsFor;

namespace DemoCryptoTest
{
    class DecryptionSuccess : SpecsFor<DemoCryptoClass>
    {
        private const string Password = "password";
        private const string PlainText = "This is the plain text..";
        private string _cipherText;
        private string _recoveredText;

        protected override void Given()
        {
            _cipherText = SUT.EncryptText(PlainText, Password);
        }

        protected override void When()
        {
            _recoveredText = SUT.DecryptText(_cipherText, Password);
        }

        [Test]
        public void TextRecoveredMatchesPlainText()
        {
            _recoveredText.ShouldEqual(PlainText);
        }
    }
}
