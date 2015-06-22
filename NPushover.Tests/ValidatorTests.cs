using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPushover.Validators;
using NPushover.Exceptions;
using NPushover.RequestObjects;

namespace NPushover.Tests
{
    [TestClass]
    public class ValidatorTests
    {
        [TestMethod]
        public void ApplicationKeyValidator_WorksCorrectly()
        {
            var target = new ApplicationKeyValidator();
            target.Validate("test", "azGDORePK8gMaC0QOYAMyEEuzJnyUi");  //Example app-key taken from https://pushover.net/api#registration
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidKeyException))]
        public void ApplicationKeyValidator_ThrowsOnInvalidKey()
        {
            var target = new ApplicationKeyValidator();
            target.Validate("test", "ThisShouldFail");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationKeyValidator_ThrowsOnNullValue()
        {
            var target = new ApplicationKeyValidator();
            target.Validate("test", null);
        }

        //===

        [TestMethod]
        public void UserOrGroupKeyValidator_WorksCorrectly_ForUserKeys()
        {
            var target = new UserOrGroupKeyValidator();
            target.Validate("test", "uQiRzpo4DXghDmr9QzzfQu27cmVRsG");  //Example user key taken from https://pushover.net/api#identifiers
        }

        [TestMethod]
        public void UserOrGroupKeyValidator_WorksCorrectly_ForGroupKeys()
        {
            var target = new UserOrGroupKeyValidator();
            target.Validate("test", "gznej3rKEVAvPUxu9vvNnqpmZpokzF");  //Example group key taken from https://pushover.net/api#identifiers
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidKeyException))]
        public void UserOrGroupKeyValidator_ThrowsOnInvalidKey()
        {
            var target = new UserOrGroupKeyValidator();
            target.Validate("test", "ThisShouldFail");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserOrGroupKeyValidator_ThrowsOnNullValue()
        {
            var target = new UserOrGroupKeyValidator();
            target.Validate("test", null);
        }

        [TestMethod]
        public void DeviceNameValidator_WorksCorrectly()
        {
            var target = new DeviceNameValidator();
            target.Validate("test", "droid2");                          //Example devicename taken from https://pushover.net/api#registration
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidKeyException))]
        public void DeviceNameValidator_ThrowsOnInvalidKey()
        {
            var target = new DeviceNameValidator();
            target.Validate("test", "Th!s$houldFa!l");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeviceNameValidator_ThrowsOnNullValue()
        {
            var target = new DeviceNameValidator();
            target.Validate("test", null);
        }

        [TestMethod]
        public void ReceiptValidator_WorksCorrectly()
        {
            var target = new ReceiptValidator();
            target.Validate("test", "yHKcl6eN0Gz9AXR22CAtslEVX8DxCc");  //Example receipt generated based on https://pushover.net/api#receipt
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidKeyException))]
        public void ReceiptValidator_ThrowsOnInvalidKey()
        {
            var target = new ReceiptValidator();
            target.Validate("test", "Th!s$houldFa!l");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReceiptValidator_ThrowsOnNullValue()
        {
            var target = new ReceiptValidator();
            target.Validate("test", null);
        }

        [TestMethod]
        public void EMailValidator_WorksCorrectly()
        {
            var target = new EMailValidator();
            target.Validate("test", "foo@bar.com");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EMailValidator_ThrowsOnInvalidEmail()
        {
            var target = new EMailValidator();
            target.Validate("test", "ThisShouldFail");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EMailValidator_ThrowsOnNullValue()
        {
            var target = new EMailValidator();
            target.Validate("test", null);
        }

        [TestMethod]
        public void DefaultMessageValidator_WorksCorrectly()
        {
            var target = new DefaultMessageValidator();
            var message = Message.Create("Minimal message");
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultMessageValidator_ThrowsOnNullValue()
        {
            var target = new DefaultMessageValidator();
            target.Validate("test", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultMessageValidator_ThrowsOnNullBody()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = null, Priority = Priority.Normal };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnBodyLimitExceeded()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = new string('X', 1025), Priority = Priority.Normal };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnTitleLimitExceeded()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = "Test", Priority = Priority.Normal, Title = new string('X', 251) };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnInvalidPriority()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = "Test", Priority = (Priority)999 };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DefaultMessageValidator_ThrowsOnRetryOptionsForNonEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = "Test", Priority = Priority.Normal, RetryOptions = new RetryOptions { } };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultMessageValidator_ThrowsOnMissingRetryOptionsForEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message { Body = "Test", Priority = Priority.Emergency };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnTooShortRetryIntervalForEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Emergency,
                RetryOptions = new RetryOptions { RetryEvery = TimeSpan.FromSeconds(29), RetryPeriod = TimeSpan.FromHours(1) }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnTooLongRetryIntervalForEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Emergency,
                RetryOptions = new RetryOptions { RetryEvery = TimeSpan.FromHours(25), RetryPeriod = TimeSpan.FromHours(1) }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnTooLongRetryPeriodForEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Emergency,
                RetryOptions = new RetryOptions { RetryEvery = TimeSpan.FromSeconds(30), RetryPeriod = TimeSpan.FromHours(25) }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnNegativeRetryPeriodForEmergencyMessage()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Emergency,
                RetryOptions = new RetryOptions { RetryEvery = TimeSpan.FromSeconds(30), RetryPeriod = TimeSpan.FromSeconds(-1) }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultMessageValidator_ThrowsOnSupplementaryURLNullUri()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Normal,
                SupplementaryUrl = new SupplementaryURL { Uri = null, Title = "Foo" }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        public void DefaultMessageValidator_AllowsSupplementaryURLNullTitle()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Normal,
                SupplementaryUrl = new SupplementaryURL { Uri = new Uri("http://robiii.me"), Title = null }
            };
            target.Validate("test", message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DefaultMessageValidator_ThrowsOnSupplementaryURLTooLongTitle()
        {
            var target = new DefaultMessageValidator();
            var message = new Message
            {
                Body = "Test",
                Priority = Priority.Normal,
                SupplementaryUrl = new SupplementaryURL { Uri = new Uri("http://robiii.me"), Title = new string('X', 101) }
            };
            target.Validate("test", message);
        }
    }
}
