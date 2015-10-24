using System;
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
            ThrowsException = true;
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
            public void ExceptionShouldBe()
            {
                Exception.ShouldBeType<ArgumentNullException>();
            }
        }

        class AndEncryptedTextIsShorterThanIv : WhenDecryption
        {
            protected override void Given()
            {
                base.Given();
                EncryptedText = "wrong";
            }

            [Test]
            public void AnExceptionIsThrown()
            {
                ThrowsException.ShouldBeTrue();
            }

            [Test]
            public void ExceptionShouldBe()
            {
                Exception.ShouldBeType<FormatException>();
            }

        }
    }
}
