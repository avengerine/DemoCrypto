using System;
using System.Security.Cryptography;
using DemoCrypto;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace DemoCryptoTest
{
    class WhenDecryption : SpecsFor<DemoCryptoClass>
    {
        protected string Password;
        protected string PlainText; 
        protected string EncryptedText;
        protected string RecoveredText;
        protected Exception Exception;
        protected bool ThrowsException;

        protected override void Given()
        {
            Password = "password";
            PlainText = "This is the plain text";
            ThrowsException = false;
            EncryptedText = SUT.EncryptText(PlainText, Password);
        }

        protected override void When()
        {
            try
            {
                RecoveredText = SUT.DecryptText(EncryptedText, Password);
            } catch (Exception e)
            {
                Exception = e;
                ThrowsException = true;
            }
        }

        class AndPasswordIsNull : WhenDecryption
        {
            protected override void Given()
            {
                base.Given();
                Password = null;
            }

            [Test]
            public void AnExceptionIsThrown()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionShouldBe()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndInputIsNull : WhenDecryption
        {
            protected override void Given()
            {
                base.Given();
                EncryptedText = null;
            }

            [Test]
            public void AnExceptionIsThrown()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionIsArgumentNull()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndEncryptedTextHasBeenChanged : WhenDecryption
        {
            protected override void Given()
            {
                base.Given();
                EncryptedText = "ie/UVnv7yO8pGRObtnNeGsLqQe8eewfp/C9n4Up5Zzvob/i+6OFf77w8eIhK/IWnILfgZFFAA3Y3mZHc0NRQjrT9jFuamittF5+IpdyCAIFjPYw1j1x2x1QOIaYB08I0Hu5dJnJQKrtbh74tuR7ZJEB/+uRQff/jBZgbedBaQEU=";
            }

            [Test]
            public void AnExceptionIsThrown()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionTypeIsCryptographic()
            {
                Exception.ShouldBeType<CryptographicException>();
            }
        }

        class AndIsSuccessful : WhenDecryption
        {
            [Test]
            public void NoExceptionIsThrown()
            {
                ThrowsException.ShouldBeFalse();
            }

            [Test]
            public void CipherTextIsNotEmpty()
            {
                EncryptedText.ShouldNotBeEmpty();
            }

            [Test]
            public void TextIsRecoverable()
            {
                RecoveredText.ShouldEqual(PlainText);
            }

            [Test]
            public void SecondEncryptionIsDifferent()
            {
                var d = new DemoCryptoClass();
                var ct = d.EncryptText(PlainText, Password);
                EncryptedText.ShouldNotEqual(ct);
            }
        }
    }
}
