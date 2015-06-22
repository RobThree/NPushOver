# ![Logo](https://raw.githubusercontent.com/RobThree/NPushOver/master/Logo/logo_48.png) NPushOver
.Net Pushover client (https://pushover.net), available as a [NuGet package](https://www.nuget.org/packages/NPushover/).

**This library is not written or supported by [Superblock](http://superblock.net/) (the creators of [Pushover](https://pushover.net)).**

## Quick start:

* [Register your application](https://pushover.net/apps/build), set its name (and optionally upload an icon) and get an API token in return (referred to as `ApplicationKey` in NPushover).
* Create an instance of the `Pushover` class and provide it with the `ApplicationKey`.
* You're all set to send your first message!

```c#
var po = new Pushover("[APPLICATIONKEY-HERE]");

// Quick message:
var msg = Message.Create("Hello world!");
var sendtask = po.SendMessageAsync(msg, "[RECIPIENT-ID-HERE]");
```

The `Message` class contains several convenience-methods to quickly create a `Message`; however you can also simply instantiate your own:

```c#
var po = new Pushover("[APPLICATIONKEY-HERE]");

// Quick message:
var msg = new Message(Sounds.Siren)
{
    Title = "The roof!",
    Body = "The roof is on fire!",
    Priority = Priority.Emergency,
    RetryOptions = new RetryOptions
    {
        RetryEvery = TimeSpan.FromSeconds(30),
        RetryPeriod = TimeSpan.FromHours(3)
    },
    SupplementaryUrl = new SupplementaryURL
    {
        Uri = new Uri("http://robiii.me"),
        Title = "Awesome dude!"
    },
};
var sendtask = po.SendMessageAsync(msg, "[RECIPIENT-ID-HERE]");
```

All REST methods found in the [Pushover API](https://pushover.net/api) are available in this library. We support:

* [Pushing messages](https://pushover.net/api#messages) (including HTML, supplementary URL's, notification sounds etc.)
* [User/Group verification](https://pushover.net/api#verification)
* [Receipts and Callbacks](https://pushover.net/api#receipt) (including cancelling retries)
* [User key migration](https://pushover.net/api/subscriptions#migration)
* [Assigning licenses](https://pushover.net/api/licensing)
* Open Client API
  * [User login](https://pushover.net/api/client#login)
  * [Device Registration](https://pushover.net/api/client#register)
  * [Message Downloading](https://pushover.net/api/client#download)
  * [Message Deleting](https://pushover.net/api/client#delete)
  * [Acknowledging emergency priority messages](https://pushover.net/api/client#p2)

NPushover also has support for reading rate-limiting information returned by Pushover which should make it easy to find out in time when you're about to run out of messages.

## Documentation

NPushover is well documented in code, comes with a helpfile (or [Sandcastle helpfile builder project](https://github.com/EWSoftware/SHFB) if you want to build it yourself). NPushover is built with [Pushover's API documentation](https://pushover.net/api) at hand. Though some names/properties may sometimes deviate a bit to make things more consistent within the API itself and/or to .Net conventions, you should be able to get around in NPushover quickly.

As more and more [unittests](https://github.com/RobThree/NPushOver/tree/master/NPushover.Tests) are created, usage of specifics should be found easily in those unittests too.

[![Build status](https://ci.appveyor.com/api/projects/status/tfa6gnupi0gmd9h5)](https://ci.appveyor.com/project/RobIII/npushover) <a href="https://www.nuget.org/packages/NPushover/"><img src="http://img.shields.io/nuget/v/NPushover.svg?style=flat-square" alt="NuGet version" height="18"></a> <a href="https://www.nuget.org/packages/NPushover/"><img src="http://img.shields.io/nuget/dt/NPushover.svg?style=flat-square" alt="NuGet downloads" height="18"></a>
