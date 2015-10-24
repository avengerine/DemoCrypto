﻿using DemoCrypto;
using Should;
using NUnit.Framework;
using SpecsFor;

namespace DemoCryptoTest
{
    class DecryptionSuccess : SpecsFor<DemoCryptoClass>
    {
        private string password = "password";
        private string plainText = "This is the plain text..";
        private string cipherText = "";
        private string recoveredText = "";

        protected override void Given()
        {
            cipherText = SUT.EncryptText(plainText, password);
        }

        protected override void When()
        {
            recoveredText = SUT.DecryptText(cipherText, password);
        }

        [Test]
        public void TextRecoveredMatchesPlainText()
        {
            recoveredText.ShouldEqual(plainText);
        }
    }
}
