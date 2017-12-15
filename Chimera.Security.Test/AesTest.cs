using System.Security.Cryptography;
using NUnit.Framework;

namespace Chimera.Security.Test
{
    [TestFixture]
    public class AesTest
    {
        [Test]
        public void IdempotencySucceeds()
        {
            const string plainText1 = "abcd1234";
            const string plainText2 = "abcd1234";

            string roundText = plainText1.Encrypt().Decrypt();

            Assert.AreEqual(plainText2, roundText);
        }

        [Test]
        public void DefaultRoundAboutSucceeds()
        {
            const string plainText = "abcd1234";
            string roundText = plainText.Encrypt().Decrypt();

            Assert.AreEqual(plainText, roundText);
        }

        [Test]
        public void CustomRoundAboutSucceeds()
        {
            const string plainText = "abcd1234";
            const string secret = "Passw0rd1";

            string roundText = plainText.Encrypt(secret).Decrypt(secret);

            Assert.AreEqual(plainText, roundText);
        }

        [Test]
        public void WrongKeyThrowsException()
        {
            const string plainText = "abcd1234";
            const string secret1 = "Passw0rd1";
            const string secret2 = "Passw0rd2";

            Assert.Throws(typeof (CryptographicException), () => plainText.Encrypt(secret1).Decrypt(secret2));
        }

        [Test]
        public void NullStringReturnsNonNull()
        {
            const string plainText = null;
            Assert.IsNotEmpty(plainText.Encrypt());
        }

        [Test]
        public void EmptyStringReturnsNonNull()
        {
            const string plainText = "";
            Assert.IsNotEmpty(plainText.Encrypt());
        }
    }
}
