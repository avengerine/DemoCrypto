using System;
using NUnit.Framework;
using Should;
using SpecsFor;
using DemoCrypto;

namespace DemoCryptoTest
{
    public class WhenEncryption : SpecsFor<DemoCryptoClass>
    {
        protected string Password;
        protected string PlainText;
        protected string CipherText;
        protected Exception Exception;
        protected bool ThrowsException;

        protected override void When()
        {
            try
            {
                CipherText = SUT.EncryptText(PlainText, Password);
            }
            catch (Exception e)
            {
                Exception = e;
                ThrowsException = true;
            }
            
        }

        class AndPasswordIsNull : WhenEncryption
        {
            protected override void Given()
            {
                Password = null;
                ThrowsException = false;
            }

            [Test]
            public void EncryptionThrowsException()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionIsArgumentNull()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndPlainTextIsNull : WhenEncryption
        {
            protected override void Given()
            {
                PlainText = null;
                ThrowsException = false;
            }

            [Test]
            public void EncryptionThrowsException()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionIsArgumentNull()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndPlainTextIsEmpty : WhenEncryption
        {
            protected override void Given()
            {
                PlainText = "";
                ThrowsException = false;
            }

            [Test]
            public void EncryptionThrowsException()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionIsArgumentNull()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndIsSuccessful : WhenEncryption
        {
            protected override void Given()
            {
                Password = "password";
                PlainText = "This is the plain text..";
                ThrowsException = false;
            }

            [Test]
            public void CipherTextIsNotEmpty()
            {
                CipherText.ShouldNotBeEmpty();
            }

            [Test]
            public void SecondEncryptionIsDifferent()
            {
                var d = new DemoCryptoClass();
                var ct = d.EncryptText(PlainText, Password);
                CipherText.ShouldNotEqual(ct);
            }

            [Test]
            public void PlainTextIsRecoverable()
            {
                var d = new DemoCryptoClass();
                var pt = d.DecryptText(CipherText, Password);
                PlainText.ShouldEqual(pt);
            }
        }
    }
}
